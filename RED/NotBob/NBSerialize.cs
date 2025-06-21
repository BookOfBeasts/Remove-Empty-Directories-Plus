using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NotBob.Lib
{
	/// <summary>
	/// Routines to aid serializing data (used by NBConfig)
	/// </summary>
	public static class NBSerialize
	{
		public static void SerializeToXmlFile<T>(T obj, string fileName, Encoding encoding)
		{
			// Do not include any namespace info
			XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
			xmlns.Add("", "");
			// Control the XML formatting
			XmlWriterSettings xmlSettings = new XmlWriterSettings();
			xmlSettings.Encoding = encoding;
			xmlSettings.Indent = true;
			//xmlSettings.IndentChars = "\t";
			// Create or Overwrite a stream and write the XML to it
			using (XmlWriter writer = XmlWriter.Create(fileName, xmlSettings))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				xmlSerializer.Serialize(writer, obj, xmlns);
				writer.Flush();
				writer.Close();
			}
		}

		public static void SerializeToXmlFile<T>(T obj, string fileName)
		{
			UTF8Encoding utf8 = new UTF8Encoding(false);
			SerializeToXmlFile(obj, fileName, utf8);
		}

		public static T DeserializeFromXmlFile<T>(string filename, Encoding encoding)
		{
			T result;
			using (StreamReader reader = new StreamReader(filename, encoding))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				result = (T)xmlSerializer.Deserialize(reader);
			}
			return result;
		}

		public static T DeserializeFromXmlFile<T>(string filename)
		{
			UTF8Encoding utf8 = new UTF8Encoding(false);
			return DeserializeFromXmlFile<T>(filename, utf8);
		}

		public static T DeserializeFromXml<T>(string xml)
		{
			T result;
			using (TextReader tr = new StringReader(xml))
			{
				XmlSerializer ser = new XmlSerializer(typeof(T));
				result = (T)ser.Deserialize(tr);
			}
			return result;
		}
	}
}