using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RED.Config;
using RED.Helper;

namespace RED.UI
{
	public partial class FormLanguage : Form
	{
		public FormLanguage(RedConfiguration config)
		{
			InitializeComponent();
			Config = config;
		}

		public string Language { get; private set; }

		private RedConfiguration Config;

		private void FormLanguage_Load(object sender, System.EventArgs e)
		{
			Populate();
		}

		private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
		{
			Language = cboLanguage.Items[cboLanguage.SelectedIndex].ToString();
		}

		private void Populate()
		{
			try
			{
				string langPath = RedGetText.GetLanguageFolder(Config.Runtime.ExecutablePath);
				cboLanguage.Items.Clear();
				cboLanguage.Items.Add(RedGetText.LanguageDefault);
				List<string> poFiles = Directory.GetFiles(langPath, "*.po").ToList();
				poFiles.Sort();
				foreach (string poFile in poFiles)
				{
					string langName = Path.GetFileNameWithoutExtension(poFile);
					cboLanguage.Items.Add(langName);
				}

				cboLanguage.SelectedIndex = 0;
				for (int i = 0; i < cboLanguage.Items.Count; i++)
				{
					if (cboLanguage.Items[i].ToString() == Config.Options.Language)
					{
						cboLanguage.SelectedIndex = i;
						break;
					}
				}
			}
			catch (Exception ex)
			{
				UiAssist.MsgBoxException(this, "RED+ Language", ex);
			}
		}
	}
}