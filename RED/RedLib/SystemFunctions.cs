using System;
using System.Diagnostics;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using Alphaleonis.Win32.Filesystem;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using RED.Helper;
using FileAccess = System.IO.FileAccess;
using FileMode = System.IO.FileMode;
using FileShare = System.IO.FileShare;
using TXT = RED.RedGetText;

namespace RED
{
    public enum DeleteModes
    {
        RecycleBin = 0,
        RecycleBinShowErrors = 1,
        RecycleBinWithQuestion = 2,
        Direct = 3,
        Simulate = 4
    }

    [Serializable]
    public class REDPermissionDeniedException : Exception
    {
        public REDPermissionDeniedException()
        { }

        public REDPermissionDeniedException(string message) : base(message) { }

        public REDPermissionDeniedException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// A collection of (generic) system functions
    ///
    /// Exception handling should be made by the caller
    /// </summary>
    public class SystemFunctions
    {
        public static void ManuallyDeleteDirectory(string path, DeleteModes deleteMode)
        {
            if (deleteMode == DeleteModes.Simulate)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new Exception(TXT.Translate("Could not delete directory because the path was empty"));
            }

            //TODO: Add FileIOPermission code?

            FileSystem.DeleteDirectory(path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
        }

        public static bool IsDirLocked(string path)
        {
            try
            {
                // UGLY hack to determine whether we have write access
                // to a specific directory

                Random r = new Random();
                string tempName = path + "deltest";

                int counter = 0;
                while (Directory.Exists(tempName))
                {
                    tempName = path + "deltest" + r.Next(0, 9999).ToString();
                    if (counter > 100)
                    {
                        return true; // Something strange is going on... stop here...
                    }

                    counter++;
                }

                Directory.Move(path, tempName);
                Directory.Move(tempName, path);

                return false;
            }
            catch //(Exception ex)
            {
                // Could not rename -> probably we have no
                // write access to the directory
                return true;
            }
        }

        public static bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return false;
                }
            }
            catch //(IOException)
            {
                // Could not open file -> probably we have no
                // write access to the file
                return true;
            }
        }

        public static void SecureDeleteDirectory(string path, DeleteModes deleteMode)
        {
            if (deleteMode == DeleteModes.Simulate)
            {
                return;
            }

            if (deleteMode == DeleteModes.Direct)
            {
                Directory.Delete(path, recursive: false, ignoreReadOnly: true); //throws IOException if not empty anymore
                return;
            }

            // Last security check before deletion
            if (Directory.GetFiles(path).Length == 0 && Directory.GetDirectories(path).Length == 0)
            {
                if (deleteMode == DeleteModes.RecycleBin)
                {
                    // Check CLR permissions -> could raise a exception
                    new FileIOPermission(FileIOPermissionAccess.Write, path + Path.DirectorySeparatorChar.ToString()).Demand();

                    if (IsDirLocked(path))
                    {
                        throw new REDPermissionDeniedException(TXT.Translate("Could not delete directory because the access is protected by the (file) system (permission denied): {0}", RedAssist.DQuote(path)));
                    }

                    FileSystem.DeleteDirectory(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
                }
                else if (deleteMode == DeleteModes.RecycleBinShowErrors)
                {
                    FileSystem.DeleteDirectory(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
                }
                else if (deleteMode == DeleteModes.RecycleBinWithQuestion)
                {
                    FileSystem.DeleteDirectory(path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
                }
                else
                {
                    throw new Exception(RedGetText.Words.ErrorUnknownDeleteMode(deleteMode));
                }
            }
            else
            {
                throw new Exception(TXT.Translate("Aborted deletion of the directory because it is no longer empty. This can happen if RED previously failed to delete an empty (trash) file: {0}", RedAssist.DQuote(path)));
            }
        }

        public static void SecureDeleteFile(FileInfo file, DeleteModes deleteMode)
        {
            if (deleteMode == DeleteModes.Simulate)
            {
                return;
            }

            if (deleteMode == DeleteModes.RecycleBin)
            {
                // Check CLR permissions -> could raise a exception
                new FileIOPermission(FileIOPermissionAccess.Write, file.FullName).Demand();

                if (IsFileLocked(file))
                {
                    throw new REDPermissionDeniedException(TXT.Translate("Could not delete file because the access is protected by the (file) system (permission denied): {0}", RedAssist.DQuote(file.FullName)));
                }

                FileSystem.DeleteFile(file.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
            }
            else if (deleteMode == DeleteModes.RecycleBinShowErrors)
            {
                FileSystem.DeleteFile(file.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
            }
            else if (deleteMode == DeleteModes.RecycleBinWithQuestion)
            {
                FileSystem.DeleteFile(file.FullName, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
            }
            else if (deleteMode == DeleteModes.Direct)
            {
                // Was used for testing the error handling:
                // if (SystemFunctions.random.NextDouble() > 0.5) throw new Exception("Test error");
                file.Delete(ignoreReadOnly: true);
            }
            else
            {
                throw new Exception(RedGetText.Words.ErrorUnknownDeleteMode(deleteMode));
            }
        }

        public static string ChooseDirectoryDialog(string path)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            folderDialog.Description = TXT.Translate("Please select the directory that you want to be cleaned");
            folderDialog.ShowNewFolderButton = false;

            if (!string.IsNullOrWhiteSpace(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);

                if (dir.Exists)
                {
                    folderDialog.SelectedPath = path;
                }
            }

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                path = folderDialog.SelectedPath;
            }

            folderDialog.Dispose();

            return path;
        }

        /// <summary>
        /// Opens a folder
        /// </summary>
        public static void OpenDirectoryWithExplorer(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            string exe = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "explorer.exe");

            Process.Start(exe, string.Format("/e,{0}", RedAssist.DQuote(path)));
        }

        public static bool IsAdmin()
        {
            WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        // HKCU Registry Keys. No Admin Rights Required
        private const string regKeyNameShell = @"Software\Classes\Directory\shell";
        private const string regKeyNameBgShl = @"Software\Classes\Directory\Background\shell";
#if DEBUG
        private const string regSubkeyRed = @"RED+DBUG";
#else
        private const string regSubkeyRed = @"RED+";
#endif
        // HKCR Legacy Registry keys (used by orginal RED)
        private const string regLegacyKeyName = @"Folder\shell";
        private const string regLegacySubKeyRed = "Remove Empty Dirs";

        /// <summary>
        /// Check for the registry key
        /// </summary>
        /// <returns>0 = No, 1 = HKCR (Legacy), 2 = HKCU</returns>
        public static int IsRegKeyIntegratedIntoWindowsExplorer()
        {
            int isIntegrated = 0;
            try
            {
                using (var reg = Registry.ClassesRoot.OpenSubKey(regLegacyKeyName + @"\" + regLegacySubKeyRed, writable: false))
                {
                    isIntegrated = reg != null ? 1 : 0;
                }
                if (isIntegrated == 0)
                {
                    using (var reg = Registry.CurrentUser.OpenSubKey(regKeyNameShell + @"\" + regSubkeyRed, writable: false))
                    {
                        isIntegrated = reg != null ? 2 : 0;
                    }
                }
            }
            catch
            {
                isIntegrated = -1;
            }
            return isIntegrated;
        }

        internal static void ExplorerIntegrationAdd(bool autosearch)
        {
            try
            {
                // Integrate with HKCU method
                ExplorerIntegrationAdd(regKeyNameShell + @"\" + regSubkeyRed, "%1", autosearch);
                ExplorerIntegrationAdd(regKeyNameBgShl + @"\" + regSubkeyRed, "%V", autosearch);
            }
            catch (Exception ex)
            {
                UiAssist.MsgBoxError(TXT.Words.Error + RedGetText.CrLf1 + TXT.Translate("Could not change registry settings:") + RedGetText.CrLf2 + ex.ToString());
            }
        }

        internal static void ExplorerIntegrationRemove()
        {
            try
            {
                int isIntegrated = SystemFunctions.IsRegKeyIntegratedIntoWindowsExplorer();
                switch (isIntegrated)
                {
                    case 1:
                        // Integrated with Legacy HKCR method. Requires Admin rights
                        ExplorerIntegrationRemove(Registry.ClassesRoot, regLegacyKeyName, regLegacySubKeyRed);
                        break;
                    case 2:
                        // Integrated with HKCU method
                        ExplorerIntegrationRemove(Registry.CurrentUser, regKeyNameShell, regSubkeyRed);
                        ExplorerIntegrationRemove(Registry.CurrentUser, regKeyNameBgShl, regSubkeyRed);
                        break;
                    default:
                        // Not integrated or unable to determine
                        break;
                }
            }
            catch (Exception ex)
            {
                UiAssist.MsgBoxError(TXT.Words.Error + RedGetText.CrLf1 + TXT.Translate("Could not change registry settings:") + RedGetText.CrLf2 + ex.ToString());
            }
        }

        private static void ExplorerIntegrationAdd(string keyname, string placeholder, bool autosearch)
        {
            using (var reg = Registry.CurrentUser.CreateSubKey(keyname))
            {
                if (reg != null)
                {
                    reg.SetValue("MUIVerb", TXT.Red.Title);
                    reg.SetValue("Icon", Application.ExecutablePath + ",0");
                    //reg.SetValue("Position", "Bottom");
                    using (RegistryKey regcmd = Registry.CurrentUser.CreateSubKey(keyname + @"\command"))
                    {
                        if (regcmd != null)
                        {
                            //string cmd = string.Format("{0} {1} {2}", Application.ExecutablePath, autosearch ? "-autosearch" : "", RedAssist.DQuote(placeholder)));
                            StringBuilder cmd = new StringBuilder();
                            cmd.Append(RedAssist.DQuote(Application.ExecutablePath));
                            cmd.Append(autosearch ? " -autosearch " : " "); //space before and after
                            cmd.Append(RedAssist.DQuote(placeholder));
                            regcmd.SetValue("", cmd.ToString());
                        }
                    }
                }
            }
        }

        private static void ExplorerIntegrationRemove(RegistryKey regKey, string keyname, string subkeyname)
        {
            using (var reg = regKey.OpenSubKey(keyname, writable: true))
            {
                if (reg != null)
                {
                    reg.DeleteSubKeyTree(subkeyname, throwOnMissingSubKey: false);
                }
            }
        }
    }
}