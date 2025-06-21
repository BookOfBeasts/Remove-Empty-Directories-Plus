using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace NotBob.Lib
{
	public class NBDataIsDirty : INotifyPropertyChanged
	{
		[XmlIgnore()]
		public virtual bool DataIsDirty { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// example of setting a field
		// private string _fieldname;
		// public string fieldname { get { return _fieldname; } set { SetField(ref _fieldname, value); } }
		protected void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			if (!EqualityComparer<T>.Default.Equals(field, value))
			{
				field = value;
				DataIsDirty = true;
				if (propertyName != null)
				{
					OnPropertyChanged(propertyName);
				}
			}
		}
	}
}