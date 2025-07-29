namespace Connect2Cart_Desktop_Hub
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox comboBoxScale;
        private System.Windows.Forms.ComboBox comboBoxPrinter;
        private System.Windows.Forms.Button btnSave;

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
            cmbScale = new ComboBox();
            cmbPrinter = new ComboBox();
            btnSave = new Button();
            SuspendLayout();
            // 
            // cmbScale
            // 
            cmbScale.FormattingEnabled = true;
            cmbScale.Location = new Point(344, 134);
            cmbScale.Name = "cmbScale";
            cmbScale.Size = new Size(242, 40);
            cmbScale.TabIndex = 0;
            // 
            // cmbPrinter
            // 
            cmbPrinter.FormattingEnabled = true;
            cmbPrinter.Location = new Point(344, 209);
            cmbPrinter.Name = "cmbPrinter";
            cmbPrinter.Size = new Size(242, 40);
            cmbPrinter.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(344, 285);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(150, 46);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSave);
            Controls.Add(cmbPrinter);
            Controls.Add(cmbScale);
            Name = "ConfigForm";
            Text = "ConfigForm";
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmbScale;
        private ComboBox cmbPrinter;
        private Button btnSave;
    }
}