using System;
using Alphaleonis.Win32.Filesystem;

namespace RED.Match
{
	public class RedScanResultItem
	{
		public RedScanResultItem(DirectoryInfo di, DirectorySearchStatusTypes status) : this(di, status, string.Empty) { }

		public RedScanResultItem(DirectoryInfo di, DirectorySearchStatusTypes status, string errorMsg)
		{
			Populate(di, status, errorMsg);
		}

		public DirectoryInfo Directory { get; private set; }
		public string FullPath { get { return Directory?.FullName; } }
		public string Name { get { return Directory?.Name; } }
		public DirectorySearchStatusTypes SearchStatus { get; private set; }
		public DirectorySearchStatusTypes SearchStatusOriginal { get; private set; }
		public string ErrorMessage { get; private set; }

		private void Populate(DirectoryInfo di, DirectorySearchStatusTypes status, string errorMsg)
		{
			try
			{
				Directory = di;
				SearchStatus = status;
				SearchStatusOriginal = status;
				ErrorMessage = errorMsg;
			}
			catch (Exception ex)
			{
				ErrorMessage = errorMsg + Environment.NewLine + ex.Message;
			}
		}
	}
}