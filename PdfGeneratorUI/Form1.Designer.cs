namespace PdfGeneratorUI
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
            gerarPdf_button = new Button();
            html_TextBox = new RichTextBox();
            SuspendLayout();
            // 
            // gerarPdf_button
            // 
            gerarPdf_button.Location = new Point(53, 381);
            gerarPdf_button.Name = "gerarPdf_button";
            gerarPdf_button.Size = new Size(75, 23);
            gerarPdf_button.TabIndex = 0;
            gerarPdf_button.Text = "Gerar PDF";
            gerarPdf_button.UseVisualStyleBackColor = true;
            gerarPdf_button.Click += gerarPdf_button_Click;
            // 
            // html_TextBox
            // 
            html_TextBox.Location = new Point(12, 12);
            html_TextBox.Name = "html_TextBox";
            html_TextBox.Size = new Size(637, 332);
            html_TextBox.TabIndex = 1;
            html_TextBox.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(661, 450);
            Controls.Add(html_TextBox);
            Controls.Add(gerarPdf_button);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button gerarPdf_button;
        private RichTextBox html_TextBox;
    }
}
