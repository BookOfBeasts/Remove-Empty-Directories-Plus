using System;
using Alphaleonis.Win32.Filesystem;
using RED.Match;

namespace RED
{
	public class WorkflowStepChangedEventArgs : EventArgs
	{
		public WorkflowSteps NewStep { get; set; }

		public WorkflowStepChangedEventArgs(WorkflowSteps NewStep)
		{
			this.NewStep = NewStep;
		}
	}

	public class ErrorEventArgs : EventArgs
	{
		public string Message { get; set; }

		public ErrorEventArgs(string msg)
		{
			this.Message = msg;
		}
	}

	public class FinishedScanForEmptyDirsEventArgs : EventArgs
	{
		public int EmptyFolderCount { get; set; }
		public int FolderCount { get; set; }

		public FinishedScanForEmptyDirsEventArgs(int EmptyFolderCount, int FolderCount)
		{
			this.EmptyFolderCount = EmptyFolderCount;
			this.FolderCount = FolderCount;
		}
	}

	public class DeleteProcessUpdateEventArgs : EventArgs
	{
		public RedScanResultItem ScanResult { get; private set; }
		public int ProgressStatus { get; set; }

		//public string Path { get; set; }
		public DirectoryDeletionStatusTypes Status { get; set; }

		public int FolderCount { get; set; }

		public DeleteProcessUpdateEventArgs(int progressStatus, RedScanResultItem scanResult, DirectoryDeletionStatusTypes status, int folderCount)
		{
			this.ScanResult = scanResult;
			this.ProgressStatus = progressStatus;
			//this.Path = path;
			this.Status = status;
			this.FolderCount = folderCount;
		}
	}

	public class DeleteProcessFinishedEventArgs : EventArgs
	{
		public int DeletedFolderCount { get; set; }
		public int FailedFolderCount { get; set; }
		public int ProtectedCount { get; set; }

		public DeleteProcessFinishedEventArgs(int deletedFolderCount, int failedFolderCount, int protectedCount)
		{
			this.DeletedFolderCount = deletedFolderCount;
			this.FailedFolderCount = failedFolderCount;
			this.ProtectedCount = protectedCount;
		}
	}

	public class ProtectionStatusChangedEventArgs : EventArgs
	{
		public string Path { get; set; }
		public bool Protected { get; set; }

		public ProtectionStatusChangedEventArgs(string Path, bool Protected)
		{
			this.Path = Path;
			this.Protected = Protected;
		}
	}

	public class DeleteRequestFromTreeEventArgs : EventArgs
	{
		public string Directory { get; set; }

		public DeleteRequestFromTreeEventArgs(string Directory)
		{
			this.Directory = Directory;
		}
	}

	public class DeletionErrorEventArgs : EventArgs
	{
		public string Path { get; set; }
		public string ErrorMessage { get; set; }

		public DeletionErrorEventArgs(string Path, string ErrorMessage)
		{
			this.Path = Path;
			this.ErrorMessage = ErrorMessage;
		}
	}

	public class FoundEmptyDirInfoEventArgs : EventArgs
	{
		public RedScanResultItem ScanResult { get; private set; }

		public FoundEmptyDirInfoEventArgs(DirectoryInfo directory, DirectorySearchStatusTypes type)
		{
			ScanResult = new RedScanResultItem(directory, type);
		}

		public FoundEmptyDirInfoEventArgs(DirectoryInfo directory, DirectorySearchStatusTypes type, string errorMessage)
		{
			ScanResult = new RedScanResultItem(directory, type, errorMessage);
		}
	}
}