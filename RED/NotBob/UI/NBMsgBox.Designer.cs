namespace NotBob.UI
{
    partial class NBMsgBox
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NBMsgBox));
			this.UxMessageText = new System.Windows.Forms.Label();
			this.UxButton1 = new System.Windows.Forms.Button();
			this.UxButton2 = new System.Windows.Forms.Button();
			this.UxButton3 = new System.Windows.Forms.Button();
			this.UxButton4 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// UxMessageText
			// 
			this.UxMessageText.AutoSize = true;
			this.UxMessageText.Location = new System.Drawing.Point(66, 12);
			this.UxMessageText.Name = "UxMessageText";
			this.UxMessageText.Size = new System.Drawing.Size(49, 13);
			this.UxMessageText.TabIndex = 0;
			this.UxMessageText.Text = "message";
			// 
			// UxButton1
			// 
			this.UxButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.UxButton1.AutoSize = true;
			this.UxButton1.Location = new System.Drawing.Point(16, 62);
			this.UxButton1.Name = "UxButton1";
			this.UxButton1.Size = new System.Drawing.Size(75, 23);
			this.UxButton1.TabIndex = 1;
			this.UxButton1.Text = "button1";
			this.UxButton1.UseVisualStyleBackColor = true;
			this.UxButton1.Click += new System.EventHandler(this.UxButton_Click);
			// 
			// UxButton2
			// 
			this.UxButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.UxButton2.AutoSize = true;
			this.UxButton2.Location = new System.Drawing.Point(97, 62);
			this.UxButton2.Name = "UxButton2";
			this.UxButton2.Size = new System.Drawing.Size(75, 23);
			this.UxButton2.TabIndex = 2;
			this.UxButton2.Text = "button2";
			this.UxButton2.UseVisualStyleBackColor = true;
			this.UxButton2.Click += new System.EventHandler(this.UxButton_Click);
			// 
			// UxButton3
			// 
			this.UxButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.UxButton3.AutoSize = true;
			this.UxButton3.Location = new System.Drawing.Point(178, 62);
			this.UxButton3.Name = "UxButton3";
			this.UxButton3.Size = new System.Drawing.Size(75, 23);
			this.UxButton3.TabIndex = 3;
			this.UxButton3.Text = "button3";
			this.UxButton3.UseVisualStyleBackColor = true;
			this.UxButton3.Click += new System.EventHandler(this.UxButton_Click);
			// 
			// UxButton4
			// 
			this.UxButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.UxButton4.AutoSize = true;
			this.UxButton4.Location = new System.Drawing.Point(259, 62);
			this.UxButton4.Name = "UxButton4";
			this.UxButton4.Size = new System.Drawing.Size(75, 23);
			this.UxButton4.TabIndex = 4;
			this.UxButton4.Text = "button4";
			this.UxButton4.UseVisualStyleBackColor = true;
			this.UxButton4.Click += new System.EventHandler(this.UxButton_Click);
			// 
			// NBMsgBox
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(355, 97);
			this.Controls.Add(this.UxButton4);
			this.Controls.Add(this.UxButton3);
			this.Controls.Add(this.UxButton2);
			this.Controls.Add(this.UxButton1);
			this.Controls.Add(this.UxMessageText);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NBMsgBox";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "NBMsgBox";
			this.Shown += new System.EventHandler(this.NBMsgBox_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label UxMessageText;
        private System.Windows.Forms.Button UxButton1;
        private System.Windows.Forms.Button UxButton2;
        private System.Windows.Forms.Button UxButton3;
        private System.Windows.Forms.Button UxButton4;
    }
}