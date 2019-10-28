namespace TINY_language_Scanner
{
    partial class ScannerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScannerForm));
            this.codeTextBox = new System.Windows.Forms.RichTextBox();
            this.codeLabel = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.scanBtn = new MetroFramework.Controls.MetroButton();
            this.outputFileBtn = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // codeTextBox
            // 
            this.codeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.codeTextBox.Font = new System.Drawing.Font("Tahoma", 14F);
            this.codeTextBox.Location = new System.Drawing.Point(3, 131);
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.Size = new System.Drawing.Size(497, 679);
            this.codeTextBox.TabIndex = 0;
            this.codeTextBox.Text = "";
            this.codeTextBox.TextChanged += new System.EventHandler(this.codeTextBox_TextChanged);
            // 
            // codeLabel
            // 
            this.codeLabel.AutoSize = true;
            this.codeLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.codeLabel.Location = new System.Drawing.Point(14, 90);
            this.codeLabel.Name = "codeLabel";
            this.codeLabel.Size = new System.Drawing.Size(158, 25);
            this.codeLabel.TabIndex = 3;
            this.codeLabel.Text = "TINY snippet code";
            // 
            // metroLabel1
            // 
            this.metroLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(523, 90);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(116, 25);
            this.metroLabel1.TabIndex = 3;
            this.metroLabel1.Text = "List of Tokens";
            // 
            // outputTextBox
            // 
            this.outputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputTextBox.Font = new System.Drawing.Font("Tahoma", 14F);
            this.outputTextBox.Location = new System.Drawing.Point(506, 131);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.Size = new System.Drawing.Size(533, 679);
            this.outputTextBox.TabIndex = 2;
            this.outputTextBox.Text = "";
            // 
            // scanBtn
            // 
            this.scanBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.scanBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scanBtn.Location = new System.Drawing.Point(921, 76);
            this.scanBtn.Name = "scanBtn";
            this.scanBtn.Size = new System.Drawing.Size(116, 49);
            this.scanBtn.TabIndex = 1;
            this.scanBtn.Text = "Scan";
            this.scanBtn.Click += new System.EventHandler(this.scanBtn_Click);
            // 
            // outputFileBtn
            // 
            this.outputFileBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.outputFileBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.outputFileBtn.Location = new System.Drawing.Point(799, 76);
            this.outputFileBtn.Name = "outputFileBtn";
            this.outputFileBtn.Size = new System.Drawing.Size(116, 49);
            this.outputFileBtn.TabIndex = 1;
            this.outputFileBtn.Text = "Make Output File";
            this.outputFileBtn.Click += new System.EventHandler(this.outputFileBtn_Click);
            // 
            // ScannerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 814);
            this.Controls.Add(this.outputFileBtn);
            this.Controls.Add(this.scanBtn);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.codeLabel);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.codeTextBox);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "ScannerForm";
            this.Resizable = false;
            this.Text = "TINY Language Scanner";
            this.Theme = MetroFramework.MetroThemeStyle.Light;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox codeTextBox;
        private MetroFramework.Controls.MetroLabel codeLabel;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.RichTextBox outputTextBox;
        private MetroFramework.Controls.MetroButton scanBtn;
        private MetroFramework.Controls.MetroButton outputFileBtn;
    }
}

