using System;
using System.Diagnostics;
using System.Security.Permissions;
using System.Windows.Forms;
using Alphaleonis.Win32.Filesystem;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using RED.Helper;
using FileAccess = System.IO.FileAccess;
using FileMode = System.IO.FileMode;
using FileShare = System.IO.FileShare;
using TXT = RED.RedGetText;

namespace RED
{
	public enum DeleteModes
	{
		RecycleBin = 0,
		RecycleBinShowErrors = 1,
		RecycleBinWithQuestion = 2,
		Direct = 3,
		Simulate = 4
	}

	[Serializable]
	public class REDPermissionDeniedException : Exception
	{
		public REDPermissionDeniedException()
		{ }

		public REDPermissionDeniedException(string message) : base(message) { }

		public REDPermissionDeniedException(string message, Exception inner) : base(message, inner) { }
	}

	/// <summary>
	/// A collection of (generic) system functions
	///
	/// Exception handling should be made by the caller
	/// </summary>
	public class SystemFunctions
	{
		// Registry keys
		private const string registryMenuName = "Folder\\shell\\Remove Empty Dirs";

		private const string registryCommand = "Folder\\shell\\Remove Empty Dirs\\command";

		public static void ManuallyDeleteDirectory(string path, DeleteModes deleteMode)
		{
			if (deleteMode == DeleteModes.Simulate)
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(path))
			{
				throw new Exception(TXT.Translate("Could not delete directory because the path was empty"));
			}

			//TODO: Add FileIOPermission code?

			FileSystem.DeleteDirectory(path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
		}

		public static bool IsDirLocked(string path)
		{
			try
			{
				// UGLY hack to determine whether we have write access
				// to a specific directory

				Random r = new Random();
				string tempName = path + "deltest";

				int counter = 0;
				while (Directory.Exists(tempName))
				{
					tempName = path + "deltest" + r.Next(0, 9999).ToString();
					if (counter > 100)
					{
						return true; // Something strange is going on... stop here...
					}

					counter++;
				}

				Directory.Move(path, tempName);
				Directory.Move(tempName, path);

				return false;
			}
			catch //(Exception ex)
			{
				// Could not rename -> probably we have no
				// write access to the directory
				return true;
			}
		}

		public static bool IsFileLocked(FileInfo file)
		{
			try
			{
				using (file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None))
				{
					return false;
				}
			}
			catch //(IOException)
			{
				// Could not open file -> probably we have no
				// write access to the file
				return true;
			}
		}

		public static void SecureDeleteDirectory(string path, DeleteModes deleteMode)
		{
			if (deleteMode == DeleteModes.Simulate)
			{
				return;
			}

			if (deleteMode == DeleteModes.Direct)
			{
				Directory.Delete(path, recursive: false, ignoreReadOnly: true); //throws IOException if not empty anymore
				return;
			}

			// Last security check before deletion
			if (Directory.GetFiles(path).Length == 0 && Directory.GetDirectories(path).Length == 0)
			{
				if (deleteMode == DeleteModes.RecycleBin)
				{
					// Check CLR permissions -> could raise a exception
					new FileIOPermission(FileIOPermissionAccess.Write, path + Path.DirectorySeparatorChar.ToString()).Demand();

					if (IsDirLocked(path))
					{
						throw new REDPermissionDeniedException(TXT.Translate("Could not delete directory because the access is protected by the (file) system (permission denied): {0}", RedAssist.DQuote(path)));
					}

					FileSystem.DeleteDirectory(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
				}
				else if (deleteMode == DeleteModes.RecycleBinShowErrors)
				{
					FileSystem.DeleteDirectory(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
				}
				else if (deleteMode == DeleteModes.RecycleBinWithQuestion)
				{
					FileSystem.DeleteDirectory(path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
				}
				else
				{
					throw new Exception(RedGetText.Words.ErrorUnknownDeleteMode(deleteMode));
				}
			}
			else
			{
				throw new Exception(TXT.Translate("Aborted deletion of the directory because it is no longer empty. This can happen if RED previously failed to delete an empty (trash) file: {0}", RedAssist.DQuote(path)));
			}
		}

		public static void SecureDeleteFile(FileInfo file, DeleteModes deleteMode)
		{
			if (deleteMode == DeleteModes.Simulate)
			{
				return;
			}

			if (deleteMode == DeleteModes.RecycleBin)
			{
				// Check CLR permissions -> could raise a exception
				new FileIOPermission(FileIOPermissionAccess.Write, file.FullName).Demand();

				if (IsFileLocked(file))
				{
					throw new REDPermissionDeniedException(TXT.Translate("Could not delete file because the access is protected by the (file) system (permission denied): {0}", RedAssist.DQuote(file.FullName)));
				}

				FileSystem.DeleteFile(file.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
			}
			else if (deleteMode == DeleteModes.RecycleBinShowErrors)
			{
				FileSystem.DeleteFile(file.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
			}
			else if (deleteMode == DeleteModes.RecycleBinWithQuestion)
			{
				FileSystem.DeleteFile(file.FullName, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
			}
			else if (deleteMode == DeleteModes.Direct)
			{
				// Was used for testing the error handling:
				// if (SystemFunctions.random.NextDouble() > 0.5) throw new Exception("Test error");
				file.Delete(ignoreReadOnly: true);
			}
			else
			{
				throw new Exception(RedGetText.Words.ErrorUnknownDeleteMode(deleteMode));
			}
		}

		public static string ChooseDirectoryDialog(string path)
		{
			FolderBrowserDialog folderDialog = new FolderBrowserDialog();

			folderDialog.Description = TXT.Translate("Please select the directory that you want to be cleaned");
			folderDialog.ShowNewFolderButton = false;

			if (!string.IsNullOrWhiteSpace(path))
			{
				DirectoryInfo dir = new DirectoryInfo(path);

				if (dir.Exists)
				{
					folderDialog.SelectedPath = path;
				}
			}

			if (folderDialog.ShowDialog() == DialogResult.OK)
			{
				path = folderDialog.SelectedPath;
			}

			folderDialog.Dispose();

			return path;
		}

		/// <summary>
		/// Opens a folder
		/// </summary>
		public static void OpenDirectoryWithExplorer(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				return;
			}

			string exe = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "explorer.exe");

			Process.Start(exe, string.Format("/e,{0}", RedAssist.DQuote(path)));
		}

		/// <summary>
		/// Check for the registry key
		/// </summary>
		/// <returns></returns>
		public static bool IsRegKeyIntegratedIntoWindowsExplorer()
		{
			return (Registry.ClassesRoot.OpenSubKey(registryMenuName) != null);
		}

		internal static void AddOrRemoveRegKey(bool add)
		{
			RegistryKey regmenu = null;
			RegistryKey regcmd = null;

			if (add)
			{
				try
				{
					regmenu = Registry.ClassesRoot.CreateSubKey(registryMenuName);

					if (regmenu != null)
					{
						regmenu.SetValue("", TXT.Red.Title);
						regmenu.SetValue("Icon", Application.ExecutablePath + ",0");
					}
					regcmd = Registry.ClassesRoot.CreateSubKey(registryCommand);

					if (regcmd != null)
					{
						regcmd.SetValue("", string.Format("{0} {1}", Application.ExecutablePath, RedAssist.DQuote("%1")));
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
				finally
				{
					if (regmenu != null)
					{
						regmenu.Close();
					}

					if (regcmd != null)
					{
						regcmd.Close();
					}
				}
			}
			else
			{
				try
				{
					RegistryKey reg = Registry.ClassesRoot.OpenSubKey(registryCommand);

					if (reg != null)
					{
						reg.Close();
						Registry.ClassesRoot.DeleteSubKey(registryCommand);
					}
					reg = Registry.ClassesRoot.OpenSubKey(registryMenuName);
					if (reg != null)
					{
						reg.Close();
						Registry.ClassesRoot.DeleteSubKey(registryMenuName);
					}
				}
				catch (Exception ex)
				{
					UiAssist.MsgBoxError(TXT.Words.Error + RedGetText.CrLf1 + TXT.Translate("Could not change registry settings:") + RedGetText.CrLf2 + ex.ToString());
				}
			}
		}
	}
}