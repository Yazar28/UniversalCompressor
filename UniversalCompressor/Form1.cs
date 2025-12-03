using System;
using System.IO;
using System.Windows.Forms;
using UniversalCompressor.Services;
using UniversalCompressor.Models;

namespace UniversalCompressor
{
    public partial class MainForm : Form
    {
        private readonly CompressionService _compressionService;

        public MainForm()
        {
            InitializeComponent();
            _compressionService = new CompressionService();

            panelDropZone.DragEnter += panelDropZone_DragEnter;
            panelDropZone.DragDrop += panelDropZone_DragDrop;
            lblDropHere.DragEnter += panelDropZone_DragEnter;
            lblDropHere.DragDrop += panelDropZone_DragDrop;
        }

        private void SetStatus(string message)
        {
            lblStatus.Text = message;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbAlgorithm.Items.Clear();
            cmbAlgorithm.Items.Add("Seleccione un algoritmo...");

            foreach (var alg in _compressionService.GetAlgorithms())
            {
                cmbAlgorithm.Items.Add(alg.Name);
            }

            cmbAlgorithm.SelectedIndex = 0;
            SetStatus("Listo.");
        }

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Seleccionar archivo de entrada";
                dialog.Filter =
                    "Archivos de texto (*.txt)|*.txt|Archivos comprimidos (*.myzip)|*.myzip";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtInputPath.Text = dialog.FileName;
                    SetStatus("Archivo de entrada seleccionado.");
                }
            }
        }

        private void panelDropZone_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length > 0)
                {
                    string extension = Path.GetExtension(files[0]);

                    if (string.Equals(extension, ".txt", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(extension, ".myzip", StringComparison.OrdinalIgnoreCase))
                    {
                        e.Effect = DragDropEffects.Copy;
                        return;
                    }
                }
            }

            e.Effect = DragDropEffects.None;
        }

        private void panelDropZone_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length > 0)
                {
                    string filePath = files[0];
                    string extension = Path.GetExtension(filePath);

                    if (string.Equals(extension, ".txt", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(extension, ".myzip", StringComparison.OrdinalIgnoreCase))
                    {
                        txtInputPath.Text = filePath;
                        SetStatus("Archivo de entrada seleccionado mediante arrastre.");
                    }
                    else
                    {
                        MessageBox.Show(
                            "Solo se permiten archivos .txt o .myzip.",
                            "Tipo de archivo no válido",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        SetStatus("Error: tipo de archivo no válido.");
                    }
                }
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Title = "Seleccionar archivo de salida";

                string inputExt = Path.GetExtension(txtInputPath.Text);

                if (inputExt.Equals(".myzip", StringComparison.OrdinalIgnoreCase))
                {
                    dialog.Filter = "Archivos de texto (*.txt)|*.txt";
                    dialog.DefaultExt = "txt";
                }
                else
                {
                    dialog.Filter = "Archivo comprimido (*.myzip)|*.myzip";
                    dialog.DefaultExt = "myzip";
                }

                dialog.AddExtension = true;

                if (!string.IsNullOrWhiteSpace(txtInputPath.Text))
                {
                    try
                    {
                        string folder = Path.GetDirectoryName(txtInputPath.Text)!;
                        string nameWithoutExt = Path.GetFileNameWithoutExtension(txtInputPath.Text);

                        if (inputExt.Equals(".myzip", StringComparison.OrdinalIgnoreCase))
                        {
                            dialog.InitialDirectory = folder;
                            dialog.FileName = nameWithoutExt + ".txt";
                        }
                        else
                        {
                            dialog.InitialDirectory = folder;
                            dialog.FileName = nameWithoutExt + ".myzip";
                        }
                    }
                    catch
                    {
                    }
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputPath.Text = dialog.FileName;
                    SetStatus("Archivo de salida seleccionado.");
                }
            }
        }

        private void ShowResult(CompressionResult result, string operation)
        {
            if (!result.Success)
            {
                txtStats.Text =
                    $"Operación: {operation}{Environment.NewLine}" +
                    $"Algoritmo: {result.AlgorithmName}{Environment.NewLine}" +
                    $"ERROR: {result.ErrorMessage}";
                SetStatus($"Error en {operation.ToLower()}.");
                return;
            }

            double ratio = result.CompressionRatio;
            double reductionPercent = 0;

            if (result.OriginalSizeBytes > 0)
            {
                reductionPercent = (1 - ratio) * 100.0;
            }

            txtStats.Text =
                $"Operación: {operation}{Environment.NewLine}" +
                $"Algoritmo: {result.AlgorithmName}{Environment.NewLine}" +
                $"Archivo de entrada: {result.InputFilePath}{Environment.NewLine}" +
                $"Archivo de salida:  {result.OutputFilePath}{Environment.NewLine}" +
                $"Tamaño original:   {result.OriginalSizeBytes} bytes{Environment.NewLine}" +
                $"Tamaño resultado:  {result.CompressedSizeBytes} bytes{Environment.NewLine}" +
                $"Razón comprimido/original: {ratio:F3}{Environment.NewLine}" +
                $"Reducción aproximada: {reductionPercent:F2}%{Environment.NewLine}" +
                $"Tiempo transcurrido: {result.ElapsedTime.TotalMilliseconds:F2} ms{Environment.NewLine}" +
                $"Memoria usada (aprox.): {result.MemoryUsedBytes} bytes";

            SetStatus($"{operation} completada correctamente.");
        }

        private void btnCompress_Click(object sender, EventArgs e)
        {
            txtStats.Clear();
            SetStatus("Iniciando compresión...");

            var filesToCompress = new List<string>();

            if (lstInputFiles.Items.Count > 0)
            {
                foreach (var item in lstInputFiles.Items)
                {
                    if (item is string path)
                    {
                        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                        {
                            MessageBox.Show(
                                $"El archivo de la lista no existe:\n{path}",
                                "Archivo no encontrado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            SetStatus("Error: uno de los archivos de la lista no existe.");
                            return;
                        }

                        string ext = Path.GetExtension(path);
                        if (!ext.Equals(".txt", StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show(
                                $"Solo se permiten archivos de texto (.txt) en la lista.\nArchivo inválido:\n{path}",
                                "Tipo de archivo no válido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            SetStatus("Error: tipo de archivo no válido en la lista.");
                            return;
                        }

                        filesToCompress.Add(path);
                    }
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtInputPath.Text) || !File.Exists(txtInputPath.Text))
                {
                    MessageBox.Show(
                        "Debes seleccionar un archivo de entrada válido.",
                        "Archivo de entrada no válido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    SetStatus("Error: archivo de entrada no válido.");
                    return;
                }

                string ext = Path.GetExtension(txtInputPath.Text);
                if (!ext.Equals(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(
                        "Para comprimir se espera un archivo de texto (.txt).",
                        "Tipo de archivo no válido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    SetStatus("Error: tipo de archivo de entrada no válido.");
                    return;
                }

                filesToCompress.Add(txtInputPath.Text);
            }

            if (string.IsNullOrWhiteSpace(txtOutputPath.Text))
            {
                MessageBox.Show(
                    "Debes seleccionar un archivo de salida.",
                    "Archivo de salida no válido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                SetStatus("Error: archivo de salida no válido.");
                return;
            }

            string outputExt = Path.GetExtension(txtOutputPath.Text);
            if (!outputExt.Equals(".myzip", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "Para comprimir se espera un archivo de salida con extensión .myzip.",
                    "Extensión de salida no válida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                SetStatus("Error: extensión de salida no válida.");
                return;
            }

            if (cmbAlgorithm.SelectedIndex <= 0)
            {
                MessageBox.Show(
                    "Debes seleccionar un algoritmo de compresión.",
                    "Algoritmo no seleccionado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                SetStatus("Error: algoritmo no seleccionado.");
                return;
            }

            string algorithmName = cmbAlgorithm.SelectedItem!.ToString()!;

            try
            {
                var result = _compressionService.CompressMultiple(
                    algorithmName,
                    filesToCompress,
                    txtOutputPath.Text);

                ShowResult(result, "Compresión");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error inesperado durante la compresión:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                SetStatus("Error inesperado en compresión.");
            }
        }

        private void btnDecompress_Click(object sender, EventArgs e)
        {
            txtStats.Clear();
            SetStatus("Iniciando descompresión...");

            if (string.IsNullOrWhiteSpace(txtInputPath.Text) || !File.Exists(txtInputPath.Text))
            {
                MessageBox.Show(
                    "Debes seleccionar un archivo de entrada.",
                    "Archivo de entrada no válido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                SetStatus("Error: archivo de entrada no válido.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtOutputPath.Text))
            {
                MessageBox.Show(
                    "Debes seleccionar una ruta de salida.",
                    "Ruta de salida no válida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                SetStatus("Error: ruta de salida no válida.");
                return;
            }

            string inputExt = Path.GetExtension(txtInputPath.Text);
            string outputExt = Path.GetExtension(txtOutputPath.Text);

            if (inputExt.Equals(".txt", StringComparison.OrdinalIgnoreCase) &&
                outputExt.Equals(".myzip", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "Con archivo de entrada .txt y salida .myzip debes usar el botón 'Comprimir', no 'Descomprimir'.",
                    "Acción no válida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                SetStatus("Acción de descompresión no válida para .txt → .myzip.");
                return;
            }

            if (!inputExt.Equals(".myzip", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "Para descomprimir se espera un archivo de entrada con extensión .myzip.",
                    "Extensión de entrada no válida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                SetStatus("Error: extensión de entrada no válida para descompresión.");
                return;
            }

            string? outputDirectory = Path.GetDirectoryName(txtOutputPath.Text);
            if (string.IsNullOrEmpty(outputDirectory))
            {
                outputDirectory = Environment.CurrentDirectory;
            }

            if (cmbAlgorithm.SelectedIndex <= 0)
            {
                MessageBox.Show(
                    "Debes seleccionar un algoritmo para descomprimir.",
                    "Algoritmo no seleccionado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                SetStatus("Error: algoritmo no seleccionado.");
                return;
            }

            string algorithmName = cmbAlgorithm.SelectedItem!.ToString()!;

            try
            {
                var result = _compressionService.DecompressMultiple(
                    algorithmName,
                    txtInputPath.Text,
                    outputDirectory
                );

                ShowResult(result, "Descompresión");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error inesperado durante la descompresión:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                SetStatus("Error inesperado en descompresión.");
            }
        }
        private void txtInputPath_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInputPath.Text))
                return;

            try
            {
                string ext = Path.GetExtension(txtInputPath.Text);
                string? folder = Path.GetDirectoryName(txtInputPath.Text);
                string nameWithoutExt = Path.GetFileNameWithoutExtension(txtInputPath.Text);

                if (string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(nameWithoutExt))
                    return;

                if (ext.Equals(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    txtOutputPath.Text = Path.Combine(folder, nameWithoutExt + ".myzip");
                    SetStatus("Modo compresión sugerido (.txt → .myzip).");
                }
                else if (ext.Equals(".myzip", StringComparison.OrdinalIgnoreCase))
                {
                    txtOutputPath.Text = Path.Combine(folder, nameWithoutExt + ".txt");
                    SetStatus("Modo descompresión sugerido (.myzip → .txt).");
                }
            }
            catch
            {
            }
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Seleccionar archivos de texto";
                dialog.Filter = "Archivos de texto (*.txt)|*.txt";
                dialog.Multiselect = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var file in dialog.FileNames)
                    {
                        if (!lstInputFiles.Items.Contains(file))
                        {
                            lstInputFiles.Items.Add(file);
                        }
                    }

                    if (lstInputFiles.Items.Count > 0)
                    {
                        SetStatus("Archivos de entrada agregados a la lista.");
                    }
                }
            }
        }

        private void btnClearFiles_Click(object sender, EventArgs e)
        {
            lstInputFiles.Items.Clear();
            SetStatus("Lista de archivos de entrada vaciada.");
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void lblDropHere_Click(object sender, EventArgs e) { }
        private void cmbAlgorithm_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtOutputPath_TextChanged(object sender, EventArgs e) { }
        private void lblTitle_Click(object sender, EventArgs e) { }
        private void statusStripMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }
        private void lblStatus_Click(object sender, EventArgs e) { }
        private void grpInput_Enter(object sender, EventArgs e) { }
    }
}
