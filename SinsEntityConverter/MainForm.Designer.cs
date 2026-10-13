namespace SinsEntityConverter
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSelectFile = new Button();
            btnConvert = new Button();
            btnSelectDir = new Button();
            panelBorder = new Panel();
            outputBox = new InvisibleScrollTextBox();
            btnSelectFiles = new Button();
            darkTabControl = new DarkTabControl();
            binConvert = new TabPage();
            xsiConvert = new TabPage();
            jsonConvert = new TabPage();
            panelBorder.SuspendLayout();
            darkTabControl.SuspendLayout();
            SuspendLayout();
            // 
            // btnSelectFile
            // 
            btnSelectFile.FlatStyle = FlatStyle.Flat;
            btnSelectFile.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSelectFile.Location = new Point(26, 558);
            btnSelectFile.Margin = new Padding(4, 3, 4, 3);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Padding = new Padding(9, 5, 9, 5);
            btnSelectFile.Size = new Size(140, 46);
            btnSelectFile.TabIndex = 2;
            btnSelectFile.Text = "Select File";
            btnSelectFile.UseCompatibleTextRendering = true;
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += btnSelectFile_Click;
            // 
            // btnConvert
            // 
            btnConvert.FlatStyle = FlatStyle.Flat;
            btnConvert.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnConvert.Location = new Point(1251, 558);
            btnConvert.Margin = new Padding(4, 3, 4, 3);
            btnConvert.Name = "btnConvert";
            btnConvert.Padding = new Padding(9, 5, 9, 5);
            btnConvert.Size = new Size(140, 46);
            btnConvert.TabIndex = 5;
            btnConvert.Text = "Convert";
            btnConvert.UseCompatibleTextRendering = true;
            btnConvert.UseVisualStyleBackColor = true;
            btnConvert.Click += btnConvert_Click;
            // 
            // btnSelectDir
            // 
            btnSelectDir.FlatStyle = FlatStyle.Flat;
            btnSelectDir.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSelectDir.Location = new Point(320, 558);
            btnSelectDir.Margin = new Padding(4, 3, 4, 3);
            btnSelectDir.Name = "btnSelectDir";
            btnSelectDir.Padding = new Padding(5);
            btnSelectDir.Size = new Size(140, 46);
            btnSelectDir.TabIndex = 4;
            btnSelectDir.Text = "Select Directory";
            btnSelectDir.UseCompatibleTextRendering = true;
            btnSelectDir.UseVisualStyleBackColor = true;
            btnSelectDir.Click += btnSelectDir_Click;
            // 
            // panelBorder
            // 
            panelBorder.Controls.Add(outputBox);
            panelBorder.Location = new Point(26, 58);
            panelBorder.Margin = new Padding(4, 3, 4, 3);
            panelBorder.Name = "panelBorder";
            panelBorder.Size = new Size(1365, 464);
            panelBorder.TabIndex = 4;
            // 
            // outputBox
            // 
            outputBox.BorderStyle = BorderStyle.None;
            outputBox.Location = new Point(1, 1);
            outputBox.Margin = new Padding(4, 3, 4, 3);
            outputBox.Name = "outputBox";
            outputBox.ReadOnly = true;
            outputBox.Size = new Size(1363, 462);
            outputBox.TabIndex = 1;
            outputBox.Text = "";
            outputBox.WordWrap = false;
            // 
            // btnSelectFiles
            // 
            btnSelectFiles.FlatStyle = FlatStyle.Flat;
            btnSelectFiles.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSelectFiles.Location = new Point(173, 558);
            btnSelectFiles.Margin = new Padding(4, 3, 4, 3);
            btnSelectFiles.Name = "btnSelectFiles";
            btnSelectFiles.Padding = new Padding(9, 5, 9, 5);
            btnSelectFiles.Size = new Size(140, 46);
            btnSelectFiles.TabIndex = 3;
            btnSelectFiles.Text = "Select Files..";
            btnSelectFiles.UseCompatibleTextRendering = true;
            btnSelectFiles.UseVisualStyleBackColor = true;
            btnSelectFiles.Click += btnSelectFiles_Click;
            // 
            // darkTabControl
            // 
            darkTabControl.Controls.Add(binConvert);
            darkTabControl.Controls.Add(xsiConvert);
            darkTabControl.Controls.Add(jsonConvert);
            darkTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            darkTabControl.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            darkTabControl.ItemSize = new Size(120, 35);
            darkTabControl.Location = new Point(27, 8);
            darkTabControl.Margin = new Padding(4, 3, 4, 3);
            darkTabControl.Name = "darkTabControl";
            darkTabControl.SelectedIndex = 0;
            darkTabControl.Size = new Size(368, 37);
            darkTabControl.SizeMode = TabSizeMode.Fixed;
            darkTabControl.TabIndex = 6;
            darkTabControl.SelectedIndexChanged += darkTabControl1_SelectedIndexChanged;
            // 
            // binConvert
            // 
            binConvert.BackColor = Color.FromArgb(30, 30, 30);
            binConvert.Location = new Point(4, 39);
            binConvert.Margin = new Padding(4, 3, 4, 3);
            binConvert.Name = "binConvert";
            binConvert.Padding = new Padding(4, 3, 4, 3);
            binConvert.Size = new Size(360, 0);
            binConvert.TabIndex = 0;
            binConvert.Text = "Binary Convert";
            // 
            // xsiConvert
            // 
            xsiConvert.BackColor = Color.FromArgb(30, 30, 30);
            xsiConvert.Location = new Point(4, 39);
            xsiConvert.Margin = new Padding(4, 3, 4, 3);
            xsiConvert.Name = "xsiConvert";
            xsiConvert.Padding = new Padding(4, 3, 4, 3);
            xsiConvert.Size = new Size(360, 0);
            xsiConvert.TabIndex = 1;
            xsiConvert.Text = "XSI Converter";
            // 
            // jsonConvert
            // 
            jsonConvert.BackColor = Color.FromArgb(30, 30, 30);
            jsonConvert.Location = new Point(4, 39);
            jsonConvert.Name = "jsonConvert";
            jsonConvert.Size = new Size(360, 0);
            jsonConvert.TabIndex = 2;
            jsonConvert.Text = "JSON Converter";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1413, 627);
            Controls.Add(darkTabControl);
            Controls.Add(btnSelectFiles);
            Controls.Add(btnSelectDir);
            Controls.Add(btnConvert);
            Controls.Add(btnSelectFile);
            Controls.Add(panelBorder);
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainForm";
            Padding = new Padding(19, 18, 19, 18);
            Text = "SOASE Resource Converter";
            panelBorder.ResumeLayout(false);
            darkTabControl.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnSelectDir;
        private System.Windows.Forms.Panel panelBorder;
        private InvisibleScrollTextBox outputBox;
        private System.Windows.Forms.Button btnSelectFiles;
        private DarkTabControl darkTabControl;
        private System.Windows.Forms.TabPage binConvert;
        private System.Windows.Forms.TabPage xsiConvert;
        private TabPage jsonConvert;
    }
}