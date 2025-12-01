using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Security.Principal;
using System.Windows.Forms;
using Alphaleonis.Win32.Filesystem;
using NotBob.Config;
using NotBob.Lib;
using RED.Config;
using RED.Helper;
using RED.Match;

using TXT = RED.RedGetText;

namespace RED.UI
{
    public partial class MainWindow : Form
    {
        private REDCore Core = null;
        private TreeManager TreeMgr = null;
        private readonly RuntimeData RunData = new RuntimeData();
        private RedConfiguration RedConfig = null;
        private readonly Stopwatch RuntimeWatch = new Stopwatch();
        private bool AutoSearchOnStart = false;

        #region Init methods

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            UseWaitCursor = true;
        }

        /// <summary>
        /// On load
        /// </summary>
        private void MainWindow_Load(object sender, EventArgs e)
        {
            // NotBob - Use new icon, load new config info, initialise translations
            Icon = Properties.Resources.iconProject;
            Text = RedGetText.Red.Title;
            ConfigLoad();

            #region Init RED core

            Core = new REDCore(RunData);

            // Attach events
            Core.OnError += new EventHandler<ErrorEventArgs>(Core_OnError);
            Core.OnCancelled += new EventHandler(Core_OnCancelled);
            Core.OnAborted += new EventHandler(Core_OnAborted);

            Core.OnProgressChanged += new EventHandler<ProgressChangedEventArgs>(Core_OnProgressChanged);
            Core.OnFoundEmptyDirectory += new EventHandler<FoundEmptyDirInfoEventArgs>(Core_OnFoundEmptyDir);
            Core.OnFinishedScanForEmptyDirs += new EventHandler<FinishedScanForEmptyDirsEventArgs>(Core_OnFoundFinishedScanForEmptyDirs);
            Core.OnDeleteProcessChanged += new EventHandler<DeleteProcessUpdateEventArgs>(Core_OnDeleteProcessChanged);
            Core.OnDeleteProcessFinished += new EventHandler<DeleteProcessFinishedEventArgs>(Core_OnDeleteProcessFinished);
            Core.OnDeleteError += new EventHandler<DeletionErrorEventArgs>(Core_OnDeleteError);

            #endregion Init RED core

            // Init tree manager / helper
            TreeMgr = new TreeManager(tvSearchResults, lbFastModeInfo);
            TreeMgr.SetFastMode(RedConfig.Options.FastSearchMode);
            TreeMgr.OnProtectionStatusChanged += new EventHandler<ProtectionStatusChangedEventArgs>(TreeMgr_OnProtectionStatusChanged);
            TreeMgr.OnDeleteRequest += new EventHandler<DeleteRequestFromTreeEventArgs>(TreeMgr_OnDeleteRequest);

            // Populate delete mode item list
            foreach (DeleteModes d in DeleteModeItem.GetList())
            {
                cbDeleteMode.Items.Add(new DeleteModeItem(d));
            }

            // Update labels
            lblRedStats.Text = string.Format("{0}: {1}", TXT.Words.DeletedSoFar, RedConfig.Volatile.CountOfDeletions);
            // NotBob - use file version info rather than product version
            FileVersionInfo vi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            lbAppTitle.Text = string.Format("{0} v{1}", RedGetText.Red.Title, vi.FileVersion.ToString());
#if DEBUG
            lbAppTitle.Text += " (DBUG)";
#endif

            lbStatus.Text = string.Empty;
            // NotBob - Display BuildTime info on the About tab
            DateTime buildTime = RedAssist.GetBuildTime();
            lbNotBobInfoBuild.Text = string.Format("Build Time: {0} {1:MMMM, yyyy} @ {1:HH:mm}", buildTime.Day.ToOrdinal(), buildTime);
            uxToolTips.SetToolTip(picAboutLogo, RedConfig.Filename);

            AdminCheck();

            UpdateContextMenu(cmTreeview, false);
            btnDelete.Enabled = false;

            DrawDirectoryIcons();

            SetProcessActiveLock(false);
            UiProgressBar(false);

            txtHelp.Text = Properties.Resources.help_filters;

            ProcessCommandLineArgs();

            // NotBob - Update the UI from the saved config details
            ConfigToUI();
            ConfigRestoreWindowDetails();
            if (!string.IsNullOrWhiteSpace(RedConfig.Runtime.Volatile.LastUsedDirectory))
            {
                txtSearchDirectory.Text = RedConfig.Runtime.Volatile.LastUsedDirectory;
            }
        }

        /// <summary>
        /// Check if we were started with admin rights
        /// </summary>
        private void AdminCheck()
        {
            WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());

            if (principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                bool isIntegrated = SystemFunctions.IsRegKeyIntegratedIntoWindowsExplorer();

                btnExplorerIntegrate.Enabled = !isIntegrated;
                btnExplorerRemove.Enabled = isIntegrated;

                Text += string.Format(" ({0})", TXT.Words.AdminMode);

                lblReqAdmin.ForeColor = Color.DarkGray;
            }
            else
            {
                gbExplorerIntegration.Enabled = false;

                // Highlight admin info text bold
                // Note: Changed it from red to bold because red looked like an error
                // but actually it's just an info message
                lblReqAdmin.Font = new Font(DefaultFont, FontStyle.Bold);
            }
        }

        /// <summary>
        /// Read and apply command line arguments
        /// </summary>
        private void ProcessCommandLineArgs()
        {
            RedConfig.Runtime.Volatile.LastUsedDirectory = string.Empty;
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
            {
                args[0] = string.Empty;

                // Extract any switches
                int i = 1;
                while (i < args.Length && args[i].StartsWith("-"))
                {
                    if (args[i].ToLower() == "-autosearch")
                    {
                        AutoSearchOnStart = true;
                    }
                    args[i] = string.Empty;
                    i++;
                }
                // Any remaining args are treated as a pathname
                string path = string.Join(string.Empty, args).Replace("\"", string.Empty).Trim();
                if (path.Length > 0)
                {
                    // add ending backslash
                    if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    {
                        path += Path.DirectorySeparatorChar.ToString();
                    }

                    RedConfig.Runtime.Volatile.LastUsedDirectory = path;
                }
                else
                {
                    AutoSearchOnStart = false;
                }
            }
        }

        private void DrawDirectoryIcons()
        {
            #region Set and display folder status icons

            Dictionary<string, string> icons = new Dictionary<string, string>
            {
                { "home", TXT.Words.Root },
                { "folder", TXT.Words.Empty },
                { "folder_trash_files", TXT.Words.ContainsTrash },
                { "folder_hidden", TXT.Words.Hidden },
                { "folder_lock", TXT.Words.Locked },
                { "folder_never_empty", TXT.Words.NeverEmpty },
                { "folder_warning", TXT.Words.Failed },
                { "protected_icon", TXT.Words.Protected },
                { "deleted", TXT.Words.Deleted }
            };

            int xpos = 6;
            int ypos = 30;

            foreach (string key in icons.Keys)
            {
                Image Icon = (Image)ilFolderIcons.Images[key];

                PictureBox picIcon = new PictureBox
                {
                    Image = Icon,
                    Location = new Point(xpos, ypos),
                    Name = "picIcon",
                    Size = new Size(Icon.Width, Icon.Height)
                };

                Label picLabel = new Label
                {
                    Text = icons[key],
                    Location = new Point(xpos + Icon.Width + 2, ypos + 2),
                    Name = "picLabel"
                };

                pnlIconDesc.Controls.Add(picIcon);
                pnlIconDesc.Controls.Add(picLabel);

                ypos += Icon.Height + 6;
            }

            pnlColorDoNoTouch.ForeColor = TreeManager.ColorDoNotTouch;
            pnlColorProtected.ForeColor = TreeManager.ColorProtected;
            pnlColorToBeDeleted.ForeColor = TreeManager.ColortoBeDeleted;

            #endregion Set and display folder status icons
        }

        #endregion Init methods

        #region Step 1: Scan for empty directories

        /// <summary>
        /// Starts the Scan-Progress
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Check given folder
            DirectoryInfo selectedDirectory;
            try
            {
                selectedDirectory = new DirectoryInfo(SanitizeDirectoryName(txtSearchDirectory.Text));

                if (!selectedDirectory.Exists)
                {
                    UiAssist.MsgBoxError(this, TXT.Translate("The path you picked is not a directory, or does not exist"));
                    return;
                }
            }
            catch (Exception ex)
            {
                UiAssist.MsgBoxException(this, TXT.Translate("The given directory caused a problem"), ex);
                return;
            }

            SetProcessActiveLock(true);
            UiProgressBar(true);
            btnDelete.Enabled = false;
            UpdateContextMenu(cmTreeview, false);

            RunData.StartFolder = selectedDirectory;
            UpdateRuntimeDataObject();

            TreeMgr.OnSearchStart(RunData.StartFolder);

            RunData.AddLogSpacer();
            SetStatusAndLogMessage(TXT.Translate("Searching For Empty Directories..."));

            RuntimeWatch.Reset();
            RuntimeWatch.Start();

            tcMain.SelectedTab = tabSearch;

            btnSearch.Enabled = false;
            Core.SearchingForEmptyDirectories();
        }

        private string SanitizeDirectoryName(string dirName)
        {
            string respx = dirName;
            if (!string.IsNullOrWhiteSpace(dirName))
            {
                if (dirName.StartsWith(@"\\") && dirName.EndsWith(@"\"))
                {
                    respx = dirName.TrimEnd(new[] { '\\' });
                }
            }
            return respx;
        }

        private void Core_OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lbStatus.Text = (string)e.UserState;
        }

        private void Core_OnFoundEmptyDir(object sender, FoundEmptyDirInfoEventArgs e)
        {
            TreeMgr.AddOrUpdateDirectoryNode(e.ScanResult.Directory, e.ScanResult.SearchStatus, e.ScanResult.ErrorMessage);
        }

        private void Core_OnFoundFinishedScanForEmptyDirs(object sender, FinishedScanForEmptyDirsEventArgs e)
        {
            // Search finished

            RuntimeWatch.Stop();
            string runtime = string.Format("{0:D2}:{1:D2}.{2:D2}", RuntimeWatch.Elapsed.Minutes, RuntimeWatch.Elapsed.Seconds, RuntimeWatch.Elapsed.Milliseconds);
            SetStatusAndLogMessage(TXT.Translate("Empty Directories Found: {0} (Checked: {1} / Runtime: {2})", e.EmptyFolderCount, e.FolderCount, runtime));

            if (RedConfig.Options.AutoProtectRoot)
            {
                TreeMgr.ProtectRoot();
            }

            UiProgressBar(false, true, e.EmptyFolderCount);
            UpdateContextMenu(cmTreeview, true);
            SetProcessActiveLock(false);
            btnSearch.Enabled = true;
            //btnSearch.Text = TXT.Translate("&Search Again");
            btnDelete.Enabled = (e.EmptyFolderCount > 0);

            TreeMgr.OnSearchFinished();
        }

        #endregion Step 1: Scan for empty directories

        #region Step 2: Delete empty directories

        private void btnDelete_Click(object sender, EventArgs e)
        {
            RunData.AddLogSpacer();
            SetStatusAndLogMessage(TXT.Translate("Started Deletion Process..."));

            UiProgressBar(true, true, RunData.ScanResults.Count);
            UpdateContextMenu(cmTreeview, false);
            SetProcessActiveLock(true);
            btnSearch.Enabled = false;
            btnDelete.Enabled = false;

            UpdateRuntimeDataObject();

            TreeMgr.OnDeletionProcessStart();

            RuntimeWatch.Reset();
            RuntimeWatch.Start();

            Core.StartDeleteProcess();
        }

        private void Core_OnDeleteProcessChanged(object sender, DeleteProcessUpdateEventArgs e)
        {
            switch (e.Status)
            {
                case DirectoryDeletionStatusTypes.Deleted:
                    lbStatus.Text = string.Format("{0} ({1} of {2})", TXT.Translate("Deleting Empty Directories"), e.ProgressStatus + 1, e.FolderCount);
                    TreeMgr.UpdateItemIcon(e.ScanResult, DirectoryIcons.deleted);
                    break;

                case DirectoryDeletionStatusTypes.Protected:
                    TreeMgr.UpdateItemIcon(e.ScanResult, DirectoryIcons.protected_icon);
                    break;

                default:
                    TreeMgr.UpdateItemIcon(e.ScanResult, DirectoryIcons.folder_warning);
                    break;
            }

            pbProgressStatus.Value = e.ProgressStatus;
        }

        private void Core_OnDeleteError(object sender, DeletionErrorEventArgs e)
        {
            DeletionError errorDialog = new DeletionError();

            errorDialog.SetPath(e.Path);
            errorDialog.SetErrorMessage(e.ErrorMessage);

            DialogResult dialogResult = errorDialog.ShowDialog();

            errorDialog.Dispose();

            if (dialogResult == DialogResult.Abort)
            {
                Core.AbortDeletion();
            }
            else
            {
                // Hack: retry means -> ignore all errors
                if (dialogResult == DialogResult.Retry)
                {
                    RunData.HideDeletionErrors = true;
                }

                Core.ContinueDeleteProcess();
            }
        }

        private void Core_OnDeleteProcessFinished(object sender, DeleteProcessFinishedEventArgs e)
        {
            RuntimeWatch.Stop();
            string runtime = string.Format("{0:D2}:{1:D2})", RuntimeWatch.Elapsed.Minutes, RuntimeWatch.Elapsed.Seconds);
            SetStatusAndLogMessage(string.Format(TXT.Translate("Deleted {0} empty directories (Failed: {1}, Skipped: {2}, Runtime: {3})"), e.DeletedFolderCount, e.FailedFolderCount, e.ProtectedCount, runtime));

            UiProgressBar(false);
            SetProcessActiveLock(false);
            btnSearch.Enabled = true;
            btnDelete.Enabled = false;

            // Increase deletion statistics (ignore overflows).
            unchecked
            {
                RedConfig.Runtime.Volatile.CountOfDeletions += e.DeletedFolderCount;
            }
            lblRedStats.Text = string.Format("{0}: {1}", TXT.Words.DeletedSoFar, RedConfig.Volatile.CountOfDeletions + RedConfig.Runtime.Volatile.CountOfDeletions);

            TreeMgr.OnDeletionProcessFinished();
        }

        #endregion Step 2: Delete empty directories

        #region Process core events / callbacks

        private void Core_OnCancelled(object sender, EventArgs e)
        {
            UiProgressBar(false);

            if (Core.CurrentProcessStep == WorkflowSteps.DeleteProcessRunning)
            {
                SetStatusAndLogMessage(TXT.Translate("Delete Process was Cancelled"));
            }
            else
            {
                SetStatusAndLogMessage(TXT.Translate("Process was Cancelled"));
            }

            SetProcessActiveLock(false);
            btnSearch.Enabled = true;
            btnDelete.Enabled = false;

            TreeMgr.OnProcessCancelled();
        }

        private void Core_OnAborted(object sender, EventArgs e)
        {
            UiProgressBar(false);

            if (Core.CurrentProcessStep == WorkflowSteps.DeleteProcessRunning)
            {
                SetStatusAndLogMessage(TXT.Translate("Delete Process was Aborted"));
            }
            else
            {
                SetStatusAndLogMessage(TXT.Translate("Process was Aborted"));
            }

            SetProcessActiveLock(false);
            btnSearch.Enabled = true;
            btnDelete.Enabled = false;

            TreeMgr.OnProcessCancelled();
        }

        private void Core_OnError(object sender, ErrorEventArgs e)
        {
            UiProgressBar(false);
            UiAssist.MsgBoxError(this, string.Format("{0}:{1}{2}", TXT.Words.Error, RedGetText.CrLf2, e.Message));
        }

        #endregion Process core events / callbacks

        #region Tree view related methods

        /// <summary>
        /// User clicks twice on a folder
        /// </summary>
        private void tvSearchResults_DoubleClick(object sender, EventArgs e)
        {
            SystemFunctions.OpenDirectoryWithExplorer(TreeMgr.GetSelectedFolderPath());
        }

        private void tsmiOpenDirectory_Click(object sender, EventArgs e)
        {
            SystemFunctions.OpenDirectoryWithExplorer(TreeMgr.GetSelectedFolderPath());
        }

        private void tsmiSearchOnlyThisDirectory_Click(object sender, EventArgs e)
        {
            txtSearchDirectory.Text = TreeMgr.GetSelectedFolderPath();
            btnSearch.PerformClick();
        }

        private void tsmiProtectDirectoryOnce_Click(object sender, EventArgs e)
        {
            TreeMgr.ProtectSelected();
        }

        private void tsmiUnprotectDirectory_Click(object sender, EventArgs e)
        {
            TreeMgr.UnprotectSelected();
        }

        private void tsmiAddToFilterDirectoryIgnore_Click(object sender, EventArgs e)
        {
            if (tvSearchResults.SelectedNode == null)
            {
                return;
            }

            TreeMgr.ProtectSelected();
            RedConfig.Filters.DirectoriesToIgnore.Add("+|P|" + ((DirectoryInfo)tvSearchResults.SelectedNode.Tag).FullName);
            ConfigToUI();

            // Focus Directories to Ignore Filter tab
            tcMain.SelectedTab = tabFilters;
            tcFilters.SelectedTab = tabFilterFoldersIgnore;

            // TODO: Update the results + tree to reflect the newly ignored item
            // Current solution: The user has to do a complete rescan
            btnDelete.Enabled = false;
        }

        private void tsmiDeleteDirectory_Click(object sender, EventArgs e)
        {
            TreeMgr.DeleteSelectedDirectory();
        }

        private void tsmiExpandAll_Click(object sender, EventArgs e)
        {
            tvSearchResults.ExpandAll();
        }

        private void tsmiCollapseAll_Click(object sender, EventArgs e)
        {
            tvSearchResults.CollapseAll();
        }

        private void TreeMgr_OnProtectionStatusChanged(object sender, ProtectionStatusChangedEventArgs e)
        {
            if (e.Protected)
            {
                Core.AddProtectedFolder(e.Path);
            }
            else
            {
                Core.RemoveProtected(e.Path);
            }
        }

        private void TreeMgr_OnDeleteRequest(object sender, DeleteRequestFromTreeEventArgs e)
        {
            try
            {
                string deletePath = e.Directory;

                // To simplify the code here there is only the RecycleBinWithQuestion or simulate possible here
                // (all others will be ignored)
                SystemFunctions.ManuallyDeleteDirectory(deletePath, (DeleteModes)RedConfig.Options.DeleteMode);

                // Remove root node
                TreeMgr.RemoveNode(deletePath);

                RunData.AddLogMessage(TXT.Translate("Manually deleted: \"{0}\" including all subdirectories", deletePath));

                // Disable the delete button because the user has to re-scan after he manually deleted a directory
                btnDelete.Enabled = false;
            }
            catch (System.OperationCanceledException)
            {
                // The user canceled the deletion
            }
            catch (Exception ex)
            {
                string emsg = string.Format(TXT.Translate("Could not manually delete \"{0}\" because of the following error"), e.Directory);
                emsg = string.Format("{0}{1}{2}", emsg, RedGetText.CrLf2, ex.Message);
                RunData.AddLogMessage(emsg);
                UiAssist.MsgBoxError(this, emsg);
            }
        }

        private void mnuExportResultsToFile_Click(object sender, EventArgs e)
        {
            //RedAssist.ExportDirectoryList(this.Data, exportToFile: true);
            using (RedExportScanResults export = new RedExportScanResults())
            {
                export.ExportToFile(RunData.ScanResults);
            }
        }

        private void mnuExportResultsToClipboard_Click(object sender, EventArgs e)
        {
            using (RedExportScanResults export = new RedExportScanResults())
            {
                export.ExportToCliboard(RunData.ScanResults);
            }
        }

        #endregion Tree view related methods

        #region GUI related functions / events

        private void SetProcessActiveLock(bool isActive)
        {
            UiBusy(isActive);
            btnCancel.Enabled = isActive;

            uxMenuButtonExtras.Enabled = !isActive;
            tcFilters.Enabled = !isActive;
            tcSettings.Enabled = !isActive;
        }

        private void UiProgressBar(bool isActive, bool isDeleting = false, int maximum = 100)
        {
            pbProgressStatus.Style = isDeleting ? ProgressBarStyle.Blocks : ProgressBarStyle.Marquee;
            pbProgressStatus.MarqueeAnimationSpeed = isDeleting ? 0 : 25;
            pbProgressStatus.Step = 1;
            pbProgressStatus.Minimum = 0;
            pbProgressStatus.Maximum = maximum;
            pbProgressStatus.Value = 0;

            if (isActive)
            {
                pbProgressStatus.Visible = true;
            }
            else
            {
                pbProgressStatus.Visible = false;
                pbProgressStatus.Style = ProgressBarStyle.Blocks;
                pbProgressStatus.MarqueeAnimationSpeed = 0;
            }
        }

        private void UiBusy(bool isBusy)
        {
            UseWaitCursor = isBusy;
            if (isBusy)
            {
                lbUiStatus.Text = TXT.Words.Busy;
            }
            else
            {
                lbUiStatus.Text = TXT.Words.Ready;
            }
        }

        private bool UiIsBusy()
        {
            return UseWaitCursor;
        }

        private void UiClipboardCheck()
        {
            if (!UiIsBusy())
            {
                // Detect paths in the clipboard
                if (cbClipboardDetection.Checked && Clipboard.ContainsText(TextDataFormat.Text))
                {
                    string clipValue = Clipboard.GetText(TextDataFormat.Text);
                    // Remove any leading or trailing quotes
                    clipValue = clipValue.Trim('"');
                    if (!clipValue.Contains("\n"))
                    {
                        if (clipValue.Contains(":" + Path.DirectorySeparatorChar.ToString()))
                        {
                            // add ending backslash
                            if (!clipValue.EndsWith(Path.DirectorySeparatorChar.ToString()))
                            {
                                clipValue += Path.DirectorySeparatorChar.ToString();
                            }
                            if (Directory.Exists(clipValue))
                            {
                                string qmsg = string.Format("{0}{1}{2}", TXT.Translate("Use this value as the start directory?"), RedGetText.CrLf2, clipValue);
                                if (UiAssist.BAskYesNo(qmsg, MessageBoxDefaultButton.Button1))
                                {
                                    RedConfig.Runtime.Volatile.LastUsedDirectory = clipValue;
                                    txtSearchDirectory.Text = clipValue;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Core.CancelCurrentProcess();
        }

        private void SetStatusAndLogMessage(string msg)
        {
            lbStatus.Text = msg;
            RunData.AddLogMessage(msg);
        }

        /// <summary>
        /// Part of the drag & drop functions
        /// (you can drag a folder into RED)
        /// </summary>
        private void MainWindow_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            string dirname = s.Length == 1 ? s[0].Trim() : null;
            if (!string.IsNullOrWhiteSpace(dirname) && Directory.Exists(dirname))
            {
                txtSearchDirectory.Text = dirname;
            }
            else
            {
                UiAssist.MsgBoxError(this, TXT.Translate("Only one directory can be accepted"));
            }
        }

        /// <summary>
        /// Part of the drag & drop functions
        /// (you can drag a folder into RED)
        /// </summary>
        private void MainWindow_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void txtSearchDirectory_Enter(object sender, EventArgs e)
        {
            UiClipboardCheck();
        }

        private void txtSearchDirectory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtSearchDirectory.SelectAll();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Let the user select a folder
        /// </summary>
        private void btnSearchDirectoryBrowseFor_Click(object sender, EventArgs e)
        {
            txtSearchDirectory.Text = SystemFunctions.ChooseDirectoryDialog(RedConfig.Runtime.Volatile.LastUsedDirectory);
        }

        private void mnuShowLog_Click(object sender, EventArgs e)
        {
            LogWindow logWindow = new LogWindow();
            logWindow.SetLog(Core.GetLogMessages());
            logWindow.ShowDialog();
            logWindow.Dispose();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool ask = cbSavePrompt.Checked || ModifierKeys.HasFlag(Keys.Alt);
            ConfigUpdateAndSave(ask);
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            UiBusy(false);
            if (AutoSearchOnStart && txtSearchDirectory.Text.Length > 0)
            {
                AutoSearchOnStart = false;
                btnSearch.PerformClick();
            }
            else
            {
                UiClipboardCheck();
            }
        }

        private void lbUiStatus_DoubleClick(object sender, EventArgs e)
        {
            //ChangeOfLanguage();
        }

        private void mnuItemLanguage_Click(object sender, EventArgs e)
        {
            ChangeOfLanguage();
        }

        // NotBob - Draw a border around ToolStrip buttons
        private void UiToolstripButton_Paint(object sender, PaintEventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, item.Width, item.Height), Color.LightGray, ButtonBorderStyle.Solid);
        }

        private void cbFastSearchMode_CheckedChanged(object sender, EventArgs e)
        {
            lbFastModeInfo.Visible = cbFastSearchMode.Checked; ;
        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlActionsSearch.Enabled = (tcMain.SelectedTab == tabSearch || RedConfig.Options.NoSTAD);

            if (tcMain.SelectedTab == tabSettings)
            {
                if (RedConfig != null && RedConfig.IsReadOnly)
                {
                    btnResetConfig.Enabled = false;
                    lbStatus.Text = "Settings are ReadOnly and cannot be changed";
                }
            }
            else
            {
                lbStatus.Text = "";
            }
        }

        #endregion GUI related functions / events

        #region Config and misc stuff

        private void btnResetFilters_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == UiAssist.MsgBoxYesNo(this, TXT.Translate("Do you really want to reset ALL FILTERS to their default values?")))
            {
                RedConfig.Filters.SetToDefaults();
                ConfigToUI();
            }
        }

        private void btnResetConfig_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == UiAssist.MsgBoxYesNo(this, TXT.Translate("Do you really want to reset ALL Settings and Filters to their default values?")))
            {
                RedConfig.Options.SetToDefaults();
                RedConfig.Filters.SetToDefaults();
                ConfigToUI();
                TreeMgr.SetFastMode(RedConfig.Options.FastSearchMode);
            }
        }

        private void btnExplorerIntegrate_Click(object sender, EventArgs e)
        {
            SystemFunctions.AddOrRemoveRegKey(true);
            btnExplorerRemove.Enabled = true;
            btnExplorerIntegrate.Enabled = false;
        }

        private void btnExplorerRemove_Click(object sender, EventArgs e)
        {
            SystemFunctions.AddOrRemoveRegKey(false);
            btnExplorerRemove.Enabled = false;
            btnExplorerIntegrate.Enabled = true;
        }

        private void linkLabelProjectHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/BookOfBeasts/Remove-Empty-Directories-Plus");
        }

        private void linkLabelJonasJohnRed_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/hxseven/Remove-Empty-Directories");
        }

        private void linkLabelCheckForUpdates_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/BookOfBeasts/Remove-Empty-Directories-Plus");
        }

        private void linkLabelFeedback_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/BookOfBeasts/Remove-Empty-Directories-Plus/issues");
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            if (File.Exists(RedConfig.Runtime.HelpFile))
            {
                Process.Start(RedConfig.Runtime.HelpFile);
            }
            else
            {
                UiAssist.MsgBoxInfo($"Help File Not Found\r\n{RedConfig.Runtime.HelpFile}");
            }
        }

        private void btnCopyDebugInfo_Click(object sender, EventArgs e)
        {
            try
            {
                RedDebug dbug = new RedDebug();
                string info = dbug.GatherDebugInfo(RedConfig);
                Clipboard.SetText(info, TextDataFormat.Text);
                UiAssist.MsgBoxInfo(this, string.Format("{0}{1}{2}", TXT.Translate("Copied this text to your clipboard:"), RedGetText.CrLf2, info));
            }
            catch (Exception ex)
            {
                UiAssist.MsgBoxException(this, TXT.Translate("Could not copy the debug info into your clipboard"), ex);
            }
        }

        private void cmTreeview_Opening(object sender, CancelEventArgs e)
        {
            tsmiOpenFolder.Enabled = tvSearchResults.SelectedNode != null;
        }

        private void UpdateRuntimeDataObject()
        {
            ConfigFromUI();

            RunData.HideDeletionErrors = RedConfig.Options.HideDeletionErrors;
            RunData.HideScanErrors = RedConfig.Options.HideScanErrors;

            RunData.IgnoreEmptyFiles = RedConfig.Options.IgnoreEmptyFiles;
            RunData.IgnoreHiddenFolders = RedConfig.Options.IgnoreHiddenDirectories;
            RunData.IgnoreSystemFolders = RedConfig.Options.IgnoreSystemDirectories;
            RunData.MinFolderAgeHours = RedConfig.Options.MinDirectoryAgeHours;
            RunData.MaxDepth = RedConfig.Options.MaxDirectoryDepth;
            RunData.InfiniteLoopDetectionCount = RedConfig.Options.InfiniteLoopDetectionCount;
            RunData.DeleteMode = (DeleteModes)RedConfig.Options.DeleteMode;
            RunData.PauseTime = RedConfig.Options.PauseBetweenDeletions;
            //NotBob added option to hide ignored directories
            RunData.HideIgnoredDirectories = RedConfig.Options.HideIgnoredDirectories;
            // NotBob use dedicated RedMatchItemLists for all the filters
            RunData.IgnoreFileNameList.Transform(RedConfig.Filters.FilesToIgnore);
            RunData.IgnoreDirectoryNameList.Transform(RedConfig.Filters.DirectoriesToIgnore);
            RunData.NeverEmptyDirectoryList.Transform(RedConfig.Filters.DirectoriesNeverEmpty);
        }

        private void NotBobConfigInfo_DoubleClick(object sender, EventArgs e)
        {
            SystemFunctions.OpenDirectoryWithExplorer(RedConfig.Runtime.ConfigPath);
        }

        /// <summary>
        /// Enables/disables all items in the context menu
        /// </summary>
        /// <param name="contextMenuStrip"></param>
        /// <param name="enable"></param>
        private void UpdateContextMenu(ContextMenuStrip contextMenuStrip, bool enable)
        {
            foreach (ToolStripItem item in contextMenuStrip.Items)
            {
                item.Enabled = enable;
            }
            uxMenuButtonExtras.Enabled = enable;
        }

        #endregion Config and misc stuff

        #region NotBob Config

        private void ConfigLoad()
        {
            ConfigAssist.ConfigLoad(ref RedConfig, "RemoveEmptyDirectories");
            ConfigLanguage(RedConfig.Options.Language);
        }

        private void ConfigUpdateAndSave(bool ask = false)
        {
            ConfigFromUI();
            ConfigAssist.ConfigSaveWithPrompt(RedConfig, ask);
        }

        private void ConfigLanguage(string language)
        {
            try
            {
                string langFolder = RedGetText.GetLanguageFolder(RedConfig.Runtime.ExecutablePath);
                if (Directory.Exists(langFolder))
                {
                    if (RedGetText.LoadLanguage(language, RedConfig.Runtime.ExecutablePath))
                    {
                        RedConfig.Options.Language = language;
                        RedGetText.UI.TranslateControls(this);
                    }
                    else
                    {
                        RedConfig.Options.Language = null;
                    }
                }
                else
                {
                    mnuItemLanguage.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                UiAssist.MsgBoxException(this, TXT.Translate("Error during config initialisation (Runtime)"), ex);
            }
        }

        // Update the UI with current configuration settings
        private void ConfigToUI()
        {
            cbSavePrompt.Checked = RedConfig.Options.SavePrompt;

            cbFastSearchMode.Checked = RedConfig.Options.FastSearchMode;
            lbFastModeInfo.Visible = RedConfig.Options.FastSearchMode;
            TreeMgr.SetFastMode(RedConfig.Options.FastSearchMode);

            cbHideScanErrors.Checked = RedConfig.Options.HideScanErrors;
            cbHideDeletionErrors.Checked = RedConfig.Options.HideDeletionErrors;

            cbAutoProtectRoot.Checked = RedConfig.Options.AutoProtectRoot;
            cbClipboardDetection.Checked = RedConfig.Options.ClipboardPathDetection;
            cbHideIgnoredFolders.Checked = RedConfig.Options.HideIgnoredDirectories;
            cbIgnore0kbFiles.Checked = RedConfig.Options.IgnoreEmptyFiles;
            cbIgnoreHiddenFolders.Checked = RedConfig.Options.IgnoreHiddenDirectories;
            cbIgnoreSystemFolders.Checked = RedConfig.Options.IgnoreSystemDirectories;
            cbDeleteMode.SelectedIndex = (int)RedConfig.Options.DeleteMode;

            nuFolderAge.Value = RedConfig.Options.MinDirectoryAgeHours;
            nuInfiniteLoopDetectionCount.Value = RedConfig.Options.InfiniteLoopDetectionCount;
            nuMaxDepth.Value = RedConfig.Options.MaxDirectoryDepth;
            nuPause.Value = RedConfig.Options.PauseBetweenDeletions;

            flIgnoreFolders.Populate(RedConfig.Filters.DirectoriesToIgnore, RedMatchFilterType.Directory);
            flNeverEmptyFolders.Populate(RedConfig.Filters.DirectoriesNeverEmpty, RedMatchFilterType.Directory);
            flIgnoreFiles.Populate(RedConfig.Filters.FilesToIgnore, RedMatchFilterType.Files);

            txtSearchDirectory.Text = RedConfig.Volatile.LastUsedDirectory;

            cbRememberWindowDetails.Checked = RedConfig.Options.RememberWindowDetails;
            cbRememberLastUsedDirectory.Checked = RedConfig.Options.RememberLastUsedDirectory;
            cbRememberDeletionStats.Checked = RedConfig.Options.RememberDeletionStats;

            if (RedConfig != null && RedConfig.IsReadOnly)
            {
                foreach (GroupBox item in tabSettings1.Controls)
                {
                    item.Enabled = false;
                }
                foreach (GroupBox item in tabSettings2.Controls)
                {
                    item.Enabled = false;
                }
                gbAdvancedExtras.Enabled = true;
                btnResetConfig.Enabled = false;
            }
            btnHelp.Enabled = File.Exists(RedConfig.Runtime.HelpFile);
        }

        private void ConfigRestoreWindowDetails()
        {
            // Ensure that we have valid values before trying to restore them
            if (RedConfig.UI.WinMainLocation.IsEmpty)
            {
                RedConfig.UI.WinMainLocation = Location;
            }
            if (RedConfig.UI.WinMainSize.IsEmpty)
            {
                RedConfig.UI.WinMainSize = Size;
            }
            RedConfig.UI.WinMainLocation = RedAssist.GetScreenValidLocation(RedConfig.UI.WinMainLocation);

            // Restore saved values
            Location = RedConfig.UI.WinMainLocation;
            Size = RedConfig.UI.WinMainSize;

            tcMain.SelectedTab = tabSearch;
        }

        private void ConfigFromUI()
        {
            try
            {
                RedConfig.Options.SavePrompt = cbSavePrompt.Checked;

                RedConfig.Options.FastSearchMode = cbFastSearchMode.Checked;

                RedConfig.Options.AutoProtectRoot = cbAutoProtectRoot.Checked;
                RedConfig.Options.ClipboardPathDetection = cbClipboardDetection.Checked;
                RedConfig.Options.HideIgnoredDirectories = cbHideIgnoredFolders.Checked;
                RedConfig.Options.HideScanErrors = cbHideScanErrors.Checked;
                RedConfig.Options.IgnoreEmptyFiles = cbIgnore0kbFiles.Checked;
                RedConfig.Options.HideDeletionErrors = cbHideDeletionErrors.Checked;
                RedConfig.Options.IgnoreHiddenDirectories = cbIgnoreHiddenFolders.Checked;
                RedConfig.Options.IgnoreSystemDirectories = cbIgnoreSystemFolders.Checked;
                RedConfig.Options.DeleteModeInt = cbDeleteMode.SelectedIndex;

                RedConfig.Options.MinDirectoryAgeHours = (uint)nuFolderAge.Value;
                RedConfig.Options.InfiniteLoopDetectionCount = (int)nuInfiniteLoopDetectionCount.Value;
                RedConfig.Options.MaxDirectoryDepth = (int)nuMaxDepth.Value;
                RedConfig.Options.PauseBetweenDeletions = (int)nuPause.Value;

                RedConfig.Runtime.Volatile.LastUsedDirectory = txtSearchDirectory.Text;

                if (RedAssist.FilterListUpdate(flIgnoreFolders.GetStringList(), RedConfig.Filters.DirectoriesToIgnore))
                {
                    RedConfig.DataIsDirty = true;
                }
                if (RedAssist.FilterListUpdate(flNeverEmptyFolders.GetStringList(), RedConfig.Filters.DirectoriesNeverEmpty))
                {
                    RedConfig.DataIsDirty = true;
                }
                if (RedAssist.FilterListUpdate(flIgnoreFiles.GetStringList(), RedConfig.Filters.FilesToIgnore))
                {
                    RedConfig.DataIsDirty = true;
                }

                RedConfig.Options.RememberDeletionStats = cbRememberDeletionStats.Checked;
                if (RedConfig.Options.RememberDeletionStats)
                {
                    RedConfig.Volatile.CountOfDeletions += RedConfig.Runtime.Volatile.CountOfDeletions;
                }

                // Save UI details if required
                RedConfig.Options.RememberWindowDetails = cbRememberWindowDetails.Checked;
                if (RedConfig.Options.RememberWindowDetails)
                {
                    RedConfig.UI.WinMainLocation = Location;
                    RedConfig.UI.WinMainSize = Size;
                }

                RedConfig.Options.RememberLastUsedDirectory = cbRememberLastUsedDirectory.Checked;
                if (RedConfig.Options.RememberLastUsedDirectory)
                {
                    RedConfig.Volatile.LastUsedDirectory = RedConfig.Runtime.Volatile.LastUsedDirectory;
                }
            }
            catch (Exception ex)
            {
                UiAssist.MsgBoxException(this, TXT.Translate("Error trying to set configuration details"), ex);
            }
        }

        #endregion NotBob Config

        private void ChangeOfLanguage()
        {
            using (FormLanguage frm = new FormLanguage(RedConfig))
            {
                if (DialogResult.OK == frm.ShowDialog(this))
                {
                    ConfigLanguage(frm.Language);
                    btnExit.Text = RedGetText.Words.Exit;
                    UiAssist.MsgBoxInfo(this, TXT.Words.RestartRequired);
                }
            }
        }
    }
}