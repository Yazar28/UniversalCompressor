namespace UniversalCompressor
{
    partial class Form1
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
            panelDropZone.SuspendLayout();
            SuspendLayout();
            // 
            // lblInput
            // 
            lblInput.AutoSize = true;
            lblInput.Location = new Point(29, 31);
            lblInput.Name = "lblInput";
            lblInput.Size = new Size(110, 15);
            lblInput.TabIndex = 0;
            lblInput.Text = "Archivo de entrada:";
            lblInput.Click += label1_Click;
            // 
            // txtInputPath
            // 
            txtInputPath.Location = new Point(145, 29);
            txtInputPath.Name = "txtInputPath";
            txtInputPath.ReadOnly = true;
            txtInputPath.Size = new Size(767, 23);
            txtInputPath.TabIndex = 1;
            // 
            // btnBrowseInput
            // 
            btnBrowseInput.Location = new Point(927, 28);
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
            panelDropZone.Location = new Point(379, 71);
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
            lblOutput.Location = new Point(29, 204);
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new Size(100, 15);
            lblOutput.TabIndex = 4;
            lblOutput.Text = "Archivo de salida:";
            // 
            // txtOutputPath
            // 
            txtOutputPath.Location = new Point(135, 201);
            txtOutputPath.Name = "txtOutputPath";
            txtOutputPath.ReadOnly = true;
            txtOutputPath.Size = new Size(777, 23);
            txtOutputPath.TabIndex = 5;
            // 
            // btnBrowseOutput
            // 
            btnBrowseOutput.Location = new Point(927, 201);
            btnBrowseOutput.Name = "btnBrowseOutput";
            btnBrowseOutput.Size = new Size(128, 23);
            btnBrowseOutput.TabIndex = 6;
            btnBrowseOutput.Text = "Guardar como ...";
            btnBrowseOutput.UseVisualStyleBackColor = true;
            btnBrowseOutput.Click += btnBrowseOutput_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1082, 603);
            Controls.Add(btnBrowseOutput);
            Controls.Add(txtOutputPath);
            Controls.Add(lblOutput);
            Controls.Add(panelDropZone);
            Controls.Add(btnBrowseInput);
            Controls.Add(txtInputPath);
            Controls.Add(lblInput);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panelDropZone.ResumeLayout(false);
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
    }
}
