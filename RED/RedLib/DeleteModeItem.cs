using System;
using TXT = RED.RedGetText;

namespace RED
{
	/// <summary>
	/// List box container class thingy
	/// </summary>
	public class DeleteModeItem
	{
		public DeleteModes DeleteMode { get; set; }

		public DeleteModeItem(DeleteModes Mode)
		{
			this.DeleteMode = Mode;
		}

		public static DeleteModes[] GetList()
		{
			return new DeleteModes[] {
				DeleteModes.RecycleBin,
				DeleteModes.RecycleBinShowErrors,
				DeleteModes.RecycleBinWithQuestion,
				DeleteModes.Direct,
				DeleteModes.Simulate
			};
		}

		public override string ToString()
		{
			switch (this.DeleteMode)
			{
				case DeleteModes.RecycleBin:
					return TXT.Translate("Delete to Recycle Bin, Ignore Errors (Safer but slower. Default Setting)");

				case DeleteModes.RecycleBinShowErrors:
					return TXT.Translate("Delete to Recycle Bin, Show Errors");

				case DeleteModes.RecycleBinWithQuestion:
					return TXT.Translate("Delete to Recycle Bin, Ask Before Every Deletion (can be annoying)");

				case DeleteModes.Direct:
					return TXT.Translate("Bypass Recycle Bin. Directly Delete Directories (more dangerous but faster)");

				case DeleteModes.Simulate:
					return TXT.Translate("Simulate Deletion (Just pretend to delete, for testing)");

				// TODO: Idea -> Move files instead of deleting?

				default:
					throw new Exception(TXT.Translate("Unknown delete mode"));
			}
		}
	}
}