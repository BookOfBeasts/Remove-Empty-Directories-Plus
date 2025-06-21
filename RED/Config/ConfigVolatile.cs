using System;
using System.Xml.Serialization;

using NotBob.Lib;

namespace RED.Config
{
    [Serializable]
    public class ConfigVolatile : NBDataIsDirty
    {
        public ConfigVolatile()
        {
            SetToDefaults();
            DataIsDirty = false;
        }

        public long CountOfDeletions { get { return _CountOfDeletions; } set { SetField(ref _CountOfDeletions, value); } }
        private long _CountOfDeletions;

        public string LastUsedDirectory { get { return _LastUsedDirectory; } set { SetField(ref _LastUsedDirectory, value); } }
        private string _LastUsedDirectory;

        [XmlIgnore]
        public override bool DataIsDirty
        {
            get { return base.DataIsDirty; }
            set { base.DataIsDirty = value; }
        }

        public void SetToDefaults()
        {
            LastUsedDirectory = @"C:\";
            CountOfDeletions = 0;
        }
    }
}