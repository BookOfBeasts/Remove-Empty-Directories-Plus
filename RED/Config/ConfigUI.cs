using System;
using System.Drawing;
using System.Xml.Serialization;

using NotBob.Lib;

namespace RED.Config
{
    [Serializable]
    public class ConfigUI : NBDataIsDirty
    {
        public ConfigUI()
        {
            DataIsDirty = false;
        }

        public Point WinMainLocation { get { return _WinMainLocation; } set { SetField(ref _WinMainLocation, value); } }
        private Point _WinMainLocation;

        public Size WinMainSize { get { return _WinMainSize; } set { SetField(ref _WinMainSize, value); } }
        private Size _WinMainSize;

        [XmlIgnore]
        public override bool DataIsDirty
        {
            get { return base.DataIsDirty; }
            set { base.DataIsDirty = value; }
        }

        internal void SetToDefaults()
        {
            WinMainLocation = new Point();
            WinMainSize = new Size();
        }
    }
}