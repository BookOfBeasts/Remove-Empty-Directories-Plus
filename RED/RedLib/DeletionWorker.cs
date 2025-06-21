using System;
using System.ComponentModel;
using System.Threading;
using Alphaleonis.Win32.Filesystem;
using RED.Helper;
using TXT = RED.RedGetText;

namespace RED
{
	/// <summary>
	/// Deletes the empty directories RED found
	/// </summary>
	public class DeletionWorker : BackgroundWorker
	{
		public RuntimeData Data { get; set; }

		public int DeletedCount { get; set; }
		public int FailedCount { get; set; }
		public int ProtectedCount { get; set; }

		public int ListPos { get; set; }

		public DeletionErrorEventArgs ErrorInfo { get; set; }

		public DeletionWorker()
		{
			WorkerReportsProgress = true;
			WorkerSupportsCancellation = true;

			this.ListPos = 0;
		}

		protected override void OnDoWork(DoWorkEventArgs e)
		{
			// This method will run on a thread other than the UI thread.
			// Be sure not to manipulate any Windows Forms controls created
			// on the UI thread from this method.

			if (CancellationPending)
			{
				e.Cancel = true;
				return;
			}

			bool stopNow = false;
			string errorMessage = string.Empty;
			this.ErrorInfo = null;

			while (this.ListPos < this.Data.ScanResults.Count)
			{
				if (CancellationPending)
				{
					e.Cancel = true;
					return;
				}

				DirectoryDeletionStatusTypes status = DirectoryDeletionStatusTypes.Ignored;
				Match.RedScanResultItem scanResult = this.Data.ScanResults[this.ListPos];

				// Do not delete one time protected folders
				if (!this.Data.ProtectedFolderList.ContainsKey(scanResult.FullPath))
				{
					try
					{
						// Try to delete the directory
						this.secureDelete(scanResult.Directory);
						this.Data.AddLogMessage(TXT.Translate("Successfully deleted directory: {0}", RedAssist.DQuote(scanResult.FullPath)));
						status = DirectoryDeletionStatusTypes.Deleted;
						this.DeletedCount++;
					}
					catch (REDPermissionDeniedException ex)
					{
						errorMessage = ex.Message;
						this.Data.AddLogMessage(TXT.Translate("Directory is protected by the system: {0} - {1}", RedAssist.DQuote(scanResult.FullPath), RedGetText.Words.ErrorMessage1(errorMessage)));
						status = DirectoryDeletionStatusTypes.Protected;
						this.ProtectedCount++;
					}
					catch (Exception ex)
					{
						errorMessage = ex.Message;
						stopNow = (!this.Data.HideDeletionErrors);
						this.Data.AddLogMessage(TXT.Translate("Failed to delete directory: {0} - {1}", RedAssist.DQuote(scanResult.FullPath), RedGetText.Words.ErrorMessage1(errorMessage)));
						status = DirectoryDeletionStatusTypes.Warning;
						this.FailedCount++;
					}

					if (!stopNow && this.Data.PauseTime > 0)
					{
						Thread.Sleep(TimeSpan.FromMilliseconds(this.Data.PauseTime));
					}
				}
				else
				{
					status = DirectoryDeletionStatusTypes.Protected;
				}

				this.ReportProgress(1, new DeleteProcessUpdateEventArgs(this.ListPos, scanResult, status, this.Data.ScanResults.Count));

				this.ListPos++;

				if (stopNow)
				{
					// stop here for now
					if (string.IsNullOrWhiteSpace(errorMessage))
					{
						errorMessage = TXT.Translate(RedGetText.Words.ErorrUnknown);
					}

					e.Cancel = true;
					this.ErrorInfo = new DeletionErrorEventArgs(scanResult.FullPath, errorMessage);
					return;
				}
			}

			e.Result = this.Data.ScanResults.Count;
		}

		private void secureDelete(DirectoryInfo emptyDirectory)
		{
			//var emptyDirectory = new DirectoryInfo(path);

			if (!emptyDirectory.Exists)
			{
				throw new Exception(TXT.Translate("Could not delete the directory because it does not exist anymore: {0}", RedAssist.DQuote(emptyDirectory.FullName)));
			}

			// Cleanup folder

			//String[] ignoreFileList = this.Data.GetIgnoreFileList();

			FileInfo[] Files = emptyDirectory.GetFiles();

			if (Files != null && Files.Length != 0)
			{
				// loop trough files and cancel if containsFiles == true
				for (int f = 0; f < Files.Length; f++)
				{
					FileInfo file = Files[f];

					string delPattern;
					bool deleteTrashFile = this.Data.IgnoreFileNameList.IsOnList(file, (int)file.Length, this.Data.IgnoreEmptyFiles, out delPattern);

					// If only one file is good, then stop.
					if (deleteTrashFile)
					{
						try
						{
							SystemFunctions.SecureDeleteFile(file, this.Data.DeleteMode);
							this.Data.AddLogMessage(TXT.Translate("-> Successfully deleted file {0} because it matched the ignore pattern {1}", RedAssist.DQuote(file.FullName), RedAssist.DQuote(delPattern)));
						}
						catch (Exception ex)
						{
							string msg = TXT.Translate("Could not delete this empty (trash) file: {0}", RedAssist.DQuote(file.FullName));
							this.Data.AddLogMessage(msg + RedGetText.Words.ErrorMessage1(ex.Message));
							msg = msg + RedGetText.CrLf2 + RedGetText.Words.ErrorMessage1(ex.Message);
							if (ex is REDPermissionDeniedException)
							{
								throw new REDPermissionDeniedException(msg, ex);
							}
							else
							{
								throw new Exception(msg, ex);
							}
						}
					}
				}
			}

			// End cleanup

			// This function will ensure that the directory is really empty before it gets deleted
			SystemFunctions.SecureDeleteDirectory(emptyDirectory.FullName, this.Data.DeleteMode);
		}
	}
}