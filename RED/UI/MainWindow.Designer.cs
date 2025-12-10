namespace RED.UI
{
	partial class MainWindow
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.ilFolderIcons = new System.Windows.Forms.ImageList(this.components);
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabSearch = new System.Windows.Forms.TabPage();
            this.gbFind = new System.Windows.Forms.GroupBox();
            this.lbFastModeInfo = new System.Windows.Forms.Label();
            this.btnSearchDirectoryBrowseFor = new System.Windows.Forms.Button();
            this.pnlIconDesc = new System.Windows.Forms.Panel();
            this.lbHorzLine1 = new System.Windows.Forms.Label();
            this.lbColorProtected = new System.Windows.Forms.Label();
            this.pnlColorProtected = new System.Windows.Forms.Panel();
            this.lbColorToBeDeleted = new System.Windows.Forms.Label();
            this.pnlColorToBeDeleted = new System.Windows.Forms.Panel();
            this.lbColorDoNotTouch = new System.Windows.Forms.Label();
            this.pnlColorDoNoTouch = new System.Windows.Forms.Panel();
            this.lbIconDesc = new System.Windows.Forms.Label();
            this.tvSearchResults = new System.Windows.Forms.TreeView();
            this.cmTreeview = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSearchOnlyThisDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiProtectDirectoryOnce = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUnprotectDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAddToFilterDirectoryIgnore = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDeleteDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.txtSearchDirectory = new System.Windows.Forms.TextBox();
            this.lblPickAFolder = new System.Windows.Forms.Label();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.tcSettings = new System.Windows.Forms.TabControl();
            this.tabSettings1 = new System.Windows.Forms.TabPage();
            this.gbSettings1a = new System.Windows.Forms.GroupBox();
            this.cbSavePrompt = new System.Windows.Forms.CheckBox();
            this.cbIgnore0kbFiles = new System.Windows.Forms.CheckBox();
            this.cbFastSearchMode = new System.Windows.Forms.CheckBox();
            this.cbHideScanErrors = new System.Windows.Forms.CheckBox();
            this.cbAutoProtectRoot = new System.Windows.Forms.CheckBox();
            this.lbFastSearchMode = new System.Windows.Forms.Label();
            this.cbIgnoreSystemFolders = new System.Windows.Forms.CheckBox();
            this.cbHideDeletionErrors = new System.Windows.Forms.CheckBox();
            this.cbHideIgnoredFolders = new System.Windows.Forms.CheckBox();
            this.lbIgnore0kbFiles = new System.Windows.Forms.Label();
            this.cbIgnoreHiddenFolders = new System.Windows.Forms.CheckBox();
            this.cbClipboardDetection = new System.Windows.Forms.CheckBox();
            this.lbClipboardDetection = new System.Windows.Forms.Label();
            this.gbDeleteMode = new System.Windows.Forms.GroupBox();
            this.cbDeleteMode = new System.Windows.Forms.ComboBox();
            this.tabSettings2 = new System.Windows.Forms.TabPage();
            this.gbSettings2a = new System.Windows.Forms.GroupBox();
            this.gbSettings2r = new System.Windows.Forms.GroupBox();
            this.cbRememberDeletionStats = new System.Windows.Forms.CheckBox();
            this.cbRememberLastUsedDirectory = new System.Windows.Forms.CheckBox();
            this.cbRememberWindowDetails = new System.Windows.Forms.CheckBox();
            this.lbnuPause2 = new System.Windows.Forms.Label();
            this.lbFolderAge2 = new System.Windows.Forms.Label();
            this.lbMaxDepth2 = new System.Windows.Forms.Label();
            this.lbFolderAge1 = new System.Windows.Forms.Label();
            this.nuFolderAge = new System.Windows.Forms.NumericUpDown();
            this.lbPause1 = new System.Windows.Forms.Label();
            this.nuPause = new System.Windows.Forms.NumericUpDown();
            this.nuMaxDepth = new System.Windows.Forms.NumericUpDown();
            this.lbInfiniteLoopDetectionCount = new System.Windows.Forms.Label();
            this.lbMaxDepth1 = new System.Windows.Forms.Label();
            this.nuInfiniteLoopDetectionCount = new System.Windows.Forms.NumericUpDown();
            this.gbExplorerIntegration = new System.Windows.Forms.GroupBox();
            this.chkExplorerIntegrateAutoSearch = new System.Windows.Forms.CheckBox();
            this.lblExplorerIntegrationInfo = new System.Windows.Forms.Label();
            this.btnExplorerRemove = new System.Windows.Forms.Button();
            this.btnExplorerIntegrate = new System.Windows.Forms.Button();
            this.lbExplorerIntegration1 = new System.Windows.Forms.Label();
            this.gbAdvancedExtras = new System.Windows.Forms.GroupBox();
            this.btnResetFilters = new System.Windows.Forms.Button();
            this.btnCopyDebugInfo = new System.Windows.Forms.Button();
            this.btnResetConfig = new System.Windows.Forms.Button();
            this.tabFilters = new System.Windows.Forms.TabPage();
            this.tcFilters = new System.Windows.Forms.TabControl();
            this.tabFilterFoldersIgnore = new System.Windows.Forms.TabPage();
            this.flIgnoreFolders = new RED.UI.UCFilterList();
            this.gbIgnoreFolders = new System.Windows.Forms.GroupBox();
            this.lbIgnoreFolders1 = new System.Windows.Forms.Label();
            this.tabFilterFoldersNeverEmpty = new System.Windows.Forms.TabPage();
            this.flNeverEmptyFolders = new RED.UI.UCFilterList();
            this.gbIgnoreDirsNeverEmpty = new System.Windows.Forms.GroupBox();
            this.lbIgnoreDirsNeverEmpty1 = new System.Windows.Forms.Label();
            this.tabFiltersFilesIgnore = new System.Windows.Forms.TabPage();
            this.flIgnoreFiles = new RED.UI.UCFilterList();
            this.gbIgnoreFiles = new System.Windows.Forms.GroupBox();
            this.picWarning1 = new System.Windows.Forms.PictureBox();
            this.lbIgnoreFiles2 = new System.Windows.Forms.Label();
            this.lbIgnoreFiles1 = new System.Windows.Forms.Label();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.gbAbout = new System.Windows.Forms.GroupBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.lbNotBobInfoBuild = new System.Windows.Forms.Label();
            this.lbAppTitle = new System.Windows.Forms.Label();
            this.lblRedStats = new System.Windows.Forms.Label();
            this.lbCreatedBy = new System.Windows.Forms.Label();
            this.picAboutLogo = new System.Windows.Forms.PictureBox();
            this.linkLabelProjectHomepage = new System.Windows.Forms.LinkLabel();
            this.linkLabelJonasJohnRed = new System.Windows.Forms.LinkLabel();
            this.linkLabelFeedback = new System.Windows.Forms.LinkLabel();
            this.txtHelp = new System.Windows.Forms.TextBox();
            this.linkLabelCheckForUpdates = new System.Windows.Forms.LinkLabel();
            this.uxToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.stausStripMain = new System.Windows.Forms.StatusStrip();
            this.lbUiStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbProgressStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.lbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.pnlActionsSearch = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.uxMenuButtonExtras = new RED.UI.UCMenuButton();
            this.cmMenuExtras = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuItemShowLog = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemExportToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemExportToClip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExit = new System.Windows.Forms.Button();
            this.tcMain.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.gbFind.SuspendLayout();
            this.pnlIconDesc.SuspendLayout();
            this.cmTreeview.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.tcSettings.SuspendLayout();
            this.tabSettings1.SuspendLayout();
            this.gbSettings1a.SuspendLayout();
            this.gbDeleteMode.SuspendLayout();
            this.tabSettings2.SuspendLayout();
            this.gbSettings2a.SuspendLayout();
            this.gbSettings2r.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuFolderAge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuPause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuMaxDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuInfiniteLoopDetectionCount)).BeginInit();
            this.gbExplorerIntegration.SuspendLayout();
            this.gbAdvancedExtras.SuspendLayout();
            this.tabFilters.SuspendLayout();
            this.tcFilters.SuspendLayout();
            this.tabFilterFoldersIgnore.SuspendLayout();
            this.gbIgnoreFolders.SuspendLayout();
            this.tabFilterFoldersNeverEmpty.SuspendLayout();
            this.gbIgnoreDirsNeverEmpty.SuspendLayout();
            this.tabFiltersFilesIgnore.SuspendLayout();
            this.gbIgnoreFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWarning1)).BeginInit();
            this.tabAbout.SuspendLayout();
            this.gbAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAboutLogo)).BeginInit();
            this.stausStripMain.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.pnlActionsSearch.SuspendLayout();
            this.cmMenuExtras.SuspendLayout();
            this.SuspendLayout();
            // 
            // ilFolderIcons
            // 
            this.ilFolderIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilFolderIcons.ImageStream")));
            this.ilFolderIcons.TransparentColor = System.Drawing.Color.White;
            this.ilFolderIcons.Images.SetKeyName(0, "trash");
            this.ilFolderIcons.Images.SetKeyName(1, "cancel");
            this.ilFolderIcons.Images.SetKeyName(2, "deleted");
            this.ilFolderIcons.Images.SetKeyName(3, "folder");
            this.ilFolderIcons.Images.SetKeyName(4, "folder_hidden");
            this.ilFolderIcons.Images.SetKeyName(5, "folder_lock");
            this.ilFolderIcons.Images.SetKeyName(6, "folder_lock_trash_files");
            this.ilFolderIcons.Images.SetKeyName(7, "folder_trash_files");
            this.ilFolderIcons.Images.SetKeyName(8, "folder_warning");
            this.ilFolderIcons.Images.SetKeyName(9, "help");
            this.ilFolderIcons.Images.SetKeyName(10, "home_jj");
            this.ilFolderIcons.Images.SetKeyName(11, "search");
            this.ilFolderIcons.Images.SetKeyName(12, "folder_hidden_trash_files");
            this.ilFolderIcons.Images.SetKeyName(13, "preferences");
            this.ilFolderIcons.Images.SetKeyName(14, "exit");
            this.ilFolderIcons.Images.SetKeyName(15, "protected_icon");
            this.ilFolderIcons.Images.SetKeyName(16, "filter");
            this.ilFolderIcons.Images.SetKeyName(17, "info");
            this.ilFolderIcons.Images.SetKeyName(18, "config1");
            this.ilFolderIcons.Images.SetKeyName(19, "config2");
            this.ilFolderIcons.Images.SetKeyName(20, "home_jj_protected");
            this.ilFolderIcons.Images.SetKeyName(21, "home");
            this.ilFolderIcons.Images.SetKeyName(22, "home_protected");
            this.ilFolderIcons.Images.SetKeyName(23, "folder_never_empty");
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tabSearch);
            this.tcMain.Controls.Add(this.tabSettings);
            this.tcMain.Controls.Add(this.tabFilters);
            this.tcMain.Controls.Add(this.tabAbout);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.ImageList = this.ilFolderIcons;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Multiline = true;
            this.tcMain.Name = "tcMain";
            this.tcMain.Padding = new System.Drawing.Point(10, 5);
            this.tcMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tcMain.SelectedIndex = 0;
            this.tcMain.ShowToolTips = true;
            this.tcMain.Size = new System.Drawing.Size(664, 470);
            this.tcMain.TabIndex = 0;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
            // 
            // tabSearch
            // 
            this.tabSearch.AccessibleDescription = "";
            this.tabSearch.AccessibleName = "";
            this.tabSearch.Controls.Add(this.gbFind);
            this.tabSearch.Controls.Add(this.lblPickAFolder);
            this.tabSearch.ImageKey = "search";
            this.tabSearch.Location = new System.Drawing.Point(4, 27);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tabSearch.Size = new System.Drawing.Size(656, 439);
            this.tabSearch.TabIndex = 0;
            this.tabSearch.Text = "Search";
            this.tabSearch.UseVisualStyleBackColor = true;
            // 
            // gbFind
            // 
            this.gbFind.Controls.Add(this.lbFastModeInfo);
            this.gbFind.Controls.Add(this.btnSearchDirectoryBrowseFor);
            this.gbFind.Controls.Add(this.pnlIconDesc);
            this.gbFind.Controls.Add(this.tvSearchResults);
            this.gbFind.Controls.Add(this.txtSearchDirectory);
            this.gbFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFind.Location = new System.Drawing.Point(3, 3);
            this.gbFind.Name = "gbFind";
            this.gbFind.Size = new System.Drawing.Size(650, 433);
            this.gbFind.TabIndex = 0;
            this.gbFind.TabStop = false;
            this.gbFind.Text = "Select Directory To Be Searched";
            // 
            // lbFastModeInfo
            // 
            this.lbFastModeInfo.AutoSize = true;
            this.lbFastModeInfo.BackColor = System.Drawing.SystemColors.Control;
            this.lbFastModeInfo.ForeColor = System.Drawing.Color.Gray;
            this.lbFastModeInfo.Location = new System.Drawing.Point(90, 130);
            this.lbFastModeInfo.Name = "lbFastModeInfo";
            this.lbFastModeInfo.Size = new System.Drawing.Size(351, 13);
            this.lbFastModeInfo.TabIndex = 11;
            this.lbFastModeInfo.Text = "[Fast mode is enabled. Results will be shown after the process is finished]";
            // 
            // btnSearchDirectoryBrowseFor
            // 
            this.btnSearchDirectoryBrowseFor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchDirectoryBrowseFor.Location = new System.Drawing.Point(517, 17);
            this.btnSearchDirectoryBrowseFor.Name = "btnSearchDirectoryBrowseFor";
            this.btnSearchDirectoryBrowseFor.Size = new System.Drawing.Size(126, 21);
            this.btnSearchDirectoryBrowseFor.TabIndex = 2;
            this.btnSearchDirectoryBrowseFor.Text = "&Browse...";
            this.btnSearchDirectoryBrowseFor.UseVisualStyleBackColor = true;
            this.btnSearchDirectoryBrowseFor.Click += new System.EventHandler(this.btnSearchDirectoryBrowseFor_Click);
            // 
            // pnlIconDesc
            // 
            this.pnlIconDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlIconDesc.BackColor = System.Drawing.SystemColors.Info;
            this.pnlIconDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlIconDesc.Controls.Add(this.lbHorzLine1);
            this.pnlIconDesc.Controls.Add(this.lbColorProtected);
            this.pnlIconDesc.Controls.Add(this.pnlColorProtected);
            this.pnlIconDesc.Controls.Add(this.lbColorToBeDeleted);
            this.pnlIconDesc.Controls.Add(this.pnlColorToBeDeleted);
            this.pnlIconDesc.Controls.Add(this.lbColorDoNotTouch);
            this.pnlIconDesc.Controls.Add(this.pnlColorDoNoTouch);
            this.pnlIconDesc.Controls.Add(this.lbIconDesc);
            this.pnlIconDesc.Enabled = false;
            this.pnlIconDesc.Location = new System.Drawing.Point(519, 45);
            this.pnlIconDesc.Name = "pnlIconDesc";
            this.pnlIconDesc.Size = new System.Drawing.Size(126, 385);
            this.pnlIconDesc.TabIndex = 10;
            // 
            // lbHorzLine1
            // 
            this.lbHorzLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbHorzLine1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbHorzLine1.Location = new System.Drawing.Point(0, 303);
            this.lbHorzLine1.Name = "lbHorzLine1";
            this.lbHorzLine1.Size = new System.Drawing.Size(126, 2);
            this.lbHorzLine1.TabIndex = 23;
            // 
            // lbColorProtected
            // 
            this.lbColorProtected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbColorProtected.AutoSize = true;
            this.lbColorProtected.Location = new System.Drawing.Point(25, 364);
            this.lbColorProtected.Name = "lbColorProtected";
            this.lbColorProtected.Size = new System.Drawing.Size(53, 13);
            this.lbColorProtected.TabIndex = 22;
            this.lbColorProtected.Text = "Protected";
            // 
            // pnlColorProtected
            // 
            this.pnlColorProtected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlColorProtected.BackColor = System.Drawing.Color.Blue;
            this.pnlColorProtected.Location = new System.Drawing.Point(8, 363);
            this.pnlColorProtected.Name = "pnlColorProtected";
            this.pnlColorProtected.Size = new System.Drawing.Size(15, 15);
            this.pnlColorProtected.TabIndex = 21;
            // 
            // lbColorToBeDeleted
            // 
            this.lbColorToBeDeleted.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbColorToBeDeleted.AutoSize = true;
            this.lbColorToBeDeleted.Location = new System.Drawing.Point(25, 341);
            this.lbColorToBeDeleted.Name = "lbColorToBeDeleted";
            this.lbColorToBeDeleted.Size = new System.Drawing.Size(77, 13);
            this.lbColorToBeDeleted.TabIndex = 20;
            this.lbColorToBeDeleted.Text = "Will be deleted";
            // 
            // pnlColorToBeDeleted
            // 
            this.pnlColorToBeDeleted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlColorToBeDeleted.BackColor = System.Drawing.Color.Red;
            this.pnlColorToBeDeleted.Location = new System.Drawing.Point(8, 340);
            this.pnlColorToBeDeleted.Name = "pnlColorToBeDeleted";
            this.pnlColorToBeDeleted.Size = new System.Drawing.Size(15, 15);
            this.pnlColorToBeDeleted.TabIndex = 19;
            // 
            // lbColorDoNotTouch
            // 
            this.lbColorDoNotTouch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbColorDoNotTouch.AutoSize = true;
            this.lbColorDoNotTouch.Location = new System.Drawing.Point(24, 318);
            this.lbColorDoNotTouch.Name = "lbColorDoNotTouch";
            this.lbColorDoNotTouch.Size = new System.Drawing.Size(95, 13);
            this.lbColorDoNotTouch.TabIndex = 18;
            this.lbColorDoNotTouch.Text = "Will not be deleted";
            // 
            // pnlColorDoNoTouch
            // 
            this.pnlColorDoNoTouch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlColorDoNoTouch.BackColor = System.Drawing.Color.Gray;
            this.pnlColorDoNoTouch.Location = new System.Drawing.Point(8, 317);
            this.pnlColorDoNoTouch.Name = "pnlColorDoNoTouch";
            this.pnlColorDoNoTouch.Size = new System.Drawing.Size(15, 15);
            this.pnlColorDoNoTouch.TabIndex = 17;
            // 
            // lbIconDesc
            // 
            this.lbIconDesc.AutoSize = true;
            this.lbIconDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIconDesc.Location = new System.Drawing.Point(4, 6);
            this.lbIconDesc.Name = "lbIconDesc";
            this.lbIconDesc.Size = new System.Drawing.Size(90, 13);
            this.lbIconDesc.TabIndex = 0;
            this.lbIconDesc.Text = "Icon Meanings";
            this.lbIconDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tvSearchResults
            // 
            this.tvSearchResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSearchResults.ContextMenuStrip = this.cmTreeview;
            this.tvSearchResults.ImageKey = "folder";
            this.tvSearchResults.ImageList = this.ilFolderIcons;
            this.tvSearchResults.Location = new System.Drawing.Point(13, 45);
            this.tvSearchResults.Name = "tvSearchResults";
            this.tvSearchResults.SelectedImageKey = "folder";
            this.tvSearchResults.ShowNodeToolTips = true;
            this.tvSearchResults.Size = new System.Drawing.Size(500, 385);
            this.tvSearchResults.TabIndex = 3;
            this.tvSearchResults.DoubleClick += new System.EventHandler(this.tvSearchResults_DoubleClick);
            // 
            // cmTreeview
            // 
            this.cmTreeview.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmTreeview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenFolder,
            this.tsmiSearchOnlyThisDirectory,
            this.toolStripSeparator4,
            this.tsmiExpandAll,
            this.tsmiCollapseAll,
            this.toolStripSeparator1,
            this.tsmiProtectDirectoryOnce,
            this.tsmiUnprotectDirectory,
            this.toolStripSeparator3,
            this.tsmiAddToFilterDirectoryIgnore,
            this.toolStripSeparator2,
            this.tsmiDeleteDirectory});
            this.cmTreeview.Name = "cmStrip";
            this.cmTreeview.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmTreeview.Size = new System.Drawing.Size(227, 204);
            this.cmTreeview.Opening += new System.ComponentModel.CancelEventHandler(this.cmTreeview_Opening);
            // 
            // tsmiOpenFolder
            // 
            this.tsmiOpenFolder.Image = ((System.Drawing.Image)(resources.GetObject("tsmiOpenFolder.Image")));
            this.tsmiOpenFolder.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiOpenFolder.Name = "tsmiOpenFolder";
            this.tsmiOpenFolder.Size = new System.Drawing.Size(226, 22);
            this.tsmiOpenFolder.Text = "&Open in Explorer";
            this.tsmiOpenFolder.Click += new System.EventHandler(this.tsmiOpenDirectory_Click);
            // 
            // tsmiSearchOnlyThisDirectory
            // 
            this.tsmiSearchOnlyThisDirectory.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSearchOnlyThisDirectory.Image")));
            this.tsmiSearchOnlyThisDirectory.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiSearchOnlyThisDirectory.Name = "tsmiSearchOnlyThisDirectory";
            this.tsmiSearchOnlyThisDirectory.Size = new System.Drawing.Size(226, 22);
            this.tsmiSearchOnlyThisDirectory.Text = "&Search only this directory";
            this.tsmiSearchOnlyThisDirectory.Click += new System.EventHandler(this.tsmiSearchOnlyThisDirectory_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(223, 6);
            // 
            // tsmiExpandAll
            // 
            this.tsmiExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiExpandAll.Image")));
            this.tsmiExpandAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiExpandAll.Name = "tsmiExpandAll";
            this.tsmiExpandAll.Size = new System.Drawing.Size(226, 22);
            this.tsmiExpandAll.Text = "&Expand all";
            this.tsmiExpandAll.Click += new System.EventHandler(this.tsmiExpandAll_Click);
            // 
            // tsmiCollapseAll
            // 
            this.tsmiCollapseAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCollapseAll.Image")));
            this.tsmiCollapseAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiCollapseAll.Name = "tsmiCollapseAll";
            this.tsmiCollapseAll.Size = new System.Drawing.Size(226, 22);
            this.tsmiCollapseAll.Text = "&Collapse all";
            this.tsmiCollapseAll.Click += new System.EventHandler(this.tsmiCollapseAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(223, 6);
            // 
            // tsmiProtectDirectoryOnce
            // 
            this.tsmiProtectDirectoryOnce.Image = ((System.Drawing.Image)(resources.GetObject("tsmiProtectDirectoryOnce.Image")));
            this.tsmiProtectDirectoryOnce.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiProtectDirectoryOnce.Name = "tsmiProtectDirectoryOnce";
            this.tsmiProtectDirectoryOnce.Size = new System.Drawing.Size(226, 22);
            this.tsmiProtectDirectoryOnce.Text = "&Protect from deletion (once)";
            this.tsmiProtectDirectoryOnce.Click += new System.EventHandler(this.tsmiProtectDirectoryOnce_Click);
            // 
            // tsmiUnprotectDirectory
            // 
            this.tsmiUnprotectDirectory.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUnprotectDirectory.Image")));
            this.tsmiUnprotectDirectory.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiUnprotectDirectory.Name = "tsmiUnprotectDirectory";
            this.tsmiUnprotectDirectory.Size = new System.Drawing.Size(226, 22);
            this.tsmiUnprotectDirectory.Text = "&Unprotect";
            this.tsmiUnprotectDirectory.Click += new System.EventHandler(this.tsmiUnprotectDirectory_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(223, 6);
            // 
            // tsmiAddToFilterDirectoryIgnore
            // 
            this.tsmiAddToFilterDirectoryIgnore.Image = ((System.Drawing.Image)(resources.GetObject("tsmiAddToFilterDirectoryIgnore.Image")));
            this.tsmiAddToFilterDirectoryIgnore.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiAddToFilterDirectoryIgnore.Name = "tsmiAddToFilterDirectoryIgnore";
            this.tsmiAddToFilterDirectoryIgnore.Size = new System.Drawing.Size(226, 22);
            this.tsmiAddToFilterDirectoryIgnore.Text = "Add to permanent &ignore list";
            this.tsmiAddToFilterDirectoryIgnore.Click += new System.EventHandler(this.tsmiAddToFilterDirectoryIgnore_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(223, 6);
            // 
            // tsmiDeleteDirectory
            // 
            this.tsmiDeleteDirectory.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDeleteDirectory.Image")));
            this.tsmiDeleteDirectory.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiDeleteDirectory.Name = "tsmiDeleteDirectory";
            this.tsmiDeleteDirectory.Size = new System.Drawing.Size(226, 22);
            this.tsmiDeleteDirectory.Text = "&Delete";
            this.tsmiDeleteDirectory.Click += new System.EventHandler(this.tsmiDeleteDirectory_Click);
            // 
            // txtSearchDirectory
            // 
            this.txtSearchDirectory.AccessibleDescription = "";
            this.txtSearchDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchDirectory.Location = new System.Drawing.Point(16, 18);
            this.txtSearchDirectory.Name = "txtSearchDirectory";
            this.txtSearchDirectory.Size = new System.Drawing.Size(497, 20);
            this.txtSearchDirectory.TabIndex = 1;
            this.txtSearchDirectory.Text = "C:\\";
            this.txtSearchDirectory.Enter += new System.EventHandler(this.txtSearchDirectory_Enter);
            this.txtSearchDirectory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtSearchDirectory_MouseDoubleClick);
            // 
            // lblPickAFolder
            // 
            this.lblPickAFolder.AutoSize = true;
            this.lblPickAFolder.Location = new System.Drawing.Point(10, 13);
            this.lblPickAFolder.Name = "lblPickAFolder";
            this.lblPickAFolder.Size = new System.Drawing.Size(0, 13);
            this.lblPickAFolder.TabIndex = 3;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tcSettings);
            this.tabSettings.ImageKey = "config1";
            this.tabSettings.Location = new System.Drawing.Point(4, 27);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(656, 439);
            this.tabSettings.TabIndex = 1;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // tcSettings
            // 
            this.tcSettings.Controls.Add(this.tabSettings1);
            this.tcSettings.Controls.Add(this.tabSettings2);
            this.tcSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSettings.ImageList = this.ilFolderIcons;
            this.tcSettings.Location = new System.Drawing.Point(3, 3);
            this.tcSettings.Name = "tcSettings";
            this.tcSettings.SelectedIndex = 0;
            this.tcSettings.Size = new System.Drawing.Size(650, 433);
            this.tcSettings.TabIndex = 12;
            // 
            // tabSettings1
            // 
            this.tabSettings1.Controls.Add(this.gbSettings1a);
            this.tabSettings1.Controls.Add(this.gbDeleteMode);
            this.tabSettings1.ImageKey = "config1";
            this.tabSettings1.Location = new System.Drawing.Point(4, 23);
            this.tabSettings1.Name = "tabSettings1";
            this.tabSettings1.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings1.Size = new System.Drawing.Size(642, 406);
            this.tabSettings1.TabIndex = 0;
            this.tabSettings1.Text = "General Settings";
            this.tabSettings1.UseVisualStyleBackColor = true;
            // 
            // gbSettings1a
            // 
            this.gbSettings1a.Controls.Add(this.cbSavePrompt);
            this.gbSettings1a.Controls.Add(this.cbIgnore0kbFiles);
            this.gbSettings1a.Controls.Add(this.cbFastSearchMode);
            this.gbSettings1a.Controls.Add(this.cbHideScanErrors);
            this.gbSettings1a.Controls.Add(this.cbAutoProtectRoot);
            this.gbSettings1a.Controls.Add(this.lbFastSearchMode);
            this.gbSettings1a.Controls.Add(this.cbIgnoreSystemFolders);
            this.gbSettings1a.Controls.Add(this.cbHideDeletionErrors);
            this.gbSettings1a.Controls.Add(this.cbHideIgnoredFolders);
            this.gbSettings1a.Controls.Add(this.lbIgnore0kbFiles);
            this.gbSettings1a.Controls.Add(this.cbIgnoreHiddenFolders);
            this.gbSettings1a.Controls.Add(this.cbClipboardDetection);
            this.gbSettings1a.Controls.Add(this.lbClipboardDetection);
            this.gbSettings1a.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSettings1a.Location = new System.Drawing.Point(3, 3);
            this.gbSettings1a.Name = "gbSettings1a";
            this.gbSettings1a.Size = new System.Drawing.Size(636, 350);
            this.gbSettings1a.TabIndex = 21;
            this.gbSettings1a.TabStop = false;
            // 
            // cbSavePrompt
            // 
            this.cbSavePrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSavePrompt.AutoSize = true;
            this.cbSavePrompt.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSavePrompt.Location = new System.Drawing.Point(525, 19);
            this.cbSavePrompt.Name = "cbSavePrompt";
            this.cbSavePrompt.Size = new System.Drawing.Size(87, 17);
            this.cbSavePrompt.TabIndex = 12;
            this.cbSavePrompt.Text = "Save Prompt";
            this.cbSavePrompt.UseVisualStyleBackColor = true;
            // 
            // cbIgnore0kbFiles
            // 
            this.cbIgnore0kbFiles.AutoSize = true;
            this.cbIgnore0kbFiles.Checked = true;
            this.cbIgnore0kbFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnore0kbFiles.Location = new System.Drawing.Point(9, 19);
            this.cbIgnore0kbFiles.Name = "cbIgnore0kbFiles";
            this.cbIgnore0kbFiles.Size = new System.Drawing.Size(281, 17);
            this.cbIgnore0kbFiles.TabIndex = 0;
            this.cbIgnore0kbFiles.Text = "Directories With Empty Files Will Be Considered Empty";
            this.cbIgnore0kbFiles.UseVisualStyleBackColor = true;
            // 
            // cbFastSearchMode
            // 
            this.cbFastSearchMode.AutoSize = true;
            this.cbFastSearchMode.Location = new System.Drawing.Point(9, 204);
            this.cbFastSearchMode.Name = "cbFastSearchMode";
            this.cbFastSearchMode.Size = new System.Drawing.Size(76, 17);
            this.cbFastSearchMode.TabIndex = 8;
            this.cbFastSearchMode.Text = "Fast Mode";
            this.cbFastSearchMode.UseVisualStyleBackColor = true;
            this.cbFastSearchMode.CheckedChanged += new System.EventHandler(this.cbFastSearchMode_CheckedChanged);
            // 
            // cbHideScanErrors
            // 
            this.cbHideScanErrors.AutoSize = true;
            this.cbHideScanErrors.Location = new System.Drawing.Point(9, 135);
            this.cbHideScanErrors.Name = "cbHideScanErrors";
            this.cbHideScanErrors.Size = new System.Drawing.Size(274, 17);
            this.cbHideScanErrors.TabIndex = 5;
            this.cbHideScanErrors.Text = "Hide Errors During Search (eg Access Denied errors)";
            this.cbHideScanErrors.UseVisualStyleBackColor = true;
            // 
            // cbAutoProtectRoot
            // 
            this.cbAutoProtectRoot.AutoSize = true;
            this.cbAutoProtectRoot.Location = new System.Drawing.Point(9, 181);
            this.cbAutoProtectRoot.Name = "cbAutoProtectRoot";
            this.cbAutoProtectRoot.Size = new System.Drawing.Size(227, 17);
            this.cbAutoProtectRoot.TabIndex = 7;
            this.cbAutoProtectRoot.Text = "Automatically Protect the Starting Directory";
            this.cbAutoProtectRoot.UseVisualStyleBackColor = true;
            // 
            // lbFastSearchMode
            // 
            this.lbFastSearchMode.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbFastSearchMode.Location = new System.Drawing.Point(26, 223);
            this.lbFastSearchMode.Name = "lbFastSearchMode";
            this.lbFastSearchMode.Size = new System.Drawing.Size(586, 26);
            this.lbFastSearchMode.TabIndex = 9;
            this.lbFastSearchMode.Text = resources.GetString("lbFastSearchMode.Text");
            // 
            // cbIgnoreSystemFolders
            // 
            this.cbIgnoreSystemFolders.AutoSize = true;
            this.cbIgnoreSystemFolders.Checked = true;
            this.cbIgnoreSystemFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnoreSystemFolders.Location = new System.Drawing.Point(9, 66);
            this.cbIgnoreSystemFolders.Name = "cbIgnoreSystemFolders";
            this.cbIgnoreSystemFolders.Size = new System.Drawing.Size(222, 17);
            this.cbIgnoreSystemFolders.TabIndex = 2;
            this.cbIgnoreSystemFolders.Text = "Ignore System Directories (recommended)";
            this.cbIgnoreSystemFolders.UseVisualStyleBackColor = true;
            // 
            // cbHideDeletionErrors
            // 
            this.cbHideDeletionErrors.AutoSize = true;
            this.cbHideDeletionErrors.Location = new System.Drawing.Point(9, 112);
            this.cbHideDeletionErrors.Name = "cbHideDeletionErrors";
            this.cbHideDeletionErrors.Size = new System.Drawing.Size(154, 17);
            this.cbHideDeletionErrors.TabIndex = 4;
            this.cbHideDeletionErrors.Text = "Hide Errors During Deletion";
            this.cbHideDeletionErrors.UseVisualStyleBackColor = true;
            // 
            // cbHideIgnoredFolders
            // 
            this.cbHideIgnoredFolders.AutoSize = true;
            this.cbHideIgnoredFolders.Location = new System.Drawing.Point(9, 158);
            this.cbHideIgnoredFolders.Name = "cbHideIgnoredFolders";
            this.cbHideIgnoredFolders.Size = new System.Drawing.Size(140, 17);
            this.cbHideIgnoredFolders.TabIndex = 6;
            this.cbHideIgnoredFolders.Text = "Hide Ignored Directories";
            this.cbHideIgnoredFolders.UseVisualStyleBackColor = true;
            // 
            // lbIgnore0kbFiles
            // 
            this.lbIgnore0kbFiles.AutoSize = true;
            this.lbIgnore0kbFiles.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbIgnore0kbFiles.Location = new System.Drawing.Point(26, 38);
            this.lbIgnore0kbFiles.Name = "lbIgnore0kbFiles";
            this.lbIgnore0kbFiles.Size = new System.Drawing.Size(199, 13);
            this.lbIgnore0kbFiles.TabIndex = 1;
            this.lbIgnore0kbFiles.Text = "An empty file is one with a size of 0 bytes";
            // 
            // cbIgnoreHiddenFolders
            // 
            this.cbIgnoreHiddenFolders.AutoSize = true;
            this.cbIgnoreHiddenFolders.Location = new System.Drawing.Point(9, 89);
            this.cbIgnoreHiddenFolders.Name = "cbIgnoreHiddenFolders";
            this.cbIgnoreHiddenFolders.Size = new System.Drawing.Size(146, 17);
            this.cbIgnoreHiddenFolders.TabIndex = 3;
            this.cbIgnoreHiddenFolders.Text = "Ignore Hidden Directories";
            this.cbIgnoreHiddenFolders.UseVisualStyleBackColor = true;
            // 
            // cbClipboardDetection
            // 
            this.cbClipboardDetection.AutoSize = true;
            this.cbClipboardDetection.Location = new System.Drawing.Point(9, 261);
            this.cbClipboardDetection.Name = "cbClipboardDetection";
            this.cbClipboardDetection.Size = new System.Drawing.Size(164, 17);
            this.cbClipboardDetection.TabIndex = 10;
            this.cbClipboardDetection.Text = "Detect Paths in the Clipboard";
            this.cbClipboardDetection.UseVisualStyleBackColor = true;
            // 
            // lbClipboardDetection
            // 
            this.lbClipboardDetection.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbClipboardDetection.Location = new System.Drawing.Point(26, 279);
            this.lbClipboardDetection.Name = "lbClipboardDetection";
            this.lbClipboardDetection.Size = new System.Drawing.Size(586, 34);
            this.lbClipboardDetection.TabIndex = 11;
            this.lbClipboardDetection.Text = resources.GetString("lbClipboardDetection.Text");
            // 
            // gbDeleteMode
            // 
            this.gbDeleteMode.Controls.Add(this.cbDeleteMode);
            this.gbDeleteMode.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbDeleteMode.Location = new System.Drawing.Point(3, 353);
            this.gbDeleteMode.Name = "gbDeleteMode";
            this.gbDeleteMode.Size = new System.Drawing.Size(636, 50);
            this.gbDeleteMode.TabIndex = 20;
            this.gbDeleteMode.TabStop = false;
            this.gbDeleteMode.Text = "How Should Empty Directories Be Deleted?";
            // 
            // cbDeleteMode
            // 
            this.cbDeleteMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDeleteMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDeleteMode.FormattingEnabled = true;
            this.cbDeleteMode.Location = new System.Drawing.Point(6, 19);
            this.cbDeleteMode.Name = "cbDeleteMode";
            this.cbDeleteMode.Size = new System.Drawing.Size(624, 21);
            this.cbDeleteMode.TabIndex = 1;
            // 
            // tabSettings2
            // 
            this.tabSettings2.Controls.Add(this.gbSettings2a);
            this.tabSettings2.Controls.Add(this.gbExplorerIntegration);
            this.tabSettings2.Controls.Add(this.gbAdvancedExtras);
            this.tabSettings2.ImageKey = "config2";
            this.tabSettings2.Location = new System.Drawing.Point(4, 23);
            this.tabSettings2.Name = "tabSettings2";
            this.tabSettings2.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings2.Size = new System.Drawing.Size(642, 406);
            this.tabSettings2.TabIndex = 1;
            this.tabSettings2.Text = "Advanced Settings";
            this.tabSettings2.UseVisualStyleBackColor = true;
            // 
            // gbSettings2a
            // 
            this.gbSettings2a.Controls.Add(this.gbSettings2r);
            this.gbSettings2a.Controls.Add(this.lbnuPause2);
            this.gbSettings2a.Controls.Add(this.lbFolderAge2);
            this.gbSettings2a.Controls.Add(this.lbMaxDepth2);
            this.gbSettings2a.Controls.Add(this.lbFolderAge1);
            this.gbSettings2a.Controls.Add(this.nuFolderAge);
            this.gbSettings2a.Controls.Add(this.lbPause1);
            this.gbSettings2a.Controls.Add(this.nuPause);
            this.gbSettings2a.Controls.Add(this.nuMaxDepth);
            this.gbSettings2a.Controls.Add(this.lbInfiniteLoopDetectionCount);
            this.gbSettings2a.Controls.Add(this.lbMaxDepth1);
            this.gbSettings2a.Controls.Add(this.nuInfiniteLoopDetectionCount);
            this.gbSettings2a.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSettings2a.Location = new System.Drawing.Point(3, 3);
            this.gbSettings2a.Name = "gbSettings2a";
            this.gbSettings2a.Size = new System.Drawing.Size(636, 283);
            this.gbSettings2a.TabIndex = 0;
            this.gbSettings2a.TabStop = false;
            // 
            // gbSettings2r
            // 
            this.gbSettings2r.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSettings2r.Controls.Add(this.cbRememberDeletionStats);
            this.gbSettings2r.Controls.Add(this.cbRememberLastUsedDirectory);
            this.gbSettings2r.Controls.Add(this.cbRememberWindowDetails);
            this.gbSettings2r.Location = new System.Drawing.Point(430, 184);
            this.gbSettings2r.Name = "gbSettings2r";
            this.gbSettings2r.Size = new System.Drawing.Size(200, 93);
            this.gbSettings2r.TabIndex = 25;
            this.gbSettings2r.TabStop = false;
            this.gbSettings2r.Text = "Remember";
            // 
            // cbRememberDeletionStats
            // 
            this.cbRememberDeletionStats.AutoSize = true;
            this.cbRememberDeletionStats.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbRememberDeletionStats.Location = new System.Drawing.Point(102, 65);
            this.cbRememberDeletionStats.Name = "cbRememberDeletionStats";
            this.cbRememberDeletionStats.Size = new System.Drawing.Size(92, 17);
            this.cbRememberDeletionStats.TabIndex = 26;
            this.cbRememberDeletionStats.Text = "Deletion Stats";
            this.cbRememberDeletionStats.UseVisualStyleBackColor = true;
            // 
            // cbRememberLastUsedDirectory
            // 
            this.cbRememberLastUsedDirectory.AutoSize = true;
            this.cbRememberLastUsedDirectory.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbRememberLastUsedDirectory.Location = new System.Drawing.Point(75, 42);
            this.cbRememberLastUsedDirectory.Name = "cbRememberLastUsedDirectory";
            this.cbRememberLastUsedDirectory.Size = new System.Drawing.Size(119, 17);
            this.cbRememberLastUsedDirectory.TabIndex = 25;
            this.cbRememberLastUsedDirectory.Text = "Last Used Directory";
            this.cbRememberLastUsedDirectory.UseVisualStyleBackColor = true;
            // 
            // cbRememberWindowDetails
            // 
            this.cbRememberWindowDetails.AutoSize = true;
            this.cbRememberWindowDetails.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbRememberWindowDetails.Location = new System.Drawing.Point(41, 19);
            this.cbRememberWindowDetails.Name = "cbRememberWindowDetails";
            this.cbRememberWindowDetails.Size = new System.Drawing.Size(153, 17);
            this.cbRememberWindowDetails.TabIndex = 24;
            this.cbRememberWindowDetails.Text = "Window Size and Location";
            this.cbRememberWindowDetails.UseVisualStyleBackColor = true;
            // 
            // lbnuPause2
            // 
            this.lbnuPause2.AutoSize = true;
            this.lbnuPause2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbnuPause2.Location = new System.Drawing.Point(71, 99);
            this.lbnuPause2.Name = "lbnuPause2";
            this.lbnuPause2.Size = new System.Drawing.Size(314, 13);
            this.lbnuPause2.TabIndex = 18;
            this.lbnuPause2.Text = "This gives you time to stop the process but is not really necessary";
            // 
            // lbFolderAge2
            // 
            this.lbFolderAge2.AutoSize = true;
            this.lbFolderAge2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbFolderAge2.Location = new System.Drawing.Point(71, 156);
            this.lbFolderAge2.Name = "lbFolderAge2";
            this.lbFolderAge2.Size = new System.Drawing.Size(246, 13);
            this.lbFolderAge2.TabIndex = 21;
            this.lbFolderAge2.Text = "This allows you to ignore freshly created directories";
            // 
            // lbMaxDepth2
            // 
            this.lbMaxDepth2.AutoSize = true;
            this.lbMaxDepth2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbMaxDepth2.Location = new System.Drawing.Point(71, 43);
            this.lbMaxDepth2.Name = "lbMaxDepth2";
            this.lbMaxDepth2.Size = new System.Drawing.Size(313, 13);
            this.lbMaxDepth2.TabIndex = 15;
            this.lbMaxDepth2.Text = "RED will only able to find empty directories that are N levels deep";
            // 
            // lbFolderAge1
            // 
            this.lbFolderAge1.AutoSize = true;
            this.lbFolderAge1.Location = new System.Drawing.Point(71, 133);
            this.lbFolderAge1.Name = "lbFolderAge1";
            this.lbFolderAge1.Size = new System.Drawing.Size(181, 13);
            this.lbFolderAge1.TabIndex = 20;
            this.lbFolderAge1.Text = "Skip directories less than N hours old";
            this.uxToolTips.SetToolTip(this.lbFolderAge1, "This allows you to ignore freshly created directories");
            // 
            // nuFolderAge
            // 
            this.nuFolderAge.Location = new System.Drawing.Point(4, 131);
            this.nuFolderAge.Margin = new System.Windows.Forms.Padding(2);
            this.nuFolderAge.Maximum = new decimal(new int[] {
            96,
            0,
            0,
            0});
            this.nuFolderAge.Name = "nuFolderAge";
            this.nuFolderAge.Size = new System.Drawing.Size(53, 20);
            this.nuFolderAge.TabIndex = 19;
            // 
            // lbPause1
            // 
            this.lbPause1.AutoSize = true;
            this.lbPause1.Location = new System.Drawing.Point(71, 78);
            this.lbPause1.Name = "lbPause1";
            this.lbPause1.Size = new System.Drawing.Size(224, 13);
            this.lbPause1.TabIndex = 17;
            this.lbPause1.Text = "Pause between each deletion (in milliseconds)";
            this.uxToolTips.SetToolTip(this.lbPause1, "This gives you time to stop the process but is not really necessary");
            // 
            // nuPause
            // 
            this.nuPause.Location = new System.Drawing.Point(6, 76);
            this.nuPause.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nuPause.Name = "nuPause";
            this.nuPause.Size = new System.Drawing.Size(53, 20);
            this.nuPause.TabIndex = 16;
            // 
            // nuMaxDepth
            // 
            this.nuMaxDepth.Location = new System.Drawing.Point(6, 19);
            this.nuMaxDepth.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nuMaxDepth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nuMaxDepth.Name = "nuMaxDepth";
            this.nuMaxDepth.Size = new System.Drawing.Size(53, 20);
            this.nuMaxDepth.TabIndex = 13;
            this.nuMaxDepth.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // lbInfiniteLoopDetectionCount
            // 
            this.lbInfiniteLoopDetectionCount.AutoSize = true;
            this.lbInfiniteLoopDetectionCount.Location = new System.Drawing.Point(71, 196);
            this.lbInfiniteLoopDetectionCount.Name = "lbInfiniteLoopDetectionCount";
            this.lbInfiniteLoopDetectionCount.Size = new System.Drawing.Size(200, 13);
            this.lbInfiniteLoopDetectionCount.TabIndex = 23;
            this.lbInfiniteLoopDetectionCount.Text = "Infinite-loop detection: Stop after N errors";
            // 
            // lbMaxDepth1
            // 
            this.lbMaxDepth1.AutoSize = true;
            this.lbMaxDepth1.Location = new System.Drawing.Point(71, 21);
            this.lbMaxDepth1.Name = "lbMaxDepth1";
            this.lbMaxDepth1.Size = new System.Drawing.Size(197, 13);
            this.lbMaxDepth1.TabIndex = 14;
            this.lbMaxDepth1.Text = "Max directory nesting depth (-1 = infinite)";
            this.uxToolTips.SetToolTip(this.lbMaxDepth1, "RED will only able to find empty directories that are N levels deep");
            // 
            // nuInfiniteLoopDetectionCount
            // 
            this.nuInfiniteLoopDetectionCount.Location = new System.Drawing.Point(4, 194);
            this.nuInfiniteLoopDetectionCount.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nuInfiniteLoopDetectionCount.Name = "nuInfiniteLoopDetectionCount";
            this.nuInfiniteLoopDetectionCount.Size = new System.Drawing.Size(53, 20);
            this.nuInfiniteLoopDetectionCount.TabIndex = 22;
            this.nuInfiniteLoopDetectionCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // gbExplorerIntegration
            // 
            this.gbExplorerIntegration.Controls.Add(this.chkExplorerIntegrateAutoSearch);
            this.gbExplorerIntegration.Controls.Add(this.lblExplorerIntegrationInfo);
            this.gbExplorerIntegration.Controls.Add(this.btnExplorerRemove);
            this.gbExplorerIntegration.Controls.Add(this.btnExplorerIntegrate);
            this.gbExplorerIntegration.Controls.Add(this.lbExplorerIntegration1);
            this.gbExplorerIntegration.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbExplorerIntegration.Location = new System.Drawing.Point(3, 286);
            this.gbExplorerIntegration.Name = "gbExplorerIntegration";
            this.gbExplorerIntegration.Size = new System.Drawing.Size(636, 71);
            this.gbExplorerIntegration.TabIndex = 2;
            this.gbExplorerIntegration.TabStop = false;
            this.gbExplorerIntegration.Text = "Windows Explorer Integration";
            // 
            // chkExplorerIntegrateAutoSearch
            // 
            this.chkExplorerIntegrateAutoSearch.AutoSize = true;
            this.chkExplorerIntegrateAutoSearch.Location = new System.Drawing.Point(378, 48);
            this.chkExplorerIntegrateAutoSearch.Name = "chkExplorerIntegrateAutoSearch";
            this.chkExplorerIntegrateAutoSearch.Size = new System.Drawing.Size(85, 17);
            this.chkExplorerIntegrateAutoSearch.TabIndex = 2;
            this.chkExplorerIntegrateAutoSearch.Text = "Auto Search";
            this.chkExplorerIntegrateAutoSearch.UseVisualStyleBackColor = true;
            // 
            // lblExplorerIntegrationInfo
            // 
            this.lblExplorerIntegrationInfo.AutoSize = true;
            this.lblExplorerIntegrationInfo.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblExplorerIntegrationInfo.Location = new System.Drawing.Point(16, 49);
            this.lblExplorerIntegrationInfo.Name = "lblExplorerIntegrationInfo";
            this.lblExplorerIntegrationInfo.Size = new System.Drawing.Size(124, 13);
            this.lblExplorerIntegrationInfo.TabIndex = 1;
            this.lblExplorerIntegrationInfo.Text = "This is a Per User setting";
            // 
            // btnExplorerRemove
            // 
            this.btnExplorerRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExplorerRemove.Location = new System.Drawing.Point(499, 24);
            this.btnExplorerRemove.Name = "btnExplorerRemove";
            this.btnExplorerRemove.Size = new System.Drawing.Size(119, 23);
            this.btnExplorerRemove.TabIndex = 4;
            this.btnExplorerRemove.Text = "Uninstall";
            this.btnExplorerRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExplorerRemove.UseVisualStyleBackColor = true;
            this.btnExplorerRemove.Click += new System.EventHandler(this.btnExplorerRemove_Click);
            // 
            // btnExplorerIntegrate
            // 
            this.btnExplorerIntegrate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExplorerIntegrate.Location = new System.Drawing.Point(374, 24);
            this.btnExplorerIntegrate.Name = "btnExplorerIntegrate";
            this.btnExplorerIntegrate.Size = new System.Drawing.Size(119, 23);
            this.btnExplorerIntegrate.TabIndex = 3;
            this.btnExplorerIntegrate.Text = "Install";
            this.btnExplorerIntegrate.UseVisualStyleBackColor = true;
            this.btnExplorerIntegrate.Click += new System.EventHandler(this.btnExplorerIntegrate_Click);
            // 
            // lbExplorerIntegration1
            // 
            this.lbExplorerIntegration1.AutoSize = true;
            this.lbExplorerIntegration1.Location = new System.Drawing.Point(16, 24);
            this.lbExplorerIntegration1.Name = "lbExplorerIntegration1";
            this.lbExplorerIntegration1.Size = new System.Drawing.Size(274, 13);
            this.lbExplorerIntegration1.TabIndex = 0;
            this.lbExplorerIntegration1.Text = "Integrate RED+ into the Windows Explorer context menu";
            // 
            // gbAdvancedExtras
            // 
            this.gbAdvancedExtras.Controls.Add(this.btnResetFilters);
            this.gbAdvancedExtras.Controls.Add(this.btnCopyDebugInfo);
            this.gbAdvancedExtras.Controls.Add(this.btnResetConfig);
            this.gbAdvancedExtras.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbAdvancedExtras.Location = new System.Drawing.Point(3, 357);
            this.gbAdvancedExtras.Name = "gbAdvancedExtras";
            this.gbAdvancedExtras.Size = new System.Drawing.Size(636, 46);
            this.gbAdvancedExtras.TabIndex = 3;
            this.gbAdvancedExtras.TabStop = false;
            // 
            // btnResetFilters
            // 
            this.btnResetFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetFilters.Image = ((System.Drawing.Image)(resources.GetObject("btnResetFilters.Image")));
            this.btnResetFilters.Location = new System.Drawing.Point(246, 13);
            this.btnResetFilters.Name = "btnResetFilters";
            this.btnResetFilters.Size = new System.Drawing.Size(183, 27);
            this.btnResetFilters.TabIndex = 1;
            this.btnResetFilters.Text = "Reset Filters to Defaults";
            this.btnResetFilters.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnResetFilters.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnResetFilters.UseVisualStyleBackColor = true;
            this.btnResetFilters.Click += new System.EventHandler(this.btnResetFilters_Click);
            // 
            // btnCopyDebugInfo
            // 
            this.btnCopyDebugInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyDebugInfo.Location = new System.Drawing.Point(6, 13);
            this.btnCopyDebugInfo.Name = "btnCopyDebugInfo";
            this.btnCopyDebugInfo.Size = new System.Drawing.Size(234, 27);
            this.btnCopyDebugInfo.TabIndex = 0;
            this.btnCopyDebugInfo.Text = "Copy Debug Information (to clipboard)";
            this.btnCopyDebugInfo.UseVisualStyleBackColor = true;
            this.btnCopyDebugInfo.Click += new System.EventHandler(this.btnCopyDebugInfo_Click);
            // 
            // btnResetConfig
            // 
            this.btnResetConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetConfig.Image = ((System.Drawing.Image)(resources.GetObject("btnResetConfig.Image")));
            this.btnResetConfig.Location = new System.Drawing.Point(435, 13);
            this.btnResetConfig.Name = "btnResetConfig";
            this.btnResetConfig.Size = new System.Drawing.Size(183, 27);
            this.btnResetConfig.TabIndex = 2;
            this.btnResetConfig.Text = "Reset Settings to Defaults";
            this.btnResetConfig.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnResetConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnResetConfig.UseVisualStyleBackColor = true;
            this.btnResetConfig.Click += new System.EventHandler(this.btnResetConfig_Click);
            // 
            // tabFilters
            // 
            this.tabFilters.Controls.Add(this.tcFilters);
            this.tabFilters.ImageKey = "filter";
            this.tabFilters.Location = new System.Drawing.Point(4, 27);
            this.tabFilters.Name = "tabFilters";
            this.tabFilters.Padding = new System.Windows.Forms.Padding(3);
            this.tabFilters.Size = new System.Drawing.Size(656, 439);
            this.tabFilters.TabIndex = 5;
            this.tabFilters.Text = "Filters";
            this.tabFilters.UseVisualStyleBackColor = true;
            // 
            // tcFilters
            // 
            this.tcFilters.Controls.Add(this.tabFilterFoldersIgnore);
            this.tcFilters.Controls.Add(this.tabFilterFoldersNeverEmpty);
            this.tcFilters.Controls.Add(this.tabFiltersFilesIgnore);
            this.tcFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcFilters.ImageList = this.ilFolderIcons;
            this.tcFilters.Location = new System.Drawing.Point(3, 3);
            this.tcFilters.Name = "tcFilters";
            this.tcFilters.SelectedIndex = 0;
            this.tcFilters.Size = new System.Drawing.Size(650, 433);
            this.tcFilters.TabIndex = 0;
            // 
            // tabFilterFoldersIgnore
            // 
            this.tabFilterFoldersIgnore.Controls.Add(this.flIgnoreFolders);
            this.tabFilterFoldersIgnore.Controls.Add(this.gbIgnoreFolders);
            this.tabFilterFoldersIgnore.ImageKey = "filter";
            this.tabFilterFoldersIgnore.Location = new System.Drawing.Point(4, 23);
            this.tabFilterFoldersIgnore.Name = "tabFilterFoldersIgnore";
            this.tabFilterFoldersIgnore.Padding = new System.Windows.Forms.Padding(3);
            this.tabFilterFoldersIgnore.Size = new System.Drawing.Size(642, 406);
            this.tabFilterFoldersIgnore.TabIndex = 0;
            this.tabFilterFoldersIgnore.Text = "Directories: Ignore";
            this.tabFilterFoldersIgnore.UseVisualStyleBackColor = true;
            // 
            // flIgnoreFolders
            // 
            this.flIgnoreFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flIgnoreFolders.Location = new System.Drawing.Point(3, 74);
            this.flIgnoreFolders.Name = "flIgnoreFolders";
            this.flIgnoreFolders.Size = new System.Drawing.Size(636, 329);
            this.flIgnoreFolders.TabIndex = 0;
            // 
            // gbIgnoreFolders
            // 
            this.gbIgnoreFolders.Controls.Add(this.lbIgnoreFolders1);
            this.gbIgnoreFolders.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbIgnoreFolders.Location = new System.Drawing.Point(3, 3);
            this.gbIgnoreFolders.Name = "gbIgnoreFolders";
            this.gbIgnoreFolders.Size = new System.Drawing.Size(636, 71);
            this.gbIgnoreFolders.TabIndex = 1;
            this.gbIgnoreFolders.TabStop = false;
            // 
            // lbIgnoreFolders1
            // 
            this.lbIgnoreFolders1.AutoSize = true;
            this.lbIgnoreFolders1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbIgnoreFolders1.Location = new System.Drawing.Point(3, 16);
            this.lbIgnoreFolders1.Name = "lbIgnoreFolders1";
            this.lbIgnoreFolders1.Size = new System.Drawing.Size(415, 52);
            this.lbIgnoreFolders1.TabIndex = 0;
            this.lbIgnoreFolders1.Text = "When a directory matches an item in this list it will be ignored\r\nAny Sub-Directo" +
    "ries will also be ignored\r\n\r\nYou can specify any valid full or partial path (exa" +
    "mples: DIRNAME, C:\\DIR, FOO\\BAR)";
            // 
            // tabFilterFoldersNeverEmpty
            // 
            this.tabFilterFoldersNeverEmpty.Controls.Add(this.flNeverEmptyFolders);
            this.tabFilterFoldersNeverEmpty.Controls.Add(this.gbIgnoreDirsNeverEmpty);
            this.tabFilterFoldersNeverEmpty.ImageKey = "filter";
            this.tabFilterFoldersNeverEmpty.Location = new System.Drawing.Point(4, 23);
            this.tabFilterFoldersNeverEmpty.Name = "tabFilterFoldersNeverEmpty";
            this.tabFilterFoldersNeverEmpty.Padding = new System.Windows.Forms.Padding(3);
            this.tabFilterFoldersNeverEmpty.Size = new System.Drawing.Size(642, 406);
            this.tabFilterFoldersNeverEmpty.TabIndex = 1;
            this.tabFilterFoldersNeverEmpty.Text = "Directories: Never Empty";
            this.tabFilterFoldersNeverEmpty.UseVisualStyleBackColor = true;
            // 
            // flNeverEmptyFolders
            // 
            this.flNeverEmptyFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flNeverEmptyFolders.Location = new System.Drawing.Point(3, 74);
            this.flNeverEmptyFolders.Name = "flNeverEmptyFolders";
            this.flNeverEmptyFolders.Size = new System.Drawing.Size(636, 329);
            this.flNeverEmptyFolders.TabIndex = 0;
            // 
            // gbIgnoreDirsNeverEmpty
            // 
            this.gbIgnoreDirsNeverEmpty.Controls.Add(this.lbIgnoreDirsNeverEmpty1);
            this.gbIgnoreDirsNeverEmpty.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbIgnoreDirsNeverEmpty.Location = new System.Drawing.Point(3, 3);
            this.gbIgnoreDirsNeverEmpty.Name = "gbIgnoreDirsNeverEmpty";
            this.gbIgnoreDirsNeverEmpty.Size = new System.Drawing.Size(636, 71);
            this.gbIgnoreDirsNeverEmpty.TabIndex = 1;
            this.gbIgnoreDirsNeverEmpty.TabStop = false;
            // 
            // lbIgnoreDirsNeverEmpty1
            // 
            this.lbIgnoreDirsNeverEmpty1.AutoSize = true;
            this.lbIgnoreDirsNeverEmpty1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbIgnoreDirsNeverEmpty1.Location = new System.Drawing.Point(3, 16);
            this.lbIgnoreDirsNeverEmpty1.Name = "lbIgnoreDirsNeverEmpty1";
            this.lbIgnoreDirsNeverEmpty1.Size = new System.Drawing.Size(355, 26);
            this.lbIgnoreDirsNeverEmpty1.TabIndex = 0;
            this.lbIgnoreDirsNeverEmpty1.Text = "When a directory matches an item in this list it will be treated as non-empty\r\nAn" +
    "y sub-directories will be checked";
            // 
            // tabFiltersFilesIgnore
            // 
            this.tabFiltersFilesIgnore.Controls.Add(this.flIgnoreFiles);
            this.tabFiltersFilesIgnore.Controls.Add(this.gbIgnoreFiles);
            this.tabFiltersFilesIgnore.Location = new System.Drawing.Point(4, 23);
            this.tabFiltersFilesIgnore.Name = "tabFiltersFilesIgnore";
            this.tabFiltersFilesIgnore.Padding = new System.Windows.Forms.Padding(3);
            this.tabFiltersFilesIgnore.Size = new System.Drawing.Size(642, 406);
            this.tabFiltersFilesIgnore.TabIndex = 3;
            this.tabFiltersFilesIgnore.Text = "Files: Ignore";
            this.tabFiltersFilesIgnore.UseVisualStyleBackColor = true;
            // 
            // flIgnoreFiles
            // 
            this.flIgnoreFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flIgnoreFiles.Location = new System.Drawing.Point(3, 74);
            this.flIgnoreFiles.Name = "flIgnoreFiles";
            this.flIgnoreFiles.Size = new System.Drawing.Size(636, 329);
            this.flIgnoreFiles.TabIndex = 0;
            // 
            // gbIgnoreFiles
            // 
            this.gbIgnoreFiles.Controls.Add(this.picWarning1);
            this.gbIgnoreFiles.Controls.Add(this.lbIgnoreFiles2);
            this.gbIgnoreFiles.Controls.Add(this.lbIgnoreFiles1);
            this.gbIgnoreFiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbIgnoreFiles.Location = new System.Drawing.Point(3, 3);
            this.gbIgnoreFiles.Name = "gbIgnoreFiles";
            this.gbIgnoreFiles.Size = new System.Drawing.Size(636, 71);
            this.gbIgnoreFiles.TabIndex = 1;
            this.gbIgnoreFiles.TabStop = false;
            // 
            // picWarning1
            // 
            this.picWarning1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picWarning1.Image = ((System.Drawing.Image)(resources.GetObject("picWarning1.Image")));
            this.picWarning1.Location = new System.Drawing.Point(385, 14);
            this.picWarning1.Name = "picWarning1";
            this.picWarning1.Size = new System.Drawing.Size(24, 24);
            this.picWarning1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picWarning1.TabIndex = 17;
            this.picWarning1.TabStop = false;
            // 
            // lbIgnoreFiles2
            // 
            this.lbIgnoreFiles2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbIgnoreFiles2.AutoSize = true;
            this.lbIgnoreFiles2.BackColor = System.Drawing.SystemColors.Info;
            this.lbIgnoreFiles2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbIgnoreFiles2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbIgnoreFiles2.Location = new System.Drawing.Point(410, 14);
            this.lbIgnoreFiles2.Name = "lbIgnoreFiles2";
            this.lbIgnoreFiles2.Size = new System.Drawing.Size(218, 41);
            this.lbIgnoreFiles2.TabIndex = 1;
            this.lbIgnoreFiles2.Text = "WARNING! Use this feature with great care.\r\nA bad pattern could potentially cause" +
    "\r\naccidental deletion of important files";
            // 
            // lbIgnoreFiles1
            // 
            this.lbIgnoreFiles1.Location = new System.Drawing.Point(3, 16);
            this.lbIgnoreFiles1.Name = "lbIgnoreFiles1";
            this.lbIgnoreFiles1.Size = new System.Drawing.Size(406, 52);
            this.lbIgnoreFiles1.TabIndex = 0;
            this.lbIgnoreFiles1.Text = resources.GetString("lbIgnoreFiles1.Text");
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.gbAbout);
            this.tabAbout.ImageKey = "info";
            this.tabAbout.Location = new System.Drawing.Point(4, 27);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Size = new System.Drawing.Size(656, 439);
            this.tabAbout.TabIndex = 2;
            this.tabAbout.Text = "About";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // gbAbout
            // 
            this.gbAbout.Controls.Add(this.btnHelp);
            this.gbAbout.Controls.Add(this.lbNotBobInfoBuild);
            this.gbAbout.Controls.Add(this.lbAppTitle);
            this.gbAbout.Controls.Add(this.lblRedStats);
            this.gbAbout.Controls.Add(this.lbCreatedBy);
            this.gbAbout.Controls.Add(this.picAboutLogo);
            this.gbAbout.Controls.Add(this.linkLabelProjectHomepage);
            this.gbAbout.Controls.Add(this.linkLabelJonasJohnRed);
            this.gbAbout.Controls.Add(this.linkLabelFeedback);
            this.gbAbout.Controls.Add(this.txtHelp);
            this.gbAbout.Controls.Add(this.linkLabelCheckForUpdates);
            this.gbAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAbout.Location = new System.Drawing.Point(0, 0);
            this.gbAbout.Name = "gbAbout";
            this.gbAbout.Size = new System.Drawing.Size(656, 439);
            this.gbAbout.TabIndex = 0;
            this.gbAbout.TabStop = false;
            // 
            // btnHelp
            // 
            this.btnHelp.Image = global::RED.Properties.Resources.x16_help1;
            this.btnHelp.Location = new System.Drawing.Point(572, 47);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(73, 23);
            this.btnHelp.TabIndex = 0;
            this.btnHelp.Text = "&Help";
            this.btnHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // lbNotBobInfoBuild
            // 
            this.lbNotBobInfoBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNotBobInfoBuild.Enabled = false;
            this.lbNotBobInfoBuild.Location = new System.Drawing.Point(377, 17);
            this.lbNotBobInfoBuild.Name = "lbNotBobInfoBuild";
            this.lbNotBobInfoBuild.Size = new System.Drawing.Size(268, 14);
            this.lbNotBobInfoBuild.TabIndex = 4;
            this.lbNotBobInfoBuild.Text = "the build time goes here";
            this.lbNotBobInfoBuild.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbAppTitle
            // 
            this.lbAppTitle.AutoSize = true;
            this.lbAppTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAppTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbAppTitle.Location = new System.Drawing.Point(92, 16);
            this.lbAppTitle.Name = "lbAppTitle";
            this.lbAppTitle.Size = new System.Drawing.Size(49, 15);
            this.lbAppTitle.TabIndex = 1;
            this.lbAppTitle.Text = "version";
            // 
            // lblRedStats
            // 
            this.lblRedStats.AutoSize = true;
            this.lblRedStats.Location = new System.Drawing.Point(92, 57);
            this.lblRedStats.Name = "lblRedStats";
            this.lblRedStats.Size = new System.Drawing.Size(165, 13);
            this.lblRedStats.TabIndex = 3;
            this.lblRedStats.Text = "{Deleted N dirs placeholder label}";
            // 
            // lbCreatedBy
            // 
            this.lbCreatedBy.AutoSize = true;
            this.lbCreatedBy.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCreatedBy.Location = new System.Drawing.Point(92, 37);
            this.lbCreatedBy.Name = "lbCreatedBy";
            this.lbCreatedBy.Size = new System.Drawing.Size(142, 14);
            this.lbCreatedBy.TabIndex = 2;
            this.lbCreatedBy.Text = "Based on Jonas John\'s RED";
            // 
            // picAboutLogo
            // 
            this.picAboutLogo.Image = ((System.Drawing.Image)(resources.GetObject("picAboutLogo.Image")));
            this.picAboutLogo.Location = new System.Drawing.Point(6, 2);
            this.picAboutLogo.Name = "picAboutLogo";
            this.picAboutLogo.Size = new System.Drawing.Size(80, 80);
            this.picAboutLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAboutLogo.TabIndex = 35;
            this.picAboutLogo.TabStop = false;
            // 
            // linkLabelProjectHomepage
            // 
            this.linkLabelProjectHomepage.AutoSize = true;
            this.linkLabelProjectHomepage.Location = new System.Drawing.Point(3, 82);
            this.linkLabelProjectHomepage.Name = "linkLabelProjectHomepage";
            this.linkLabelProjectHomepage.Size = new System.Drawing.Size(93, 13);
            this.linkLabelProjectHomepage.TabIndex = 5;
            this.linkLabelProjectHomepage.TabStop = true;
            this.linkLabelProjectHomepage.Text = "Project homepage\r\n";
            this.linkLabelProjectHomepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelProjectHomepage_LinkClicked);
            // 
            // linkLabelJonasJohnRed
            // 
            this.linkLabelJonasJohnRed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelJonasJohnRed.AutoSize = true;
            this.linkLabelJonasJohnRed.Location = new System.Drawing.Point(515, 82);
            this.linkLabelJonasJohnRed.Name = "linkLabelJonasJohnRed";
            this.linkLabelJonasJohnRed.Size = new System.Drawing.Size(130, 13);
            this.linkLabelJonasJohnRed.TabIndex = 8;
            this.linkLabelJonasJohnRed.TabStop = true;
            this.linkLabelJonasJohnRed.Text = "Jonas John\'s Orignal RED";
            this.linkLabelJonasJohnRed.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelJonasJohnRed_LinkClicked);
            // 
            // linkLabelFeedback
            // 
            this.linkLabelFeedback.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelFeedback.AutoSize = true;
            this.linkLabelFeedback.Location = new System.Drawing.Point(250, 82);
            this.linkLabelFeedback.Name = "linkLabelFeedback";
            this.linkLabelFeedback.Size = new System.Drawing.Size(128, 13);
            this.linkLabelFeedback.TabIndex = 7;
            this.linkLabelFeedback.TabStop = true;
            this.linkLabelFeedback.Text = "Feedback / Report a bug";
            this.linkLabelFeedback.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelFeedback_LinkClicked);
            // 
            // txtHelp
            // 
            this.txtHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHelp.Location = new System.Drawing.Point(9, 109);
            this.txtHelp.Multiline = true;
            this.txtHelp.Name = "txtHelp";
            this.txtHelp.ReadOnly = true;
            this.txtHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHelp.Size = new System.Drawing.Size(639, 324);
            this.txtHelp.TabIndex = 9;
            this.txtHelp.Text = "[placeholder for help text]";
            this.txtHelp.WordWrap = false;
            // 
            // linkLabelCheckForUpdates
            // 
            this.linkLabelCheckForUpdates.AutoSize = true;
            this.linkLabelCheckForUpdates.Location = new System.Drawing.Point(129, 82);
            this.linkLabelCheckForUpdates.Name = "linkLabelCheckForUpdates";
            this.linkLabelCheckForUpdates.Size = new System.Drawing.Size(94, 13);
            this.linkLabelCheckForUpdates.TabIndex = 6;
            this.linkLabelCheckForUpdates.TabStop = true;
            this.linkLabelCheckForUpdates.Text = "Check for updates";
            this.linkLabelCheckForUpdates.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelCheckForUpdates_LinkClicked);
            // 
            // stausStripMain
            // 
            this.stausStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbUiStatus,
            this.pbProgressStatus,
            this.lbStatus});
            this.stausStripMain.Location = new System.Drawing.Point(0, 502);
            this.stausStripMain.Name = "stausStripMain";
            this.stausStripMain.Size = new System.Drawing.Size(664, 24);
            this.stausStripMain.TabIndex = 2;
            this.stausStripMain.Text = "statusStrip1";
            // 
            // lbUiStatus
            // 
            this.lbUiStatus.AutoSize = false;
            this.lbUiStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lbUiStatus.DoubleClickEnabled = true;
            this.lbUiStatus.Name = "lbUiStatus";
            this.lbUiStatus.Size = new System.Drawing.Size(70, 19);
            this.lbUiStatus.Text = "Initialising...";
            this.lbUiStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbUiStatus.DoubleClick += new System.EventHandler(this.lbUiStatus_DoubleClick);
            // 
            // pbProgressStatus
            // 
            this.pbProgressStatus.MarqueeAnimationSpeed = 0;
            this.pbProgressStatus.Name = "pbProgressStatus";
            this.pbProgressStatus.Size = new System.Drawing.Size(100, 18);
            // 
            // lbStatus
            // 
            this.lbStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lbStatus.Margin = new System.Windows.Forms.Padding(2, 3, 0, 2);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(475, 19);
            this.lbStatus.Spring = true;
            this.lbStatus.Text = "Status Text";
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlActions
            // 
            this.pnlActions.AutoSize = true;
            this.pnlActions.Controls.Add(this.pnlActionsSearch);
            this.pnlActions.Controls.Add(this.uxMenuButtonExtras);
            this.pnlActions.Controls.Add(this.btnExit);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.Location = new System.Drawing.Point(0, 470);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(664, 32);
            this.pnlActions.TabIndex = 1;
            // 
            // pnlActionsSearch
            // 
            this.pnlActionsSearch.Controls.Add(this.btnSearch);
            this.pnlActionsSearch.Controls.Add(this.btnDelete);
            this.pnlActionsSearch.Controls.Add(this.btnCancel);
            this.pnlActionsSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlActionsSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlActionsSearch.Name = "pnlActionsSearch";
            this.pnlActionsSearch.Size = new System.Drawing.Size(429, 32);
            this.pnlActionsSearch.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSize = true;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(7, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(170, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "&Search For Empty Directories";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(191, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(108, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "&Delete Matches";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(313, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // uxMenuButtonExtras
            // 
            this.uxMenuButtonExtras.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uxMenuButtonExtras.Location = new System.Drawing.Point(445, 6);
            this.uxMenuButtonExtras.Menu = this.cmMenuExtras;
            this.uxMenuButtonExtras.Name = "uxMenuButtonExtras";
            this.uxMenuButtonExtras.Size = new System.Drawing.Size(75, 23);
            this.uxMenuButtonExtras.SplitWidth = 0;
            this.uxMenuButtonExtras.TabIndex = 3;
            this.uxMenuButtonExtras.Text = "E&xtras";
            this.uxMenuButtonExtras.UseVisualStyleBackColor = true;
            // 
            // cmMenuExtras
            // 
            this.cmMenuExtras.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemShowLog,
            this.mnuItemExportToFile,
            this.mnuItemExportToClip,
            this.toolStripSeparator5,
            this.mnuItemLanguage});
            this.cmMenuExtras.Name = "cmMenuExtras";
            this.cmMenuExtras.Size = new System.Drawing.Size(255, 98);
            // 
            // mnuItemShowLog
            // 
            this.mnuItemShowLog.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemShowLog.Image")));
            this.mnuItemShowLog.Name = "mnuItemShowLog";
            this.mnuItemShowLog.Size = new System.Drawing.Size(254, 22);
            this.mnuItemShowLog.Text = "Show &Log";
            this.mnuItemShowLog.Click += new System.EventHandler(this.mnuShowLog_Click);
            // 
            // mnuItemExportToFile
            // 
            this.mnuItemExportToFile.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemExportToFile.Image")));
            this.mnuItemExportToFile.Name = "mnuItemExportToFile";
            this.mnuItemExportToFile.Size = new System.Drawing.Size(254, 22);
            this.mnuItemExportToFile.Text = "Export Search Results to &File";
            this.mnuItemExportToFile.Click += new System.EventHandler(this.mnuExportResultsToFile_Click);
            // 
            // mnuItemExportToClip
            // 
            this.mnuItemExportToClip.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemExportToClip.Image")));
            this.mnuItemExportToClip.Name = "mnuItemExportToClip";
            this.mnuItemExportToClip.Size = new System.Drawing.Size(254, 22);
            this.mnuItemExportToClip.Text = "Export Search Results to &Clipboard";
            this.mnuItemExportToClip.Click += new System.EventHandler(this.mnuExportResultsToClipboard_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(251, 6);
            // 
            // mnuItemLanguage
            // 
            this.mnuItemLanguage.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemLanguage.Image")));
            this.mnuItemLanguage.Name = "mnuItemLanguage";
            this.mnuItemLanguage.Size = new System.Drawing.Size(254, 22);
            this.mnuItemLanguage.Text = "&Language";
            this.mnuItemLanguage.Click += new System.EventHandler(this.mnuItemLanguage_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(526, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(127, 23);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "&Exit";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MainWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 526);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.pnlActions);
            this.Controls.Add(this.stausStripMain);
            this.MinimumSize = new System.Drawing.Size(680, 560);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Remove Empty Directories+";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragEnter);
            this.tcMain.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.tabSearch.PerformLayout();
            this.gbFind.ResumeLayout(false);
            this.gbFind.PerformLayout();
            this.pnlIconDesc.ResumeLayout(false);
            this.pnlIconDesc.PerformLayout();
            this.cmTreeview.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.tcSettings.ResumeLayout(false);
            this.tabSettings1.ResumeLayout(false);
            this.gbSettings1a.ResumeLayout(false);
            this.gbSettings1a.PerformLayout();
            this.gbDeleteMode.ResumeLayout(false);
            this.tabSettings2.ResumeLayout(false);
            this.gbSettings2a.ResumeLayout(false);
            this.gbSettings2a.PerformLayout();
            this.gbSettings2r.ResumeLayout(false);
            this.gbSettings2r.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuFolderAge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuPause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuMaxDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuInfiniteLoopDetectionCount)).EndInit();
            this.gbExplorerIntegration.ResumeLayout(false);
            this.gbExplorerIntegration.PerformLayout();
            this.gbAdvancedExtras.ResumeLayout(false);
            this.tabFilters.ResumeLayout(false);
            this.tcFilters.ResumeLayout(false);
            this.tabFilterFoldersIgnore.ResumeLayout(false);
            this.gbIgnoreFolders.ResumeLayout(false);
            this.gbIgnoreFolders.PerformLayout();
            this.tabFilterFoldersNeverEmpty.ResumeLayout(false);
            this.gbIgnoreDirsNeverEmpty.ResumeLayout(false);
            this.gbIgnoreDirsNeverEmpty.PerformLayout();
            this.tabFiltersFilesIgnore.ResumeLayout(false);
            this.gbIgnoreFiles.ResumeLayout(false);
            this.gbIgnoreFiles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWarning1)).EndInit();
            this.tabAbout.ResumeLayout(false);
            this.gbAbout.ResumeLayout(false);
            this.gbAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAboutLogo)).EndInit();
            this.stausStripMain.ResumeLayout(false);
            this.stausStripMain.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            this.pnlActionsSearch.ResumeLayout(false);
            this.pnlActionsSearch.PerformLayout();
            this.cmMenuExtras.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.TabControl tcMain;
		private System.Windows.Forms.TabPage tabAbout;
		private System.Windows.Forms.Label lbAppTitle;
		private System.Windows.Forms.CheckBox cbIgnore0kbFiles;
		private System.Windows.Forms.CheckBox cbIgnoreSystemFolders;
		private System.Windows.Forms.CheckBox cbIgnoreHiddenFolders;
		private System.Windows.Forms.TabPage tabSearch;
		private System.Windows.Forms.TreeView tvSearchResults;
		private System.Windows.Forms.TextBox txtSearchDirectory;
		private System.Windows.Forms.Button btnSearchDirectoryBrowseFor;
		private System.Windows.Forms.Label lblPickAFolder;
        private System.Windows.Forms.LinkLabel linkLabelProjectHomepage;
		private System.Windows.Forms.Label lbCreatedBy;
        private System.Windows.Forms.ImageList ilFolderIcons;
        private System.Windows.Forms.Panel pnlIconDesc;
        private System.Windows.Forms.Label lbIconDesc;
        private System.Windows.Forms.Label lbColorDoNotTouch;
        private System.Windows.Forms.Panel pnlColorDoNoTouch;
        private System.Windows.Forms.Label lbColorToBeDeleted;
        private System.Windows.Forms.Panel pnlColorToBeDeleted;
        private System.Windows.Forms.Label lblRedStats;
        private System.Windows.Forms.Label lbColorProtected;
        private System.Windows.Forms.Panel pnlColorProtected;
        private System.Windows.Forms.ContextMenuStrip cmTreeview;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiProtectDirectoryOnce;
        private System.Windows.Forms.ToolStripMenuItem tsmiUnprotectDirectory;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddToFilterDirectoryIgnore;
        private System.Windows.Forms.CheckBox cbClipboardDetection;
        private System.Windows.Forms.ComboBox cbDeleteMode;
        private System.Windows.Forms.TextBox txtHelp;
        private System.Windows.Forms.LinkLabel linkLabelCheckForUpdates;
        private System.Windows.Forms.CheckBox cbHideDeletionErrors;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteDirectory;
        private System.Windows.Forms.ToolStripMenuItem tsmiSearchOnlyThisDirectory;
        private System.Windows.Forms.CheckBox cbHideScanErrors;
        private System.Windows.Forms.LinkLabel linkLabelFeedback;
		private System.Windows.Forms.CheckBox cbFastSearchMode;
        private System.Windows.Forms.Label lbFastSearchMode;
        private System.Windows.Forms.Label lbIgnore0kbFiles;
        private System.Windows.Forms.GroupBox gbDeleteMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmiExpandAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiCollapseAll;
        private System.Windows.Forms.Label lbClipboardDetection;
        private System.Windows.Forms.Label lbFastModeInfo;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.CheckBox cbHideIgnoredFolders;
        private System.Windows.Forms.Label lbNotBobInfoBuild;
        private System.Windows.Forms.CheckBox cbAutoProtectRoot;
        private System.Windows.Forms.ToolTip uxToolTips;
        private System.Windows.Forms.TabPage tabFilters;
        private System.Windows.Forms.TabControl tcFilters;
        private System.Windows.Forms.TabPage tabFilterFoldersIgnore;
        private System.Windows.Forms.TabPage tabFilterFoldersNeverEmpty;
        private System.Windows.Forms.GroupBox gbIgnoreFolders;
        private UI.UCFilterList flIgnoreFolders;
        private System.Windows.Forms.GroupBox gbIgnoreDirsNeverEmpty;
        private UI.UCFilterList flNeverEmptyFolders;
        private System.Windows.Forms.GroupBox gbFind;
        private System.Windows.Forms.GroupBox gbAbout;
        private System.Windows.Forms.PictureBox picAboutLogo;
        private System.Windows.Forms.TabPage tabFiltersFilesIgnore;
        private UI.UCFilterList flIgnoreFiles;
        private System.Windows.Forms.GroupBox gbIgnoreFiles;
        private System.Windows.Forms.PictureBox picWarning1;
        private System.Windows.Forms.Label lbHorzLine1;
        private System.Windows.Forms.Label lbIgnoreFolders1;
        private System.Windows.Forms.Label lbIgnoreDirsNeverEmpty1;
        private System.Windows.Forms.Label lbIgnoreFiles1;
        private System.Windows.Forms.Label lbIgnoreFiles2;
        private System.Windows.Forms.TabControl tcSettings;
        private System.Windows.Forms.TabPage tabSettings1;
        private System.Windows.Forms.GroupBox gbSettings1a;
        private System.Windows.Forms.TabPage tabSettings2;
        private System.Windows.Forms.GroupBox gbSettings2a;
        private System.Windows.Forms.Label lbnuPause2;
        private System.Windows.Forms.Label lbFolderAge2;
        private System.Windows.Forms.Label lbMaxDepth2;
        private System.Windows.Forms.Label lbFolderAge1;
        private System.Windows.Forms.NumericUpDown nuFolderAge;
        private System.Windows.Forms.Label lbPause1;
        private System.Windows.Forms.NumericUpDown nuPause;
        private System.Windows.Forms.NumericUpDown nuMaxDepth;
        private System.Windows.Forms.Label lbInfiniteLoopDetectionCount;
        private System.Windows.Forms.Label lbMaxDepth1;
        private System.Windows.Forms.NumericUpDown nuInfiniteLoopDetectionCount;
        private System.Windows.Forms.GroupBox gbExplorerIntegration;
        private System.Windows.Forms.Label lblExplorerIntegrationInfo;
        private System.Windows.Forms.Button btnExplorerRemove;
        private System.Windows.Forms.Button btnExplorerIntegrate;
        private System.Windows.Forms.Label lbExplorerIntegration1;
        private System.Windows.Forms.GroupBox gbAdvancedExtras;
        private System.Windows.Forms.Button btnCopyDebugInfo;
        private System.Windows.Forms.Button btnResetConfig;
        private System.Windows.Forms.StatusStrip stausStripMain;
        private System.Windows.Forms.ToolStripStatusLabel lbUiStatus;
        private System.Windows.Forms.ToolStripProgressBar pbProgressStatus;
        private System.Windows.Forms.ToolStripStatusLabel lbStatus;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSearch;
        private UI.UCMenuButton uxMenuButtonExtras;
        private System.Windows.Forms.ContextMenuStrip cmMenuExtras;
        private System.Windows.Forms.ToolStripMenuItem mnuItemShowLog;
        private System.Windows.Forms.ToolStripMenuItem mnuItemExportToFile;
        private System.Windows.Forms.ToolStripMenuItem mnuItemExportToClip;
        private System.Windows.Forms.CheckBox cbRememberWindowDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem mnuItemLanguage;
        private System.Windows.Forms.GroupBox gbSettings2r;
        private System.Windows.Forms.CheckBox cbRememberDeletionStats;
        private System.Windows.Forms.CheckBox cbRememberLastUsedDirectory;
        private System.Windows.Forms.CheckBox cbSavePrompt;
        private System.Windows.Forms.LinkLabel linkLabelJonasJohnRed;
        private System.Windows.Forms.Button btnResetFilters;
        private System.Windows.Forms.Panel pnlActionsSearch;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.CheckBox chkExplorerIntegrateAutoSearch;
    }
}

