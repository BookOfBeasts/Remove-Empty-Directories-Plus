using System;
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
                        // If the file is zero length, then it indicates 'portable' mode is required
                        // but the file needs to be created
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
                string cfgFolder = Path.GetDirectoryName(executablePath);
                configFilename = Path.Combine(cfgFolder, cfgName);
                if (!File.Exists(configFilename))
                {
                    // Use APPDATA
                    string cfgBase = Path.GetFileNameWithoutExtension(cfgName);
                    cfgFolder = GetSpecialFolder(appName, Environment.SpecialFolder.ApplicationData);
                    if (!Directory.Exists(cfgFolder))
                    {
                        Directory.CreateDirectory(cfgFolder);
                    }
                    configFilename = Path.Combine(cfgFolder, cfgName);
                }
            }
            catch (Exception)
            {
                configFilename = string.Empty;
            }
            return configFilename;
        }

        private static string GetSpecialFolder(string appName, Environment.SpecialFolder specialFolder)
        {
            string settingsFolder = Environment.GetFolderPath(specialFolder);
            settingsFolder = Path.Combine(settingsFolder, "NotBob");
            settingsFolder = Path.Combine(settingsFolder, appName);
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