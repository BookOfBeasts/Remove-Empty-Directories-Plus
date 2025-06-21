using System;
using System.IO;
using System.Xml.Serialization;

using NotBob.Lib;

namespace RED.Config
{
    [Serializable]
    [XmlRoot("RED.PLUS")]
    public class RedConfiguration : NBDataIsDirty
    {
        public RedConfiguration()
        {
            Options = new ConfigOptions();
            Filters = new ConfigFilters();
            UI = new ConfigUI();
            Volatile = new ConfigVolatile();
            Runtime = new ConfigRuntime();

            IsReadOnly = false;
            DataIsDirty = false;
        }

        internal string Path { get { return Runtime.ConfigPath; } }
        internal string Filename { get { return Runtime.ConfigFilename; } }
        internal bool IsReadOnly { get; set; }
        internal bool Exists { get { return File.Exists(Filename); } }

        public string CreatedBy { get { return _CreatedBy; } set { SetField(ref _CreatedBy, value); } }
        private string _CreatedBy;

        public string RedirectTo { get; set; }

        public ConfigVolatile Volatile { get; set; }

        public ConfigOptions Options { get; set; }

        public ConfigFilters Filters { get; set; }

        public ConfigUI UI { get; set; }

        internal ConfigRuntime Runtime { get; private set; }

        [XmlIgnore]
        public override bool DataIsDirty
        {
            get { return (base.DataIsDirty || Volatile.DataIsDirty || Options.DataIsDirty || Filters.DataIsDirty || UI.DataIsDirty); }
            set { base.DataIsDirty = value; Volatile.DataIsDirty = value; Options.DataIsDirty = value; Filters.DataIsDirty = value; UI.DataIsDirty = value; }
        }

        internal void SetToDefaults()
        {
            Options.SetToDefaults();
            Filters.SetToDefaults();
            Volatile.SetToDefaults();
            UI.SetToDefaults();
        }

        public void PopulateRuntime(string configFilename, string executableName, string productName, string productVersion)
        {
            Runtime.CreatedBy = string.Format("{0} {1}", productName, productVersion);
            Runtime.ConfigFilename = configFilename;
            Runtime.ProductName = productName;
            Runtime.Executable = executableName;
        }
    }
}