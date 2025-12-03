namespace UniversalCompressor
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblInput = new Label();
            txtInputPath = new TextBox();
            btnBrowseInput = new Button();
            panelDropZone = new Panel();
            lblDropHere = new Label();
            lblOutput = new Label();
            txtOutputPath = new TextBox();
            btnBrowseOutput = new Button();
            lblAlgorithm = new Label();
            cmbAlgorithm = new ComboBox();
            lblTitle = new Label();
            grpInput = new GroupBox();
            grpOutput = new GroupBox();
            grpSettings = new GroupBox();
            grpActions = new GroupBox();
            btnDecompress = new Button();
            btnCompress = new Button();
            grpStats = new GroupBox();
            txtStats = new TextBox();
            statusStripMain = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            panelDropZone.SuspendLayout();
            grpInput.SuspendLayout();
            grpOutput.SuspendLayout();
            grpSettings.SuspendLayout();
            grpActions.SuspendLayout();
            grpStats.SuspendLayout();
            statusStripMain.SuspendLayout();
            SuspendLayout();
            // 
            // lblInput
            // 
            lblInput.AutoSize = true;
            lblInput.Location = new Point(16, 30);
            lblInput.Name = "lblInput";
            lblInput.Size = new Size(110, 15);
            lblInput.TabIndex = 0;
            lblInput.Text = "Archivo de entrada:";
            lblInput.Click += label1_Click;
            // 
            // txtInputPath
            // 
            txtInputPath.Location = new Point(132, 26);
            txtInputPath.Name = "txtInputPath";
            txtInputPath.ReadOnly = true;
            txtInputPath.Size = new Size(767, 23);
            txtInputPath.TabIndex = 1;
            // 
            // btnBrowseInput
            // 
            btnBrowseInput.Location = new Point(915, 26);
            btnBrowseInput.Name = "btnBrowseInput";
            btnBrowseInput.Size = new Size(75, 23);
            btnBrowseInput.TabIndex = 2;
            btnBrowseInput.Text = "Buscar...";
            btnBrowseInput.UseVisualStyleBackColor = true;
            btnBrowseInput.Click += btnBrowseInput_Click;
            // 
            // panelDropZone
            // 
            panelDropZone.AllowDrop = true;
            panelDropZone.AutoSize = true;
            panelDropZone.BackColor = SystemColors.ButtonHighlight;
            panelDropZone.BorderStyle = BorderStyle.FixedSingle;
            panelDropZone.Controls.Add(lblDropHere);
            panelDropZone.Location = new Point(427, 55);
            panelDropZone.Name = "panelDropZone";
            panelDropZone.Size = new Size(200, 100);
            panelDropZone.TabIndex = 3;
            panelDropZone.Paint += panel1_Paint;
            // 
            // lblDropHere
            // 
            lblDropHere.AllowDrop = true;
            lblDropHere.Dock = DockStyle.Fill;
            lblDropHere.Location = new Point(0, 0);
            lblDropHere.Name = "lblDropHere";
            lblDropHere.Size = new Size(198, 98);
            lblDropHere.TabIndex = 0;
            lblDropHere.Text = "Arrastra aquí tu archivo de texto";
            lblDropHere.TextAlign = ContentAlignment.MiddleCenter;
            lblDropHere.Click += lblDropHere_Click;
            // 
            // lblOutput
            // 
            lblOutput.AutoSize = true;
            lblOutput.Location = new Point(16, 28);
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new Size(100, 15);
            lblOutput.TabIndex = 4;
            lblOutput.Text = "Archivo de salida:";
            // 
            // txtOutputPath
            // 
            txtOutputPath.Location = new Point(122, 25);
            txtOutputPath.Name = "txtOutputPath";
            txtOutputPath.ReadOnly = true;
            txtOutputPath.Size = new Size(777, 23);
            txtOutputPath.TabIndex = 5;
            // 
            // btnBrowseOutput
            // 
            btnBrowseOutput.Location = new Point(915, 28);
            btnBrowseOutput.Name = "btnBrowseOutput";
            btnBrowseOutput.Size = new Size(128, 23);
            btnBrowseOutput.TabIndex = 6;
            btnBrowseOutput.Text = "Guardar como ...";
            btnBrowseOutput.UseVisualStyleBackColor = true;
            btnBrowseOutput.Click += btnBrowseOutput_Click;
            // 
            // lblAlgorithm
            // 
            lblAlgorithm.AutoSize = true;
            lblAlgorithm.Location = new Point(16, 30);
            lblAlgorithm.Name = "lblAlgorithm";
            lblAlgorithm.Size = new Size(64, 15);
            lblAlgorithm.TabIndex = 7;
            lblAlgorithm.Text = "Algoritmo:";
            // 
            // cmbAlgorithm
            // 
            cmbAlgorithm.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAlgorithm.FormattingEnabled = true;
            cmbAlgorithm.Location = new Point(86, 27);
            cmbAlgorithm.Name = "cmbAlgorithm";
            cmbAlgorithm.Size = new Size(171, 23);
            cmbAlgorithm.TabIndex = 8;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1082, 36);
            lblTitle.TabIndex = 9;
            lblTitle.Text = "UNIVERSAL COMPRESSOR";
            lblTitle.TextAlign = ContentAlignment.BottomCenter;
            lblTitle.Click += lblTitle_Click;
            // 
            // grpInput
            // 
            grpInput.Controls.Add(lblInput);
            grpInput.Controls.Add(txtInputPath);
            grpInput.Controls.Add(btnBrowseInput);
            grpInput.Controls.Add(panelDropZone);
            grpInput.Location = new Point(12, 34);
            grpInput.Name = "grpInput";
            grpInput.Size = new Size(1058, 171);
            grpInput.TabIndex = 10;
            grpInput.TabStop = false;
            grpInput.Text = "Entrada del archivo";
            // 
            // grpOutput
            // 
            grpOutput.Controls.Add(lblOutput);
            grpOutput.Controls.Add(txtOutputPath);
            grpOutput.Controls.Add(btnBrowseOutput);
            grpOutput.Location = new Point(12, 211);
            grpOutput.Name = "grpOutput";
            grpOutput.Size = new Size(1058, 100);
            grpOutput.TabIndex = 11;
            grpOutput.TabStop = false;
            grpOutput.Text = "Archivo de salida";
            // 
            // grpSettings
            // 
            grpSettings.Controls.Add(lblAlgorithm);
            grpSettings.Controls.Add(cmbAlgorithm);
            grpSettings.Location = new Point(12, 317);
            grpSettings.Name = "grpSettings";
            grpSettings.Size = new Size(270, 69);
            grpSettings.TabIndex = 12;
            grpSettings.TabStop = false;
            grpSettings.Text = "Configuración";
            // 
            // grpActions
            // 
            grpActions.Controls.Add(btnDecompress);
            grpActions.Controls.Add(btnCompress);
            grpActions.Location = new Point(12, 392);
            grpActions.Name = "grpActions";
            grpActions.Size = new Size(270, 116);
            grpActions.TabIndex = 13;
            grpActions.TabStop = false;
            grpActions.Text = "Acciones";
            // 
            // btnDecompress
            // 
            btnDecompress.Location = new Point(16, 74);
            btnDecompress.Name = "btnDecompress";
            btnDecompress.Size = new Size(241, 23);
            btnDecompress.TabIndex = 14;
            btnDecompress.Text = "Descomprimir";
            btnDecompress.UseVisualStyleBackColor = true;
            btnDecompress.Click += btnDecompress_Click;
            // 
            // btnCompress
            // 
            btnCompress.Location = new Point(16, 31);
            btnCompress.Name = "btnCompress";
            btnCompress.Size = new Size(241, 23);
            btnCompress.TabIndex = 14;
            btnCompress.Text = "Comprimir";
            btnCompress.UseVisualStyleBackColor = true;
            btnCompress.Click += btnCompress_Click;
            // 
            // grpStats
            // 
            grpStats.Controls.Add(txtStats);
            grpStats.Location = new Point(288, 317);
            grpStats.Name = "grpStats";
            grpStats.Size = new Size(782, 191);
            grpStats.TabIndex = 14;
            grpStats.TabStop = false;
            grpStats.Text = "Resultados y estadísticas";
            // 
            // txtStats
            // 
            txtStats.Dock = DockStyle.Fill;
            txtStats.Location = new Point(3, 19);
            txtStats.Multiline = true;
            txtStats.Name = "txtStats";
            txtStats.ReadOnly = true;
            txtStats.ScrollBars = ScrollBars.Vertical;
            txtStats.Size = new Size(776, 169);
            txtStats.TabIndex = 0;
            // 
            // statusStripMain
            // 
            statusStripMain.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStripMain.Location = new Point(0, 517);
            statusStripMain.Name = "statusStripMain";
            statusStripMain.Size = new Size(1082, 22);
            statusStripMain.TabIndex = 15;
            statusStripMain.Text = "statusStrip1";
            statusStripMain.ItemClicked += statusStripMain_ItemClicked;
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(32, 17);
            lblStatus.Text = "Listo";
            lblStatus.Click += lblStatus_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1082, 539);
            Controls.Add(statusStripMain);
            Controls.Add(grpStats);
            Controls.Add(grpActions);
            Controls.Add(grpSettings);
            Controls.Add(grpOutput);
            Controls.Add(grpInput);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Universal Compressor";
            Load += Form1_Load;
            panelDropZone.ResumeLayout(false);
            grpInput.ResumeLayout(false);
            grpInput.PerformLayout();
            grpOutput.ResumeLayout(false);
            grpOutput.PerformLayout();
            grpSettings.ResumeLayout(false);
            grpSettings.PerformLayout();
            grpActions.ResumeLayout(false);
            grpStats.ResumeLayout(false);
            grpStats.PerformLayout();
            statusStripMain.ResumeLayout(false);
            statusStripMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblInput;
        private TextBox txtInputPath;
        private Button btnBrowseInput;
        private Panel panelDropZone;
        private Label lblDropHere;
        private Label lblOutput;
        private TextBox txtOutputPath;
        private Button btnBrowseOutput;
        private Label lblAlgorithm;
        private ComboBox cmbAlgorithm;
        private Label lblTitle;
        private GroupBox grpInput;
        private GroupBox grpOutput;
        private GroupBox grpSettings;
        private GroupBox grpActions;
        private Button btnDecompress;
        private Button btnCompress;
        private GroupBox grpStats;
        private TextBox txtStats;
        private StatusStrip statusStripMain;
        private ToolStripStatusLabel lblStatus;
    }
}
