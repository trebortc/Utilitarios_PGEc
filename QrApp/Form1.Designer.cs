namespace QrApp
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
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            button1 = new Button();
            openFileDialog1 = new OpenFileDialog();
            label5 = new Label();
            label6 = new Label();
            comboBox1 = new ComboBox();
            pictureBox1 = new PictureBox();
            button2 = new Button();
            menuStrip1 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            limpiarNombresToolStripMenuItem = new ToolStripMenuItem();
            ordenNombresToolStripMenuItem = new ToolStripMenuItem();
            virarArchivosToolStripMenuItem = new ToolStripMenuItem();
            copiarDatosArchivoToolStripMenuItem = new ToolStripMenuItem();
            separarPdfToolStripMenuItem = new ToolStripMenuItem();
            separarPdf2ToolStripMenuItem = new ToolStripMenuItem();
            servidorToolStripMenuItem = new ToolStripMenuItem();
            generarCarpetaToolStripMenuItem = new ToolStripMenuItem();
            subirArchivosToolStripMenuItem = new ToolStripMenuItem();
            folderBrowserDialog1 = new FolderBrowserDialog();
            progressBar1 = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(8, 144);
            label1.Name = "label1";
            label1.Size = new Size(145, 20);
            label1.TabIndex = 0;
            label1.Text = "Dirección sitio web:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(40, 193);
            label2.Name = "label2";
            label2.Size = new Size(113, 20);
            label2.TabIndex = 1;
            label2.Text = "Nombre curso:";
            // 
            // textBox1
            // 
            textBox1.Enabled = false;
            textBox1.Location = new Point(168, 144);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(622, 27);
            textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            textBox2.Enabled = false;
            textBox2.Location = new Point(168, 193);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(323, 27);
            textBox2.TabIndex = 3;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 245);
            label3.Name = "label3";
            label3.Size = new Size(122, 20);
            label3.TabIndex = 4;
            label3.Text = "Cargar usuarios:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Copperplate Gothic Bold", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(275, 48);
            label4.Name = "label4";
            label4.Size = new Size(291, 31);
            label4.TabIndex = 6;
            label4.Text = "GENERADOR QRs";
            label4.Click += label4_Click;
            // 
            // button1
            // 
            button1.Location = new Point(168, 268);
            button1.Name = "button1";
            button1.Size = new Size(134, 29);
            button1.TabIndex = 7;
            button1.Text = "cargar archivo";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "Archivos txt (*.xlsx)|*.xlsx";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label5.Location = new Point(168, 245);
            label5.MinimumSize = new Size(600, 0);
            label5.Name = "label5";
            label5.Size = new Size(600, 20);
            label5.TabIndex = 8;
            label5.Click += label5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(76, 105);
            label6.Name = "label6";
            label6.Size = new Size(77, 20);
            label6.TabIndex = 9;
            label6.Text = "PGE sede:";
            label6.Click += label6_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "PRINCIPAL", "REGIONAL 1" });
            comboBox1.Location = new Point(168, 102);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(323, 28);
            comboBox1.TabIndex = 10;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = ResourceForm.QrPGE;
            pictureBox1.Location = new Point(311, 458);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(180, 57);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // button2
            // 
            button2.Location = new Point(351, 399);
            button2.Name = "button2";
            button2.Size = new Size(140, 29);
            button2.TabIndex = 12;
            button2.Text = "GENERAR QRs";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem, servidorToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(820, 28);
            menuStrip1.TabIndex = 13;
            menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { limpiarNombresToolStripMenuItem, ordenNombresToolStripMenuItem, virarArchivosToolStripMenuItem, copiarDatosArchivoToolStripMenuItem, separarPdfToolStripMenuItem, separarPdf2ToolStripMenuItem });
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(73, 24);
            archivoToolStripMenuItem.Text = "Archivo";
            // 
            // limpiarNombresToolStripMenuItem
            // 
            limpiarNombresToolStripMenuItem.Name = "limpiarNombresToolStripMenuItem";
            limpiarNombresToolStripMenuItem.Size = new Size(229, 26);
            limpiarNombresToolStripMenuItem.Text = "Limpiar nombres";
            limpiarNombresToolStripMenuItem.Click += limpiarNombresToolStripMenuItem_Click;
            // 
            // ordenNombresToolStripMenuItem
            // 
            ordenNombresToolStripMenuItem.Name = "ordenNombresToolStripMenuItem";
            ordenNombresToolStripMenuItem.Size = new Size(229, 26);
            ordenNombresToolStripMenuItem.Text = "Orden nombres";
            ordenNombresToolStripMenuItem.Click += ordenNombresToolStripMenuItem_Click;
            // 
            // virarArchivosToolStripMenuItem
            // 
            virarArchivosToolStripMenuItem.Name = "virarArchivosToolStripMenuItem";
            virarArchivosToolStripMenuItem.Size = new Size(229, 26);
            virarArchivosToolStripMenuItem.Text = "Virar archivos";
            virarArchivosToolStripMenuItem.Click += virarArchivosToolStripMenuItem_Click;
            // 
            // copiarDatosArchivoToolStripMenuItem
            // 
            copiarDatosArchivoToolStripMenuItem.Name = "copiarDatosArchivoToolStripMenuItem";
            copiarDatosArchivoToolStripMenuItem.Size = new Size(229, 26);
            copiarDatosArchivoToolStripMenuItem.Text = "Copiar datos archivo";
            copiarDatosArchivoToolStripMenuItem.Click += copiarDatosArchivoToolStripMenuItem_Click;
            // 
            // separarPdfToolStripMenuItem
            // 
            separarPdfToolStripMenuItem.Name = "separarPdfToolStripMenuItem";
            separarPdfToolStripMenuItem.Size = new Size(229, 26);
            separarPdfToolStripMenuItem.Text = "Separar pdf ";
            separarPdfToolStripMenuItem.Click += separarPdfToolStripMenuItem_Click;
            // 
            // separarPdf2ToolStripMenuItem
            // 
            separarPdf2ToolStripMenuItem.Name = "separarPdf2ToolStripMenuItem";
            separarPdf2ToolStripMenuItem.Size = new Size(229, 26);
            separarPdf2ToolStripMenuItem.Text = "Separar pdf2";
            separarPdf2ToolStripMenuItem.Click += separarPdf2ToolStripMenuItem_Click;
            // 
            // servidorToolStripMenuItem
            // 
            servidorToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { generarCarpetaToolStripMenuItem, subirArchivosToolStripMenuItem });
            servidorToolStripMenuItem.Name = "servidorToolStripMenuItem";
            servidorToolStripMenuItem.Size = new Size(78, 24);
            servidorToolStripMenuItem.Text = "Servidor";
            // 
            // generarCarpetaToolStripMenuItem
            // 
            generarCarpetaToolStripMenuItem.Name = "generarCarpetaToolStripMenuItem";
            generarCarpetaToolStripMenuItem.Size = new Size(198, 26);
            generarCarpetaToolStripMenuItem.Text = "Generar carpeta";
            generarCarpetaToolStripMenuItem.Click += generarCarpetaToolStripMenuItem_Click;
            // 
            // subirArchivosToolStripMenuItem
            // 
            subirArchivosToolStripMenuItem.Name = "subirArchivosToolStripMenuItem";
            subirArchivosToolStripMenuItem.Size = new Size(198, 26);
            subirArchivosToolStripMenuItem.Text = "Subir archivos";
            subirArchivosToolStripMenuItem.Click += subirArchivosToolStripMenuItem_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(108, 345);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(622, 29);
            progressBar1.TabIndex = 14;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(820, 556);
            Controls.Add(progressBar1);
            Controls.Add(button2);
            Controls.Add(pictureBox1);
            Controls.Add(comboBox1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            StartPosition = FormStartPosition.WindowsDefaultBounds;
            Text = "Generador QRs";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label3;
        private Label label4;
        private Button button1;
        private OpenFileDialog openFileDialog1;
        private Label label5;
        private Label label6;
        private ComboBox comboBox1;
        private PictureBox pictureBox1;
        private Button button2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private ToolStripMenuItem limpiarNombresToolStripMenuItem;
        private ToolStripMenuItem ordenNombresToolStripMenuItem;
        private FolderBrowserDialog folderBrowserDialog1;
        private ToolStripMenuItem servidorToolStripMenuItem;
        private ToolStripMenuItem generarCarpetaToolStripMenuItem;
        private ToolStripMenuItem subirArchivosToolStripMenuItem;
        private ProgressBar progressBar1;
        private ToolStripMenuItem virarArchivosToolStripMenuItem;
        private ToolStripMenuItem copiarDatosArchivoToolStripMenuItem;
        private ToolStripMenuItem separarPdfToolStripMenuItem;
        private ToolStripMenuItem separarPdf2ToolStripMenuItem;
    }
}
