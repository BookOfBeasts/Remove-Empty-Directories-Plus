using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using RED.Config;

namespace RED.Helper
{
    internal class RedDebug
    {
        public RedDebug() { }

        public string GatherDebugInfo(RedConfiguration RedConfig)
        {
            StringBuilder info = new StringBuilder();

            info.AppendLine("System info");
            info.Append("- RED Version: ");
            try
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                FileVersionInfo vi = FileVersionInfo.GetVersionInfo(asm.Location);
                info.Append(string.Format("File Version={0}", vi.FileVersion.ToString()));
                info.Append(string.Format(", Product Version={0}", asm.GetName().Version.ToString()));
#if DEBUG
                info.Append(" (DBUG)");
#endif
                info.AppendLine();
            }
            catch (Exception ex)
            {
                info.AppendLine("Version Failed (" + ex.Message + ")");
            }

            info.Append("- Operating System: ");
            try
            {
                info.AppendLine(Environment.OSVersion.ToString());
            }
            catch (Exception ex)
            {
                info.AppendLine("OSVersion Failed (" + ex.Message + ")");
            }

            info.Append("- Processor architecture: ");
            try
            {
                info.AppendLine(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));
            }
            catch (Exception ex)
            {
                info.AppendLine("Architecture Failed (" + ex.Message + ")");
            }

            info.Append("- Is Administrator: ");
            try
            {
                WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                info.AppendLine((principal.IsInRole(WindowsBuiltInRole.Administrator) ? "Yes" : "No"));
            }
            catch (Exception ex)
            {
                info.AppendLine("IsAdmin Failed (" + ex.Message + ")");
            }

            info.AppendLine("");
            info.AppendLine("RED Config settings: ");
            try
            {

                info.AppendLine(string.Format(" File Location: {0}", RedConfig.Filename));

                info.AppendLine(string.Format(" Ignore Empty Files: {0}", RedConfig.Options.IgnoreEmptyFiles));
                info.AppendLine(string.Format(" Ignore System Directories: {0}", RedConfig.Options.IgnoreSystemDirectories));
                info.AppendLine(string.Format(" Ignore Hidden Directories: {0}", RedConfig.Options.IgnoreHiddenDirectories));
                info.AppendLine(string.Format(" Hide Deletion Errors: {0}", RedConfig.Options.HideDeletionErrors));
                info.AppendLine(string.Format(" Hide Scan Errors: {0}", RedConfig.Options.HideScanErrors));
                info.AppendLine(string.Format(" Hide Ignored Directories: {0}", RedConfig.Options.HideIgnoredDirectories));
                info.AppendLine(string.Format(" Auto Protect Start Directory: {0}", RedConfig.Options.AutoProtectRoot));
                info.AppendLine(string.Format(" Fast Search Mode: {0}", RedConfig.Options.FastSearchMode));
                info.AppendLine(string.Format(" Clipboard Path Detection: {0}", RedConfig.Options.ClipboardPathDetection));

                info.AppendLine(string.Format(" Delete Mode: {0}", RedConfig.Options.DeleteModeInt));

                info.AppendLine(string.Format(" Max Nesting Depth: {0}", RedConfig.Options.MaxDirectoryDepth));
                info.AppendLine(string.Format(" Pause Between Deletions: {0}", RedConfig.Options.PauseBetweenDeletions));
                info.AppendLine(string.Format(" Minimum Directory Age: {0}", RedConfig.Options.MinDirectoryAgeHours));
                info.AppendLine(string.Format(" Infinite Loop Count: {0}", RedConfig.Options.InfiniteLoopDetectionCount));

                info.AppendLine(string.Format(" Remember Window Details: {0}", RedConfig.Options.RememberWindowDetails));
                info.AppendLine(string.Format(" Remember Last Used Dir : {0}", RedConfig.Options.RememberLastUsedDirectory));
                info.AppendLine(string.Format(" Remember Deletion Stats: {0}", RedConfig.Options.RememberDeletionStats));
                info.AppendLine(string.Format(" Save Prompt: {0}", RedConfig.Options.SavePrompt));
                info.AppendLine(string.Format(" Language: {0}", RedConfig.Options.Language));

                AppendFilterInfo(info, "Ignored Files", RedConfig.Filters.FilesToIgnore);
                AppendFilterInfo(info, "Ignored Directories", RedConfig.Filters.DirectoriesToIgnore);
                AppendFilterInfo(info, "Never Empty Directories", RedConfig.Filters.DirectoriesNeverEmpty);

                info.AppendLine(string.Format(" Last Used Directory: {0}", RedConfig.Volatile.LastUsedDirectory));
                info.AppendLine(string.Format(" Deletion Count: {0}", RedConfig.Volatile.CountOfDeletions));
            }
            catch (Exception ex)
            {
                info.AppendLine("Failed (" + ex.Message + ")");
            }

            return info.ToString();
        }

        private void AppendFilterInfo(StringBuilder info, string title, List<string> filterList)
        {
            info.AppendLine(string.Format(" {0}: Count={1}", title, filterList.Count));
            for (int i = 0; i < filterList.Count; i++)
            {
                info.AppendLine(string.Format("  {0}={1}", i + 1, filterList[i]));
            }
        }
    }
}
