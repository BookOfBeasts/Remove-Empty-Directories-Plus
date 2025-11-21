using System.IO;

namespace RED.Config
{
    public class ConfigRuntime
    {
        public ConfigRuntime()
        {
            Volatile = new ConfigVolatile();
        }

        internal string CreatedBy { get; set; }
        internal string ProductName { get; set; }
        internal string Executable { get; set; }
        internal string ExecutablePath { get { return Path.GetDirectoryName(Executable); } }
        internal string ExecutableName { get { return Path.GetFileName(Executable); } }
        internal string ConfigFilename { get; set; }
        internal string ConfigPath { get { return Path.GetDirectoryName(ConfigFilename); } }

        internal SecondLanguage.Translator Translator = SecondLanguage.Translator.Default;

        internal string HelpFile { get { return Path.Combine(ExecutablePath, @"help\index.htm"); } }

        internal ConfigVolatile Volatile { get; set; }
    }
}