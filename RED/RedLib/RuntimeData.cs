using System;
using System.Collections.Generic;
using System.Text;
using Alphaleonis.Win32.Filesystem;
using RED.Match;

namespace RED
{
	/// <summary>
	/// Container for runtime related data
	/// </summary>
	public class RuntimeData
	{
		public RuntimeData()
		{
			this.LogMessages = new StringBuilder();
			this.ProtectedFolderList = new Dictionary<string, bool>();
			this.ScanResults = new RedScanResultItemList();
		}

		public RedMatchItemList NeverEmptyDirectoryList = new RedMatchItemList(RedMatchFilterType.Directory);
		public RedMatchItemList IgnoreDirectoryNameList = new RedMatchItemList(RedMatchFilterType.Directory);
		public RedMatchItemList IgnoreFileNameList = new RedMatchItemList(RedMatchFilterType.Files);

		public DirectoryInfo StartFolder { get; set; }

        public bool HideScanErrors { get; set; }
        public bool HideDeletionErrors { get; set; }

		public DeleteModes DeleteMode { get; set; }
		public bool IgnoreEmptyFiles { get; set; }
		public bool IgnoreHiddenFolders { get; set; }
		public bool IgnoreSystemFolders { get; set; }
		public double PauseTime { get; set; }
		public uint MinFolderAgeHours { get; set; }

		public int MaxDepth { get; set; }
		public int InfiniteLoopDetectionCount { get; set; }

		public StringBuilder LogMessages = null;
		public Dictionary<string, bool> ProtectedFolderList = new Dictionary<string, bool>();

		// NotBob option to exclude ignored directories from the main window scanned list
		public bool HideIgnoredDirectories { get; set; }

		// NotBob - Dedicated ScanResults list to collect details of a RED scan
		public RedScanResultItemList ScanResults { get; private set; }

		public void AddLogMessage(string msg)
		{
			this.LogMessages.AppendLine(DateTime.Now.ToString("r") + "\t" + msg);
		}

		internal void AddLogSpacer()
		{
			if (this.LogMessages.Length > 0)
            {
                this.LogMessages.Append(Environment.NewLine);
            }
        }
	}
}