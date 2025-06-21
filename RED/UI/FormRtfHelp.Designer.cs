
namespace RED.UI
{
    partial class FormRtfHelp
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
			this.btnHelp1Cancel = new System.Windows.Forms.Button();
			this.btnHelp1OK = new System.Windows.Forms.Button();
			this.rtfHelpText = new System.Windows.Forms.RichTextBox();
			this.pnlHelp1 = new System.Windows.Forms.Panel();
			this.pnlHelpActions = new System.Windows.Forms.Panel();
			this.pnlHelp1.SuspendLayout();
			this.pnlHelpActions.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnHelp1Cancel
			// 
			this.btnHelp1Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp1Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnHelp1Cancel.Location = new System.Drawing.Point(360, 4);
			this.btnHelp1Cancel.Name = "btnHelp1Cancel";
			this.btnHelp1Cancel.Size = new System.Drawing.Size(75, 23);
			this.btnHelp1Cancel.TabIndex = 1;
			this.btnHelp1Cancel.Text = "Cancel";
			this.btnHelp1Cancel.UseVisualStyleBackColor = true;
			this.btnHelp1Cancel.Click += new System.EventHandler(this.btnHelp1Cancel_Click);
			// 
			// btnHelp1OK
			// 
			this.btnHelp1OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp1OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnHelp1OK.Location = new System.Drawing.Point(441, 4);
			this.btnHelp1OK.Name = "btnHelp1OK";
			this.btnHelp1OK.Size = new System.Drawing.Size(75, 23);
			this.btnHelp1OK.TabIndex = 0;
			this.btnHelp1OK.Text = "&OK";
			this.btnHelp1OK.UseVisualStyleBackColor = true;
			this.btnHelp1OK.Click += new System.EventHandler(this.btnHelp1OK_Click);
			// 
			// rtfHelpText
			// 
			this.rtfHelpText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rtfHelpText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtfHelpText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtfHelpText.Location = new System.Drawing.Point(0, 0);
			this.rtfHelpText.Name = "rtfHelpText";
			this.rtfHelpText.Size = new System.Drawing.Size(526, 421);
			this.rtfHelpText.TabIndex = 0;
			this.rtfHelpText.Text = "";
			// 
			// pnlHelp1
			// 
			this.pnlHelp1.Controls.Add(this.rtfHelpText);
			this.pnlHelp1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlHelp1.Location = new System.Drawing.Point(0, 0);
			this.pnlHelp1.Name = "pnlHelp1";
			this.pnlHelp1.Size = new System.Drawing.Size(526, 421);
			this.pnlHelp1.TabIndex = 2;
			// 
			// pnlHelpActions
			// 
			this.pnlHelpActions.AutoSize = true;
			this.pnlHelpActions.Controls.Add(this.btnHelp1Cancel);
			this.pnlHelpActions.Controls.Add(this.btnHelp1OK);
			this.pnlHelpActions.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlHelpActions.Location = new System.Drawing.Point(0, 421);
			this.pnlHelpActions.Name = "pnlHelpActions";
			this.pnlHelpActions.Size = new System.Drawing.Size(526, 31);
			this.pnlHelpActions.TabIndex = 0;
			// 
			// FormRtfHelp
			// 
			this.AcceptButton = this.btnHelp1OK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnHelp1Cancel;
			this.ClientSize = new System.Drawing.Size(526, 452);
			this.Controls.Add(this.pnlHelp1);
			this.Controls.Add(this.pnlHelpActions);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRtfHelp";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "RED Help";
			this.Load += new System.EventHandler(this.FormRedMatchHelp_Load);
			this.Shown += new System.EventHandler(this.FormRtfHelp_Shown);
			this.pnlHelp1.ResumeLayout(false);
			this.pnlHelpActions.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnHelp1OK;
        private System.Windows.Forms.Button btnHelp1Cancel;
        private System.Windows.Forms.RichTextBox rtfHelpText;
        private System.Windows.Forms.Panel pnlHelp1;
        private System.Windows.Forms.Panel pnlHelpActions;
    }
}