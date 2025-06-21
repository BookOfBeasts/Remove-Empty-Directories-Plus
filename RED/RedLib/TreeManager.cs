using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Alphaleonis.Win32.Filesystem;
using RED.Match;
using FileAttributes = System.IO.FileAttributes;
using TXT = RED.RedGetText;

namespace RED
{
	/// <summary>
	/// Icon names (Warning: Entries are case sensitive)
	/// </summary>
	public enum DirectoryIcons
	{
		home,
		deleted,
		protected_icon,
		folder_warning
	}

	/// <summary>
	/// Handles tree related things
	///
	/// TODO: Handle null references within tree nodes
	/// </summary>
	public class TreeManager
	{
		public static Color ColorDoNotTouch { get { return Color.Gray; } }
		public static Color ColorProtected { get { return Color.Blue; } }
		public static Color ColortoBeDeleted { get { return Color.Red; } }

		private TreeView treeView = null;
		private TreeNode rootNode = null;
		private string rootPath = string.Empty;

		private Label fastModeInfoLabel = null;

		private Dictionary<String, TreeNode> directoryToTreeNodeMapping = null;

		/// <summary>
		/// This dictionary holds the original properties of protected
		/// nodes so that they can be restored if the user undoes the action
		/// </summary>
		private Dictionary<string, object> nodePropsBackup = new Dictionary<string, object>();

		public event EventHandler<ProtectionStatusChangedEventArgs> OnProtectionStatusChanged;

		public event EventHandler<DeleteRequestFromTreeEventArgs> OnDeleteRequest;

		private bool fastMode { get; set; } = true;

		public TreeManager(TreeView dirTree, Label fastModeInfoLabel)
		{
			this.treeView = dirTree;
			this.treeView.MouseClick += new MouseEventHandler(this.tvFolders_MouseClick);

			this.fastModeInfoLabel = fastModeInfoLabel;

			this.resetTree();

			this.rootPath = string.Empty;
		}

		#region Incoming "events"

		public void SetFastMode(bool fastModeActive)
		{
			this.fastMode = fastModeActive;
			fastModeInfoLabel.Visible = fastModeActive;

			if (this.fastMode)
			{
				this.treeView.SuspendLayout();
			}
			else
			{
				this.clearFastMode();
				this.treeView.ResumeLayout();
			}
		}

		public void OnSearchStart(DirectoryInfo directory)
		{
			this.resetTree();

			// Disable UI updates when fast mode is enabled
			if (this.fastMode)
			{
				suspendTreeViewForFastMode();
			}

			this.createRootNode(directory, DirectoryIcons.home);
		}

		public void OnSearchFinished()
		{
			this.showFastModeResults();
		}

		public void OnDeletionProcessStart()
		{
			if (this.fastMode)
			{
				this.treeView.Nodes.Clear();
				suspendTreeViewForFastMode();
			}
		}

		public void OnDeletionProcessFinished()
		{
			this.showFastModeResults();
		}

		public void OnProcessCancelled()
		{
			this.showFastModeResults();
		}

		#endregion Incoming "events"

		private void suspendTreeViewForFastMode()
		{
			this.treeView.SuspendLayout();

			this.treeView.BackColor = SystemColors.Control;
			this.fastModeInfoLabel.Visible = true;
		}

		private void clearFastMode()
		{
			this.treeView.BackColor = SystemColors.Window;
			this.fastModeInfoLabel.Visible = false;
		}

		private void showFastModeResults()
		{
			if (!this.fastMode)
			{
				return;
			}

			this.treeView.ResumeLayout();
			this.clearFastMode();

			this.addRootNode();

			// Scroll to root node and expand all dirs
			this.rootNode.EnsureVisible();
			this.treeView.ExpandAll();
		}

		/// <summary>
		/// Hack to selected the correct node
		/// </summary>
		private void tvFolders_MouseClick(object sender, MouseEventArgs e)
		{
			this.treeView.SelectedNode = this.treeView.GetNodeAt(e.X, e.Y);
		}

		private void resetTree()
		{
			this.rootNode = null;
			this.directoryToTreeNodeMapping = new Dictionary<string, TreeNode>();
			this.nodePropsBackup = new Dictionary<string, object>();

			this.treeView.Nodes.Clear();
		}

		private void createRootNode(DirectoryInfo directory, DirectoryIcons imageKey)
		{
			this.rootPath = directory.FullName.Trim(Path.DirectorySeparatorChar);

			rootNode = new TreeNode(directory.Name);
			rootNode.Tag = directory;
			rootNode.ImageKey = imageKey.ToString();
			rootNode.SelectedImageKey = imageKey.ToString();

			directoryToTreeNodeMapping = new Dictionary<String, TreeNode>();
			directoryToTreeNodeMapping.Add(directory.FullName, rootNode);

			if (!this.fastMode)
			{
				// During fast mode the root node will be added after the search finished
				addRootNode();
			}
		}

		private void addRootNode()
		{
			if (rootNode == null || (treeView.Nodes.Count == 1 && treeView.Nodes[0] == rootNode))
			{
				return;
			}

			this.treeView.Nodes.Clear();
			this.treeView.Nodes.Add(rootNode);
		}

		private void scrollToNode(TreeNode node)
		{
			// Ignore when fast mode is enabled
			if (!this.fastMode)
			{
				node.EnsureVisible();
			}
		}

		/// <summary>
		/// Marks a folder with the warning or deleted icon
		/// </summary>
		/// <param name="path">Dir path</param>
		/// <param name="iconKey">Icon</param>
		internal void UpdateItemIcon(RedScanResultItem scanResult, DirectoryIcons iconKey)
		{
			TreeNode treeNode = this.findOrCreateDirectoryNodeByPath(scanResult.Directory);

			treeNode.ImageKey = iconKey.ToString();
			treeNode.SelectedImageKey = iconKey.ToString();

			this.scrollToNode(treeNode);
		}

		// TODO: Find better code structure for the following two routines
		private TreeNode findOrCreateDirectoryNodeByPath(DirectoryInfo directory)
		{
			if (directory == null || string.IsNullOrWhiteSpace(directory.FullName))
			{
				return null;
			}

			if (directoryToTreeNodeMapping.ContainsKey(directory.FullName))
			{
				return directoryToTreeNodeMapping[directory.FullName];
			}
			else
			{
				return AddOrUpdateDirectoryNode(directory, DirectorySearchStatusTypes.NotEmpty, string.Empty);
			}
		}

		public TreeNode AddOrUpdateDirectoryNode(DirectoryInfo directory, DirectorySearchStatusTypes statusType, string optionalErrorMsg)
		{
			if (directoryToTreeNodeMapping.ContainsKey(directory.FullName))
			{
				// Just update the style if the node already exists
				TreeNode node = directoryToTreeNodeMapping[directory.FullName];
				applyNodeStyle(node, directory, statusType, optionalErrorMsg);
				return node;
			}

			//var directory = new DirectoryInfo(path);

			// Create new tree node
			TreeNode newTreeNode = new TreeNode(directory.Name);
			applyNodeStyle(newTreeNode, directory, statusType, optionalErrorMsg);
			newTreeNode.Tag = directory;

			if (directory.Parent.FullName.Trim(Path.AltDirectorySeparatorChar).Equals(this.rootPath, StringComparison.OrdinalIgnoreCase))
			{
				this.rootNode.Nodes.Add(newTreeNode);
			}
			else
			{
				TreeNode parentNode = this.findOrCreateDirectoryNodeByPath(directory.Parent);
				parentNode.Nodes.Add(newTreeNode);
			}

			directoryToTreeNodeMapping.Add(directory.FullName, newTreeNode);

			this.scrollToNode(newTreeNode);

			return newTreeNode;
		}

		private void applyNodeStyle(TreeNode treeNode, RedScanResultItem scanResult)
		{
			applyNodeStyle(treeNode, scanResult.Directory, scanResult.SearchStatus, scanResult.ErrorMessage);
		}

		//private void applyNodeStyle(TreeNode treeNode, string path, DirectorySearchStatusTypes statusType, string optionalErrorMsg)
		private void applyNodeStyle(TreeNode treeNode, DirectoryInfo directory, DirectorySearchStatusTypes statusType, string optionalErrorMsg)
		{
			//var directory = new DirectoryInfo(path);

			// TODO: use enums for icon names
			treeNode.ForeColor = (statusType == DirectorySearchStatusTypes.Empty) ? ColortoBeDeleted : ColorDoNotTouch;
			string iconKey = string.Empty;

			switch (statusType)
			{
				case DirectorySearchStatusTypes.Empty:
					int fileCount = directory.GetFiles().Length;
					bool containsTrash = (fileCount > 0);

					iconKey = containsTrash ? "folder_trash_files" : "folder";
					// TODO: use data from scan thread
					if ((directory.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
					{
						iconKey = containsTrash ? "folder_hidden_trash_files" : "folder_hidden";
					}
					if ((directory.Attributes & FileAttributes.Encrypted) == FileAttributes.Encrypted)
					{
						iconKey = containsTrash ? "folder_lock_trash_files" : "folder_lock";
					}
					if ((directory.Attributes & FileAttributes.System) == FileAttributes.System)
					{
						iconKey = containsTrash ? "folder_lock_trash_files" : "folder_lock";
					}
					if (containsTrash)
					{
						treeNode.ToolTipText += TXT.Translate("«empty files: {0}»", fileCount);
						treeNode.Text += "  " + treeNode.ToolTipText;
					}
					break;

				case DirectorySearchStatusTypes.Ignore:
					iconKey = "protected_icon";
					treeNode.ForeColor = ColorProtected;
					break;

				case DirectorySearchStatusTypes.NeverEmpty:
					iconKey = "folder_never_empty";
					treeNode.ForeColor = ColorDoNotTouch;
					treeNode.ToolTipText = TXT.Translate("«Never Empty»");
					treeNode.Text += "  " + treeNode.ToolTipText;
					break;

				case DirectorySearchStatusTypes.Error:
					iconKey = "folder_warning";
					if (!string.IsNullOrWhiteSpace(optionalErrorMsg))
					{
						optionalErrorMsg = optionalErrorMsg.Replace("\r", string.Empty).Replace("\n", string.Empty);
						if (optionalErrorMsg.Length > 55)
						{
							optionalErrorMsg = optionalErrorMsg.Substring(0, 55) + "...";
						}
						treeNode.Text += " (" + optionalErrorMsg + ")";
					}
					break;

				default:
					break;
			}

			if (treeNode != this.rootNode)
			{
				treeNode.ImageKey = iconKey;
				treeNode.SelectedImageKey = iconKey;
			}
		}

		/// <summary>
		/// Returns the selected folder path
		/// </summary>
		public string GetSelectedFolderPath()
		{
			if (this.treeView.SelectedNode != null && this.treeView.SelectedNode.Tag != null && this.treeView.SelectedNode.Tag is DirectoryInfo)
			{
				return ((DirectoryInfo)this.treeView.SelectedNode.Tag).FullName;
			}
			return string.Empty;
		}

		internal void DeleteSelectedDirectory()
		{
			if (this.treeView.SelectedNode != null && this.treeView.SelectedNode.Tag != null && this.treeView.SelectedNode.Tag is DirectoryInfo)
			{
				DirectoryInfo folder = (DirectoryInfo)this.treeView.SelectedNode.Tag;

				if (OnDeleteRequest != null)
				{
					OnDeleteRequest(this, new DeleteRequestFromTreeEventArgs(folder.FullName));
				}
			}
		}

		internal void RemoveNode(string path)
		{
			if (this.nodePropsBackup.ContainsKey(path))
			{
				this.nodePropsBackup.Remove(path);
			}

			if (this.directoryToTreeNodeMapping.ContainsKey(path))
			{
				this.directoryToTreeNodeMapping[path].Remove();
				this.directoryToTreeNodeMapping.Remove(path);
			}
		}

		#region Directory protection

		// NotBob - Added ProtectRoot t
		internal void ProtectRoot()
		{
			if (treeView.Nodes.Count > 0)
			{
				this.ProtectNode(treeView.Nodes[0]);
			}
		}

		internal void ProtectSelected()
		{
			if (treeView.SelectedNode != null)
			{
				this.ProtectNode(treeView.SelectedNode);
			}
		}

		internal void UnprotectSelected()
		{
			unprotectNode(treeView.SelectedNode);
		}

		private void unprotectNode(TreeNode node)
		{
			if (node != null)
			{
				DirectoryInfo directory = ((DirectoryInfo)node.Tag);

				if (!this.nodePropsBackup.ContainsKey(directory.FullName))
				{
					// TODO: What to do when this info is missing, show error?
					return;
				}

				// Restore props from backup values
				string[] propList = ((string)this.nodePropsBackup[directory.FullName]).Split('|');

				this.nodePropsBackup.Remove(directory.FullName);

				node.ImageKey = propList[0];
				node.SelectedImageKey = propList[0];
				node.ForeColor = Color.FromArgb(Int32.Parse(propList[1]));

				if (OnProtectionStatusChanged != null)
				{
					OnProtectionStatusChanged(this, new ProtectionStatusChangedEventArgs(directory.FullName, false));
				}

				// Unprotect all subnodes
				foreach (TreeNode subNode in node.Nodes)
				{
					this.unprotectNode(subNode);
				}
			}
		}

		private void ProtectNode(TreeNode node)
		{
			if (node != null)
			{
				DirectoryInfo directory = (DirectoryInfo)node.Tag;

				if (nodePropsBackup.ContainsKey(directory.FullName))
				{
					return;
				}

				if (OnProtectionStatusChanged != null)
				{
					OnProtectionStatusChanged(this, new ProtectionStatusChangedEventArgs(directory.FullName, true));
				}

				// Backup node props if the user changes his mind we can restore the node
				// TODO: I'm sure there is a better way to do this, maybe this info can be stored
				// in the node.Tag or we simply recreate this info like it's a new node.
				nodePropsBackup.Add(directory.FullName, node.ImageKey + "|" + node.ForeColor.ToArgb().ToString());
				if (node == rootNode)
				{
					node.ImageKey = "home_protected";
					node.SelectedImageKey = "home_protected";
				}
				else
				{
					node.ImageKey = "protected_icon";
					node.SelectedImageKey = "protected_icon";
				}
				node.ForeColor = ColorProtected;

				// Recursively protect directories
				if (node.Parent != this.rootNode)
				{
					ProtectNode(node.Parent);
				}
			}
		}

		#endregion Directory protection
	}
}