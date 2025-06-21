using System;
using System.Drawing;
using System.Windows.Forms;

using Alphaleonis.Win32.Filesystem;

using NotBob.Lib;

using RED;
using RED.Config;
using RED.Helper;
using TXT = RED.RedGetText;

namespace NotBob.Config
{
    // NotBob Configuration File helper routines.

    internal static class ConfigAssist
    {
        private static int RedirectCount = 0;
        private static readonly int RedirectMax = 3;

        internal static void ConfigLoad(ref RedConfiguration config, string appName)
        {
            bool createConfig = false;
            string filename = "?";
            try
            {
                string cfgName = Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".cfg";
                filename = ConfigAssist.GetConfigFilename(cfgName, appName, Application.ExecutablePath);
                if (!string.IsNullOrEmpty(filename))
                {
                    while (File.Exists(filename) && File.GetSize(filename) > 0)
                    {
                        config = ConfigAssist.Load<RedConfiguration>(filename);

                        // Does the config file redirect to another location?
                        if (!string.IsNullOrWhiteSpace(config.RedirectTo))
                        {
                            RedirectCount++;
                            if (RedirectCount >= RedirectMax)
                            {
                                UiAssist.MsgBoxError(string.Format(TXT.Translate("Redirect maximum [{0}] reached in configuration file"), RedirectMax));
                                config.RedirectTo = string.Empty;
                                break;
                            }
                            else
                            {
                                filename = config.RedirectTo;
                            }
                        }
                        else
                        {
                            // No redirect in place, use this configuration file
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
                    {
                        // The config may already have its ReadOnly flag set.
                        // This is an extra check to see if the file itself is set to ReadOnly
                        if (File.GetAttributes(filename).HasFlag(System.IO.FileAttributes.ReadOnly))
                        {
                            config.IsReadOnly = true;
                        }
                        // If the file is zero length, then the config needs to be created
                        if (File.GetSize(filename) == 0)
                        {
                            createConfig = true;
                        }
                    }
                    else
                    {
                        createConfig = true;
                    }
                }
                else
                {
                    createConfig = true;
                }

                if (createConfig)
                {
                    RedConfiguration obj = new RedConfiguration();
                    obj.SetToDefaults();
                    config = obj;
                    UiAssist.MsgBoxInfo(TXT.Translate("Settings have been set to their defaults"));
                    if (string.IsNullOrWhiteSpace(filename))
                    {
                        config.IsReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string emsg = string.Format("{0}:{1}{2}", TXT.Translate("Error trying to read configuration file"), RedGetText.CrLf1, filename);
                UiAssist.MsgBoxException(emsg, ex);
                config.IsReadOnly = true;
            }
            finally
            {
                config.PopulateRuntime(filename, Application.ExecutablePath, Application.ProductName, Application.ProductVersion);
                if (createConfig && !string.IsNullOrWhiteSpace(config.Runtime.ConfigFilename) && !config.IsReadOnly)
                {
                    ConfigSave(config, force: true);
                }
                config.DataIsDirty = false;
            }
        }

        internal static void ConfigSave(RedConfiguration config, bool force = false)
        {
            try
            {
                config.CreatedBy = config.Runtime.CreatedBy;

                bool saveRequired = config.DataIsDirty || force;
                if (saveRequired && !force)
                {
                    saveRequired = ConfigSavePrompt(config, saveRequired);
                }

                if (config.DataIsDirty || force)
                {
                    if (!config.IsReadOnly)
                    {
                        string cfgFolder = Path.GetDirectoryName(config.Runtime.ConfigFilename);
                        if ((!Directory.Exists(cfgFolder)))
                        {
                            Directory.CreateDirectory(cfgFolder);
                        }
                        ConfigAssist.Save(config, config.Runtime.ConfigFilename);
                        config.DataIsDirty = false;
                    }
                }
            }
            catch (Exception ex)
            {
                string emsg = string.Format("{0}:{1}{2}", TXT.Translate("Error trying to save configuration file"), RedGetText.CrLf1, config.Runtime.ConfigFilename);
                UiAssist.MsgBoxException(emsg, ex);
                config.IsReadOnly = true;
            }
        }

        internal static void ConfigSaveWithPrompt(RedConfiguration config, bool ask = false)
        {
            bool saveRequired = config.DataIsDirty && !config.IsReadOnly;
            if (ask)
            {
                saveRequired = ConfigAssist.ConfigSavePrompt(config, saveRequired);
            }
            ConfigAssist.ConfigSave(config, saveRequired);
        }

        private static bool ConfigSavePrompt(RedConfiguration config, bool saveRequired)
        {
            string msg = TXT.Translate("Save Settings?") + "\r\n" + config.Filename;
            return UiAssist.BAskYesNo(msg, saveRequired ? MessageBoxDefaultButton.Button1 : MessageBoxDefaultButton.Button2);
        }

        private static string GetConfigFilename(string cfgName, string appName, string executablePath)
        {
            string configFilename = string.Empty;
            try
            {
                // Try portable mode first (ie app folder)
                configFilename = GetConfigFilenamePortable(cfgName, appName, executablePath);
                if (!File.Exists(configFilename))
                {
                    // Try %APPDATA%
                    string cfgFile = GetConfigFilenameAppData(cfgName, appName, executablePath);
                    if (File.Exists(cfgFile))
                    {
                        configFilename = cfgFile;
                    }
                    else
                    {
                        // No config file found, ask user where they would like it to be created
                        if (ConfirmUseOfPortableMode())
                        {
                            // User wants to use portable mode
                            configFilename = GetConfigFilenamePortable(cfgName, appName, executablePath);
                        }
                        else
                        {
                            // User wants to use %APPDATA%
                            configFilename = GetConfigFilenameAppData(cfgName, appName, executablePath);
                        }
                        //string cfgFolder = Path.GetDirectoryName(configFilename);
                    }
                }
            }
            catch (Exception)
            {
                configFilename = string.Empty;
            }
            return configFilename;
        }

        private static string GetConfigFilenamePortable(string cfgName, string appName, string executablePath)
        {
            string cfgFolder = Path.GetDirectoryName(executablePath);
            string configFilename = Path.Combine(cfgFolder, cfgName);
            return configFilename;
        }

        private static string GetConfigFilenameAppData(string cfgName, string appName, string executablePath)
        {
            string cfgBase = Path.GetFileNameWithoutExtension(cfgName);
            string cfgFolder = GetNotBobAppFolder(appName, Environment.SpecialFolder.ApplicationData);
            string configFilename = Path.Combine(cfgFolder, cfgName);
            return configFilename;
        }

        private static bool AskUserForConfigPortableOrAppData(int defaultButton)
        {
            bool respx = false;
            using (NotBob.UI.NBMsgBox mbox = new NotBob.UI.NBMsgBox("RED: No Config File Found", MessageBoxIcon.Question))
            {
                mbox.ControlBox = true;
                mbox.Icon = RED.Properties.Resources.iconProject;
                mbox.SetMessage(TXT.Translate("Do you want to use Portable Mode or %APPDATA%?"));
                mbox.SetButton(1, "Portable Mode", DialogResult.Yes, isDefault: true);
                mbox.SetButton(2, "%APPDATA%", DialogResult.No);
                mbox.ShowDialog();
                switch (mbox.DialogExitButton)
                {
                    case NotBob.UI.NBMsgBoxExitButton.Button1:
                        respx = (defaultButton == 1) ? true : false;
                        break;
                    case NotBob.UI.NBMsgBoxExitButton.Button2:
                        respx = (defaultButton == 2) ? true : false;
                        break;
                    default:
                        respx = false;
                        break;
                }
            }
            return respx;
        }

        private static bool ConfirmUseOfAppDataMode()
        {
            return AskUserForConfigPortableOrAppData(2);
        }

        private static bool ConfirmUseOfPortableMode()
        {
            return AskUserForConfigPortableOrAppData(1);
        }

        private static string GetNotBobAppFolder(string appName, Environment.SpecialFolder specialFolder)
        {
            string settingsFolder = GetNotBobFolder(specialFolder);
            settingsFolder = Path.Combine(settingsFolder, appName);
            return settingsFolder;
        }

        private static string GetNotBobFolder(Environment.SpecialFolder specialFolder)
        {
            string settingsFolder = GetSpecialFolder(specialFolder);
            settingsFolder = Path.Combine(settingsFolder, "NotBob");
            return settingsFolder;
        }

        private static string GetSpecialFolder(Environment.SpecialFolder specialFolder)
        {
            string settingsFolder = Environment.GetFolderPath(specialFolder);
            return settingsFolder;
        }

        private static T Load<T>(string filename)
        {
            T respx = default(T);
            if (File.Exists(filename))
            {
                respx = NBSerialize.DeserializeFromXmlFile<T>(filename);
            }
            return respx;
        }

        private static void Save<T>(T config, string filename)
        {
            NBSerialize.SerializeToXmlFile<T>(config, filename);
        }
    }
}