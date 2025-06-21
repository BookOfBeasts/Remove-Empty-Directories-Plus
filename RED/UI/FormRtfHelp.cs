using System;
using System.IO;
using System.Windows.Forms;

namespace RED.UI
{
	public partial class FormRtfHelp : Form
	{
		public FormRtfHelp()
		{
			InitializeComponent();
		}

		public string Title
		{
            get { return _Title; }
			set
			{
				_Title = value;
				this.Text = "RED Help: " + _Title;
			}
		}

		private string _Title;
		
        public string HelpText { get; set; }

		private void FormRedMatchHelp_Load(object sender, EventArgs e)
		{
			this.Icon = Properties.Resources.iconProject;
			// Ensure the Cancel button is 'off screen'
			// Allows use of in-built ESC = Cancel to close the form without any special handlers
			this.btnHelp1Cancel.Left = this.btnHelp1Cancel.Width * -2;
		}

		private void FormRtfHelp_Shown(object sender, EventArgs e)
		{
			LoadRtfFromHelpText();
		}

		private void btnHelp1OK_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnHelp1Cancel_Click(object sender, EventArgs e)
		{
#if DEBUG
			rtfHelpText.SaveFile($"!{Title}_HelpText.rtf", RichTextBoxStreamType.RichText);
			File.WriteAllText($"!{Title}_HelpText.txt", HelpText);
#endif
		}

		private void LoadRtfFromHelpText()
		{
			using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(HelpText)))
			{
				rtfHelpText.LoadFile(stream, RichTextBoxStreamType.RichText);
			}
		}
	}
}