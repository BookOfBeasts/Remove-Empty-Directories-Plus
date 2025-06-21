using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using NotBob.Lib;

namespace RED.Config
{
	[Serializable]
	public class ConfigFilters : NBDataIsDirty
	{
		public ConfigFilters()
		{
			FilesToIgnore = new List<string>();
			DirectoriesToIgnore = new List<string>();
			DirectoriesNeverEmpty = new List<string>();
			DataIsDirty = false;
		}

		[XmlArray("FilesToIgnore", IsNullable = false)]
		[XmlArrayItem("item")]
		public List<string> FilesToIgnore { get; set; }

		[XmlArray("DirectoriesToIgnore", IsNullable = false)]
		[XmlArrayItem("item")]
		public List<string> DirectoriesToIgnore { get; set; }

		[XmlArray("DirectoriesNeverEmpty", IsNullable = false)]
		[XmlArrayItem("item")]
		public List<string> DirectoriesNeverEmpty { get; set; }

		[XmlIgnore]
		public override bool DataIsDirty
		{
			get { return base.DataIsDirty; }
			set { base.DataIsDirty = value; }
		}

		internal void SetToDefaults()
		{
			FilesToIgnore.Clear();
			FilesToIgnore.Add(@"+|N|desktop.ini");
			FilesToIgnore.Add(@"+|N|Thumbs.db");
			FilesToIgnore.Add(@"+|N|.DS_Store");
			FilesToIgnore.Add(@"+|RN|._*");

			DirectoriesToIgnore.Clear();
			DirectoriesToIgnore.Add(@"+|N|System Volume Information");
			DirectoriesToIgnore.Add(@"+|N|RECYCLER");
			DirectoriesToIgnore.Add(@"+|N|Recycled");
			DirectoriesToIgnore.Add(@"+|N|NtUninstall");
			DirectoriesToIgnore.Add(@"+|N|$RECYCLE.BIN");
			DirectoriesToIgnore.Add(@"+|N|GAC_MSIL");
			DirectoriesToIgnore.Add(@"+|N|GAC_32");
			DirectoriesToIgnore.Add(@"+|N|winsxs");
			DirectoriesToIgnore.Add(@"+|N|System32");

			DirectoriesNeverEmpty.Clear();
		}
	}
}