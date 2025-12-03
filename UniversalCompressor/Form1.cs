using System;
using System.IO;
using System.Windows.Forms;
using UniversalCompressor.Services;

namespace UniversalCompressor
{
    public partial class Form1 : Form
    {
        private readonly CompressionService _compressionService;

        public Form1()
        {
            InitializeComponent();
            _compressionService = new CompressionService();

            // 🔹 Muy IMPORTANTE: aquí conectamos los eventos de drag & drop
            panelDropZone.DragEnter += panelDropZone_DragEnter;
            panelDropZone.DragDrop += panelDropZone_DragDrop;

            lblDropHere.DragEnter += panelDropZone_DragEnter;
            lblDropHere.DragDrop += panelDropZone_DragDrop;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Luego llenamos el ComboBox aquí
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // No hacemos nada por ahora
        }

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Seleccionar archivo de entrada";
                dialog.Filter = "Archivos de texto (*.txt)|*.txt"; // Solo .txt

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtInputPath.Text = dialog.FileName;
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // No usamos este evento por ahora
        }

        private void lblDropHere_Click(object sender, EventArgs e)
        {
            // Este es solo el click del texto, no lo necesitamos para drag & drop
        }

        private void panelDropZone_DragEnter(object sender, DragEventArgs e)
        {
            // Se ejecuta cuando el mouse entra a la zona de drop con algo arrastrado
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length > 0)
                {
                    string extension = Path.GetExtension(files[0]);

                    // Aceptamos solo .txt
                    if (string.Equals(extension, ".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        e.Effect = DragDropEffects.Copy;
                        return;
                    }
                }
            }

            // Si no es válido, cursor bloqueado
            e.Effect = DragDropEffects.None;
        }

        private void panelDropZone_DragDrop(object sender, DragEventArgs e)
        {
            // Se ejecuta al SOLTAR el archivo
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length > 0)
                {
                    string filePath = files[0];
                    string extension = Path.GetExtension(filePath);

                    if (string.Equals(extension, ".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        // Ponemos la ruta en el textbox de entrada
                        txtInputPath.Text = filePath;
                    }
                    else
                    {
                        MessageBox.Show(
                            "Solo se permiten archivos de texto (.txt).",
                            "Tipo de archivo no válido",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Title = "Seleccionar archivo de salida";
                dialog.Filter = "Archivo comprimido (*.myzip)|*.myzip";
                dialog.DefaultExt = "myzip";
                dialog.AddExtension = true;

                // Si ya hay archivo de entrada, sugerimos el mismo nombre pero con .myzip
                if (!string.IsNullOrWhiteSpace(txtInputPath.Text))
                {
                    try
                    {
                        string folder = Path.GetDirectoryName(txtInputPath.Text)!;
                        string nameWithoutExt = Path.GetFileNameWithoutExtension(txtInputPath.Text);
                        dialog.InitialDirectory = folder;
                        dialog.FileName = nameWithoutExt + ".myzip";
                    }
                    catch
                    {
                        // Si algo falla con la ruta, no pasa nada, dejamos los valores por defecto
                    }
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputPath.Text = dialog.FileName;
                }
            }
        }
    }
}
