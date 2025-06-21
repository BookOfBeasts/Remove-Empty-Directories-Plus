using System;
using System.Collections.Generic;
using System.ComponentModel;
using Alphaleonis.Win32.Filesystem;
using RED.Helper;
using FileAttributes = System.IO.FileAttributes;
using TXT = RED.RedGetText;

namespace RED
{
	/// <summary>
	/// Searches for empty directories
	/// </summary>
	public class FindEmptyDirectoryWorker : BackgroundWorker
	{
		private int folderCount = 0;

		public int FolderCount
		{
			get { return folderCount; }
		}

		public RuntimeData RunData { get; set; }

		public DeletionErrorEventArgs ErrorInfo { get; set; }

		public int PossibleEndlessLoop { get; set; }

		public FindEmptyDirectoryWorker()
		{
			WorkerReportsProgress = true;
			WorkerSupportsCancellation = true;
		}

		protected override void OnDoWork(DoWorkEventArgs e)
		{
			DirectoryInfo startFolder = (DirectoryInfo)e.Argument;

			this.PossibleEndlessLoop = 0;
			this.RunData.ScanResults.Clear();

			try
			{
				DirectorySearchStatusTypes rootStatusType = this.CheckIfDirectoryEmpty(startFolder, 1);

				this.ReportProgress(0, new FoundEmptyDirInfoEventArgs(startFolder, rootStatusType));

				if (this.PossibleEndlessLoop > this.RunData.InfiniteLoopDetectionCount)
				{
					string emsg = TXT.Translate("Detected possible infinite - loop somewhere in the target path {0} (symbolic links can cause this)", RedAssist.DQuote(startFolder.FullName));
					this.RunData.AddLogMessage(emsg);
					throw new Exception(emsg);
				}
			}
			catch (Exception ex)
			{
				e.Cancel = true;
				this.RunData.AddLogMessage(TXT.Translate("An error occurred during the scan process: " + ex.Message));
				this.ErrorInfo = new DeletionErrorEventArgs(startFolder.FullName, ex.Message);
				return;
			}

			if (CancellationPending)
			{
				this.RunData.AddLogMessage(TXT.Translate("Scan process was cancelled"));
				e.Cancel = true;
				e.Result = 0;
				return;
			}

			e.Result = 1;
		}

		private DirectorySearchStatusTypes CheckIfDirectoryEmpty(DirectoryInfo startDir, int depth)
		{
			if (this.PossibleEndlessLoop > this.RunData.InfiniteLoopDetectionCount)
			{
				this.ReportProgress(0, new FoundEmptyDirInfoEventArgs(startDir, DirectorySearchStatusTypes.Error, TXT.Translate("Aborted - possible infinite-loop detected")));
				return DirectorySearchStatusTypes.Error;
			}

			try
			{
				// Thread.Sleep(500); -> ?

				if (this.RunData.MaxDepth != -1 && depth > this.RunData.MaxDepth)
				{
					return DirectorySearchStatusTypes.NotEmpty;
				}

				// Cancel process if the user hits stop
				if (CancellationPending)
				{
					return DirectorySearchStatusTypes.NotEmpty;
				}

				this.folderCount++;

				// update status progress bar after 100 steps:
				if (this.folderCount % 100 == 0)
				{
					this.ReportProgress(folderCount, TXT.Translate("Checking directory: {0}", startDir.Name));
				}

				bool containsFiles = false;

				// NotBob - If this directory is on the NeverEmpty list treat it as if it contains files
				if (this.RunData.NeverEmptyDirectoryList.IsOnList(startDir))
				{
					containsFiles = true;
					string msg = TXT.Translate("Directory is on the NeverEmpty list: {0}", RedAssist.DQuote(startDir.FullName));
					this.RunData.AddLogMessage(msg);
					if (!this.RunData.HideIgnoredDirectories)
					{
						this.ReportProgress(0, new FoundEmptyDirInfoEventArgs(startDir, DirectorySearchStatusTypes.NeverEmpty, msg));
					}
				}

				if (!containsFiles)
				{
					// Get file list
					FileInfo[] fileList = null;

					// some directories could trigger an exception:
					try
					{
						fileList = startDir.GetFiles();
					}
					catch
					{
						fileList = null;
					}

					if (fileList == null)
					{
						// if containsFiles is true then the folder does not get deleted:
						containsFiles = true; // secure way
						this.RunData.AddLogMessage(TXT.Translate("Failed to access files in {0}", RedAssist.DQuote(startDir.FullName)));
						this.ReportProgress(0, new FoundEmptyDirInfoEventArgs(startDir, DirectorySearchStatusTypes.Error, TXT.Translate("Failed to access files")));
					}
					else if (fileList.Length == 0)
					{
						containsFiles = false;
					}
					else
					{
						string delPattern = string.Empty;

						// loop trough files and cancel if containsFiles == true
						for (int f = 0; (f < fileList.Length && !containsFiles); f++)
						{
							FileInfo file = null;
							int filesize = 0;

							try
							{
								file = fileList[f];
								filesize = (int)file.Length;
							}
							catch
							{
								// keep folder if there is a strange file that triggers an exception:
								containsFiles = true;
								break;
							}

							// It only takes one file to be found to stop the scan
							if (!this.RunData.IgnoreFileNameList.IsOnList(file, filesize, RunData.IgnoreEmptyFiles, out delPattern))
							{
								containsFiles = true;
							}
						}
					}
				}

				List<DirectoryInfo> subFolderList = new List<DirectoryInfo>();
				try
				{
					subFolderList.AddRange(startDir.GetDirectories());
				}
				catch
				{
					// If we can not read the folder -> don't delete it:
					this.RunData.AddLogMessage(TXT.Translate("Failed to access subdirectories in {0}", RedAssist.DQuote(startDir.FullName)));
					this.ReportProgress(0, new FoundEmptyDirInfoEventArgs(startDir, DirectorySearchStatusTypes.Error, TXT.Translate("Failed to access subdirectories")));
					return DirectorySearchStatusTypes.Error;
				}

				// The folder is empty, break here:
				if (!containsFiles && subFolderList.Count == 0)
				{
					return DirectorySearchStatusTypes.Empty;
				}

				bool allSubDirectoriesEmpty = true;

				// NotBob - sort subfolders to give a more 'natural' order to the displayed results
				subFolderList.Sort((x, y) => x.Name.CompareTo(y.Name));

				foreach (DirectoryInfo curDir in subFolderList)
				{
					FileAttributes attribs = curDir.Attributes;

					bool ignoreSystemDir = (this.RunData.IgnoreSystemFolders && ((attribs & FileAttributes.System) == FileAttributes.System));
					bool ignoreHiddenDir = (this.RunData.IgnoreHiddenFolders && ((attribs & FileAttributes.Hidden) == FileAttributes.Hidden));

					bool ignoreSubDirectory = (ignoreSystemDir || ignoreHiddenDir);

					if (!ignoreSubDirectory && this.RunData.IgnoreDirectoryNameList.IsOnList(curDir))
					{
						this.RunData.AddLogMessage(TXT.Translate("Aborted scan of {0} because it is on the ignore list", RedAssist.DQuote(curDir.FullName)));
						ignoreSubDirectory = true;
						// NotBob - option to exclude ignored directories from the scan window
						if (!this.RunData.HideIgnoredDirectories)
						{
							this.ReportProgress(0, new FoundEmptyDirInfoEventArgs(curDir, DirectorySearchStatusTypes.Ignore));
						}
					}

					if (!ignoreSubDirectory && (attribs & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint)
					{
						this.RunData.AddLogMessage(TXT.Translate("Aborted scan of {0} because it is a symbolic link", RedAssist.DQuote(curDir.FullName)));
						this.ReportProgress(0, new FoundEmptyDirInfoEventArgs(curDir, DirectorySearchStatusTypes.Error, TXT.Translate("Aborted because directory is a symbolic link")));
						ignoreSubDirectory = true;
					}

					// TODO: Implement more checks
					//else if ((attribs & FileAttributes.Device) == FileAttributes.Device) msg = "Device - Aborted - found";
					//else if ((attribs & FileAttributes.Encrypted) == FileAttributes.Encrypted) msg = "Encrypted -  found";
					// The file will not be indexed by the operating system's content indexing service.
					// else if ((attribs & FileAttributes.NotContentIndexed) == FileAttributes.NotContentIndexed) msg = "NotContentIndexed - Device found";
					//else if ((attribs & FileAttributes.Offline) == FileAttributes.Offline) msg = "Offline -  found";
					//else if ((attribs & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) msg = "ReadOnly -  found";
					//else if ((attribs & FileAttributes.Temporary) == FileAttributes.Temporary) msg = "Temporary -  found";

					// Scan sub folder:
					DirectorySearchStatusTypes subFolderStatus = DirectorySearchStatusTypes.NotEmpty;

					if (!ignoreSubDirectory)
					{
						// JRS ADDED check for AGE of folder
						if (curDir.CreationTime.AddHours(this.RunData.MinFolderAgeHours) < DateTime.Now)
						{
							subFolderStatus = this.CheckIfDirectoryEmpty(curDir, depth + 1);
						}
						else
						{
							string fmt = string.Format(TXT.Translate("Directory {0} skipped because creation time [{1}] is < {2} hours old"));
							this.RunData.AddLogMessage(string.Format(fmt, RedAssist.DQuote(curDir.FullName), curDir.CreationTime.ToString(), this.RunData.MinFolderAgeHours.ToString()));
						}

						// Report status to the GUI
						if (subFolderStatus == DirectorySearchStatusTypes.Empty)
						{
							this.ReportProgress(0, new FoundEmptyDirInfoEventArgs(curDir, subFolderStatus));
						}
					}

					// this folder is not empty:
					if (subFolderStatus != DirectorySearchStatusTypes.Empty || ignoreSubDirectory)
					{
						allSubDirectoriesEmpty = false;
					}
				}

				// All subdirectories are empty
				return (allSubDirectoriesEmpty && !containsFiles) ? DirectorySearchStatusTypes.Empty : DirectorySearchStatusTypes.NotEmpty;
			}
			catch (Exception ex)
			{
				// Error handling
				if (ex is System.IO.PathTooLongException)
				{
					this.PossibleEndlessLoop++;
				}
				this.RunData.AddLogMessage(TXT.Translate("An unknown error occurred while trying to scan directory: {0} - {1}", RedAssist.DQuote(startDir.FullName), ex.Message));
				this.ReportProgress(0, new FoundEmptyDirInfoEventArgs(startDir, DirectorySearchStatusTypes.Error, ex.Message));
				return DirectorySearchStatusTypes.Error;
			}
		}
	}
}