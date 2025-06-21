using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RED
{
	/// <summary>
	/// RED core class, handles events and communicates with the GUI
	/// </summary>
	public class REDCore
	{
		public WorkflowSteps CurrentProcessStep = WorkflowSteps.Idle;
		private readonly RuntimeData RunData = null;

		// Workers
		private FindEmptyDirectoryWorker SearchEmptyFoldersWorker = null;

		private DeletionWorker DeletionWorker = null;

		// Events
		public event EventHandler<ErrorEventArgs> OnError;

		public event EventHandler OnCancelled;

		public event EventHandler OnAborted;

		public event EventHandler<ProgressChangedEventArgs> OnProgressChanged;

		public event EventHandler<FoundEmptyDirInfoEventArgs> OnFoundEmptyDirectory;

		public event EventHandler<FinishedScanForEmptyDirsEventArgs> OnFinishedScanForEmptyDirs;

		public event EventHandler<DeleteProcessUpdateEventArgs> OnDeleteProcessChanged;

		public event EventHandler<DeletionErrorEventArgs> OnDeleteError;

		public event EventHandler<DeleteProcessFinishedEventArgs> OnDeleteProcessFinished;

		public REDCore(RuntimeData data)
		{
			RunData = data;
		}

		/// <summary>
		/// Start searching empty folders
		/// </summary>
		public void SearchingForEmptyDirectories()
		{
			CurrentProcessStep = WorkflowSteps.StartSearchingForEmptyDirs;

			// Rest folder list
			RunData.ProtectedFolderList = new Dictionary<string, bool>();

			// Start async empty directory search worker
			SearchEmptyFoldersWorker = new FindEmptyDirectoryWorker();
			SearchEmptyFoldersWorker.RunData = RunData;
			SearchEmptyFoldersWorker.ProgressChanged += new ProgressChangedEventHandler(SearchEmptyFoldersWorker_ProgressChanged);
			SearchEmptyFoldersWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(SearchEmptyFoldersWorker_RunWorkerCompleted);
			SearchEmptyFoldersWorker.RunWorkerAsync(RunData.StartFolder);
		}

		/// <summary>
		/// This function gets called on a status update of the find worker
		/// </summary>
		private void SearchEmptyFoldersWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (e.UserState is FoundEmptyDirInfoEventArgs info)
			{
				if (info.ScanResult.SearchStatus == DirectorySearchStatusTypes.Empty)
				{
					// Found an empty dir, add it to the list
					RunData.ScanResults.AddItem(info.ScanResult);
				}
				else if (info.ScanResult.SearchStatus == DirectorySearchStatusTypes.Error && RunData.HideScanErrors)
				{
					return;
				}

				OnFoundEmptyDirectory?.Invoke(this, info);
			}
			else if (e.UserState is string userstate)
			{
				OnProgressChanged?.Invoke(this, new ProgressChangedEventArgs(0, userstate));
			}
			else
			{
				// TODO: Handle unknown types
			}
		}

		private void SearchEmptyFoldersWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			CurrentProcessStep = WorkflowSteps.Idle;

			if (e.Error != null)
			{
				SearchEmptyFoldersWorker.Dispose(); SearchEmptyFoldersWorker = null;
				ShowErrorMsg(e.Error.Message);
			}
			else if (e.Cancelled)
			{
				if (SearchEmptyFoldersWorker.ErrorInfo != null)
				{
					// A error occurred, process was stopped
					ShowErrorMsg(SearchEmptyFoldersWorker.ErrorInfo.ErrorMessage);
					SearchEmptyFoldersWorker.Dispose(); SearchEmptyFoldersWorker = null;
					OnAborted?.Invoke(this, new EventArgs());
				}
				else
				{
					SearchEmptyFoldersWorker.Dispose(); SearchEmptyFoldersWorker = null;

					OnCancelled?.Invoke(this, new EventArgs());
				}
			}
			else
			{
				int FolderCount = SearchEmptyFoldersWorker.FolderCount;

				SearchEmptyFoldersWorker.Dispose(); SearchEmptyFoldersWorker = null;

				OnFinishedScanForEmptyDirs?.Invoke(this, new FinishedScanForEmptyDirsEventArgs(RunData.ScanResults.Count, FolderCount));
			}
		}

		internal void CancelCurrentProcess()
		{
			if (CurrentProcessStep == WorkflowSteps.StartSearchingForEmptyDirs)
			{
				if (SearchEmptyFoldersWorker == null)
				{
					return;
				}

				if ((SearchEmptyFoldersWorker.IsBusy == true) || (SearchEmptyFoldersWorker.CancellationPending == false))
				{
					SearchEmptyFoldersWorker.CancelAsync();
				}
			}
			else if (CurrentProcessStep == WorkflowSteps.DeleteProcessRunning)
			{
				if (DeletionWorker == null)
				{
					return;
				}

				if ((DeletionWorker.IsBusy == true) || (DeletionWorker.CancellationPending == false))
				{
					DeletionWorker.CancelAsync();
				}
			}
		}

		public void StartDeleteProcess()
		{
			CurrentProcessStep = WorkflowSteps.DeleteProcessRunning;

			// Kick-off deletion worker to async delete directories
			DeletionWorker = new DeletionWorker();
			DeletionWorker.Data = RunData;
			DeletionWorker.ProgressChanged += new ProgressChangedEventHandler(DeletionWorker_ProgressChanged);
			DeletionWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DeletionWorker_RunWorkerCompleted);
			DeletionWorker.RunWorkerAsync();
		}

		private void DeletionWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			DeleteProcessUpdateEventArgs state = e.UserState as DeleteProcessUpdateEventArgs;

			OnDeleteProcessChanged?.Invoke(this, state);
		}

		private void DeletionWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			CurrentProcessStep = WorkflowSteps.Idle;

			if (e.Error != null)
			{
				ShowErrorMsg(e.Error.Message);

				DeletionWorker.Dispose(); DeletionWorker = null;
			}
			else if (e.Cancelled)
			{
				if (DeletionWorker.ErrorInfo != null)
				{
					// A error occurred, process was stopped
					//
					// -> Ask user to continue

					if (OnDeleteError != null)
					{
						OnDeleteError(this, DeletionWorker.ErrorInfo);
					}
					else
					{
						throw new Exception(RedGetText.Words.EventHandlerMissing);
					}
				}
				else
				{
					// The user cancelled the process
					OnCancelled?.Invoke(this, new EventArgs());
				}
			}
			else
			{
				// TODO: Use separate class here?
				int deletedCount = DeletionWorker.DeletedCount;
				int failedCount = DeletionWorker.FailedCount;
				int protectedCount = DeletionWorker.ProtectedCount;

				DeletionWorker.Dispose();
				DeletionWorker = null;

				OnDeleteProcessFinished?.Invoke(this, new DeleteProcessFinishedEventArgs(deletedCount, failedCount, protectedCount));
			}
		}

		internal void AddProtectedFolder(string path)
		{
			if (!RunData.ProtectedFolderList.ContainsKey(path))
			{
				RunData.ProtectedFolderList.Add(path, true);
			}
		}

		internal void RemoveProtected(string FolderFullName)
		{
			if (RunData.ProtectedFolderList.ContainsKey(FolderFullName))
			{
				RunData.ProtectedFolderList.Remove(FolderFullName);
			}
		}

		public string GetLogMessages()
		{
			return RunData.LogMessages.ToString();
		}

		private void ShowErrorMsg(string errorMessage)
		{
			OnError?.Invoke(this, new ErrorEventArgs(errorMessage));
		}

		internal void AbortDeletion()
		{
			CurrentProcessStep = WorkflowSteps.Idle;

			DeletionWorker.Dispose(); DeletionWorker = null;

			OnAborted?.Invoke(this, new EventArgs());
		}

		internal void ContinueDeleteProcess()
		{
			CurrentProcessStep = WorkflowSteps.DeleteProcessRunning;
			DeletionWorker.RunWorkerAsync();
		}
	}
}