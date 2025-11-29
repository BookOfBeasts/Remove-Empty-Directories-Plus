using System;
using System.IO;
using System.Windows.Forms;
using RED.Helper;
using RED.Match;
using RED.UI;
using SecondLanguage;

namespace RED
{
	internal static class RedGetText
	{
		internal static readonly string Lf1 = "\n";
		internal static readonly string CrLf1 = "\r\n";
		internal static readonly string CrLf2 = "\r\n\r\n";
		internal static readonly string LanguageDefault = "--Default--";

		internal static Translator GT = Translator.Default;

		internal static string Translate(string id, params object[] args)
		{
			if (!string.IsNullOrWhiteSpace(id))
			{
				return GT.Translate(id, args);
			}
			return id;
		}

		internal static string Translate(string id)
		{
			if (!string.IsNullOrWhiteSpace(id))
			{
				if (id.Contains("\r\n"))
				{
					return TranslateML(id);
				}
				else
				{
					return GT.Translate(id);
				}
			}
			return id;
		}

		private static string TranslateML(string id)
		{
			string x = GT.Translate(id.Replace(CrLf1, Lf1));
			return x.Replace(Lf1, CrLf1);
		}

		internal static string GetLanguageFolder(string baseFolder)
		{
			return Path.Combine(baseFolder, "language");
		}

		internal static string GetLanguageFile(string language, string baseFolder)
		{
			return Path.Combine(GetLanguageFolder(baseFolder), language + ".po");
		}

		internal static bool LoadLanguage(string language, string baseFolder)
		{
			bool respx = false;

			if (!string.IsNullOrWhiteSpace(language) && language != RedGetText.LanguageDefault)
			{
				string poFilename = GetLanguageFile(language, baseFolder);
				if (File.Exists(poFilename))
				{
					if (GT.TryRegisterTranslation(poFilename))
					{
						GT.ClearTranslationList();
						GT.TryRegisterTranslation(poFilename);
						respx = true;
					}
				}
			}

			if (!respx)
			{
				// Reset to use the default language (English)
				GT.ClearTranslationList();
			}

			return respx;
		}

		internal static class Red
		{
			internal static string Title => GT.Translate("Remove Empty Directories +");
			internal static string CaptionError => GT.Translate("RED+ Error");
			internal static string CaptionInfo => GT.Translate("RED+");
		}

		internal static class Words
		{
			// Single Words (ish!)

			internal static string AdminMode => GT.Translate("Admin mode");
			internal static string Busy => GT.Translate("Busy");
			internal static string Deleted => GT.Translate("Deleted"); internal static string Empty => GT.Translate("Empty");
			internal static string Error => GT.Translate("Error");
			internal static string Exit => GT.Translate("Exit");
			internal static string Failed => GT.Translate("Failed");
			internal static string Hidden => GT.Translate("Hidden");
			internal static string Locked => GT.Translate("Locked");
			internal static string NeverEmpty => GT.Translate("Never Empty");
			internal static string Protected => GT.Translate("Protected");
			internal static string Ready => GT.Translate("Ready");
			internal static string Root => GT.Translate("Root");

			internal static string Unknown()
			{ return GT.Translate("Unknown"); }

			// Phrases

			internal static string ContainsTrash => GT.Translate("Contains 'Trash'");
			internal static string DeletedSoFar => GT.Translate("Empty Directories Deleted So Far");
			internal static string ErorrUnknown => GT.Translate("Unknown Error");

			internal static string ErrorMessage1(string emsg)
			{ return GT.Translate("Error Message: {0}", emsg); }

			internal static string ErrorUnknownDeleteMode(DeleteModes deleteMode)
			{ return GT.Translate("Internal error: Unknown delete mode: {0}", deleteMode.ToString()); }

			internal static string EventHandlerMissing => GT.Translate("Internal error: event handler is missing");
			internal static string RestartMaybe => GT.Translate("You may need to restart the application for this to take full effect");
			internal static string RestartRequired => GT.Translate("You will need to restart the application for this to take full effect");
		}

		internal static string MatchMethodDescription(RedMatchMethodType matchMethod)
		{
			switch (matchMethod)
			{
				case RedMatchMethodType.Invalid: return GT.Translate("Invalid: No Match");
				case RedMatchMethodType.Contains: return GT.Translate("Contains");
				case RedMatchMethodType.Endswith: return GT.Translate("Endswith");
				case RedMatchMethodType.Startwith: return GT.Translate("Startswith");
				case RedMatchMethodType.NameExact: return GT.Translate("Name (Exact)");
				case RedMatchMethodType.NameExactWithPath: return GT.Translate("Path (Exact)");
				case RedMatchMethodType.RegExName: return GT.Translate("RegEx (Name)");
				case RedMatchMethodType.RegExPath: return GT.Translate("RegEx (Path)");
				default: return RedGetText.Words.Unknown();
			}
		}

		internal static class UI
		{
			internal static void TranslateControls(Control parent)
			{
				try
				{
					foreach (Control c in parent.Controls)
					{
						if (c is UCMenuButton)
						{
							TranslateContextMenuStrip((c as UCMenuButton).Menu);
						}
						if (c is ToolStrip)
						{
							TranslateToolStripItemCollection((c as ToolStrip).Items);
						}
						if (c is TreeView)
						{
							TranslateContextMenuStrip((c as TreeView).ContextMenuStrip);
						}
						if (c is DataGridView)
						{
							TranslateDataGridView(c as DataGridView);
						}

						c.Text = Translate(c.Text);
						if (c is TabPage)
						{
							TabPage o = c as TabPage;
							o.ToolTipText = Translate(o.ToolTipText);
						}

						if (c.Controls.Count > 0)
						{
							TranslateControls(c);
						}
					}
				}
				catch (Exception ex)
				{
					UiAssist.MsgBoxException(parent, "UiLanguageTranslate()", ex);
				}
			}

			private static void TranslateDataGridView(DataGridView o)
			{
				if (o != null)
				{
					foreach (DataGridViewColumn item in o.Columns)
					{
						item.HeaderText = Translate(item.HeaderText);
						item.ToolTipText = Translate(item.ToolTipText);
					}
				}
			}

			private static void TranslateContextMenuStrip(ContextMenuStrip o)
			{
				if (o != null && o.Items.Count > 0)
				{
					TranslateToolStripItemCollection(o.Items);
				}
			}

			private static void TranslateToolStripItemCollection(ToolStripItemCollection items)
			{
				foreach (ToolStripItem item in items)
				{
					item.Text = Translate(item.Text);
					item.ToolTipText = Translate(item.ToolTipText);
					if (item is ToolStripDropDownButton)
					{
						TranslateToolStripItemCollection((item as ToolStripDropDownButton).DropDownItems);
					}
				}
			}
		}
	}
}