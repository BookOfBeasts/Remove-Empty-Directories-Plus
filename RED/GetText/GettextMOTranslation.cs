#region License

/*
SecondLanguage Gettext Library for .NET
Copyright 2013 James F. Bellinger <http://www.zer7.com/software/secondlanguage>

This software is provided 'as-is', without any express or implied
warranty. In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

1. The origin of this software must not be misrepresented; you must
not claim that you wrote the original software. If you use this software
in a product, an acknowledgement in the product documentation would be
appreciated but is not required.

2. Altered source versions must be plainly marked as such, and must
not be misrepresented as being the original software.

3. This notice may not be removed or altered from any source
distribution.
*/

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SecondLanguage
{
	/// <summary>
	/// Loads and saves Gettext .mo files and provides lower-level access to strings.
	/// </summary>
	public partial class GettextMOTranslation : GettextTranslation
	{
		private const uint MOMagic = 0x950412de;
		private const uint MOMagicReversed = 0xde120495;

		private byte[] _buffer;
		private bool _littleEndian;
		private uint _offsetOfMsgidTable;
		private uint _offsetOfMsgstrTable;
		private Dictionary<string, string> _strings = new Dictionary<string, string>();

		/// <summary>
		/// Initializes a new instance of the <see cref="GettextMOTranslation"/> class.
		/// </summary>
		public GettextMOTranslation()
		{
		}

		/// <inheritdoc />
		public override IEnumerable<KeyValuePair<GettextKey, string[]>> GetGettextKeys()
		{
			foreach (KeyValuePair<string, string> kvp in _strings)
			{
				yield return new KeyValuePair<GettextKey, string[]>
					(GettextKey.FromMOKeyString(kvp.Key), kvp.Value.Split('\0'));
			}
		}

		protected override void ClearOverride()
		{
			base.ClearOverride();

			_strings.Clear();
			SetString("", DefaultHeaders);
			IsLittleEndian = true;
		}

		protected override void LoadOverride(byte[] buffer)
		{
			Clear();

			_buffer = buffer;
			Encoding = Encoding.ASCII;

			uint magic = ParseUInt32(0);
			if (magic == 0x950412de)
			{
				// IsLittleEndian is correct.
			}
			else if (magic == 0xde120495)
			{
				IsLittleEndian = false;
			}
			else
			{
				throw new IOException("Not a .mo file.");
			}

			uint revision = ParseUInt32(4);
			uint stringCount = ParseUInt32(8);
			_offsetOfMsgidTable = ParseUInt32(12);
			_offsetOfMsgstrTable = ParseUInt32(16);
			uint sizeOfHashTable = ParseUInt32(20);
			uint offsetOfHashTable = ParseUInt32(24);

			if (revision != 0)
			{
				throw new IOException("Unsupported revision number " + revision.ToString() + ".");
			}

			for (uint i = 0; i < stringCount; i++)
			{
				string msgid, msgstr;
				ExtractStringPair(i, out msgid, out msgstr);

				if (msgid == "")
				{
					SetStringCore(msgid, msgstr); // The first time around, it will update Encoding.
					ExtractStringPair(i, out msgid, out msgstr);
				}

				SetStringCore(msgid, msgstr);
			}
		}

		protected override string GetStringOverride(string id, string context)
		{
			string key = new GettextKey(id, context: context).ToMOKeyString();

			string translation;
			return _strings.TryGetValue(key, out translation) ? translation : null;
		}

		protected override string GetPluralStringByIndexOverride(string id, string idPlural, int index, string context)
		{
			string key = new GettextKey(id, idPlural: idPlural, context: context).ToMOKeyString();

			string translation = GetString(key);
			if (translation == null) { return null; }

			string[] translations = translation.Split('\0');
			return (uint)index < (uint)translations.Length ? translations[index] : null;
		}

		protected override void SetStringOverride(string id, string translation, string context)
		{
			string key = new GettextKey(id, context: context).ToMOKeyString();

			SetStringCore(key, translation);
		}

		protected override void SetPluralStringOverride(string id, string idPlural, string[] translations, string context)
		{
			string key = new GettextKey(id, idPlural: idPlural, context: context).ToMOKeyString();

			SetString(key, string.Join("\0", translations));
		}

		private void SetStringCore(string msgid, string msgstr)
		{
			_strings[msgid] = msgstr;
			if (msgid == "") { ParseHeaders(); }
		}

		protected override byte[] SaveOverride()
		{
			using (MemoryStream stream = new MemoryStream())
			{
				checked
				{
					uint headerLength = 28;
					uint msgidTableOffset = headerLength;
					uint msgidTableLength = 8 * (uint)_strings.Count;
					uint msgstrTableOffset = msgidTableOffset + msgidTableLength;
					uint msgstrTableLength = msgidTableLength;
					uint dataOffset = msgstrTableOffset + msgstrTableLength;

					Action<uint> writeUInt32 = value => stream.Write(SaveUInt32(value), 0, 4);

					stream.Seek(0, SeekOrigin.Begin);
					writeUInt32(MOMagic);
					writeUInt32(0); // revision
					writeUInt32((uint)_strings.Count);
					writeUInt32(msgidTableOffset);
					writeUInt32(msgstrTableOffset);
					writeUInt32(0); // hash size
					writeUInt32(0); // hash offset

					Func<string, byte[]> encodeString = s => Encoding.GetBytes(s);
					Action<uint, byte[]> writeStringIntoTable = (tableOffset, msg) =>
						{
							stream.Seek(tableOffset, SeekOrigin.Begin);
							writeUInt32((uint)msg.Length);
							writeUInt32(dataOffset);

							Array.Resize(ref msg, (msg.Length + 4) & ~3);
							stream.Seek(dataOffset, SeekOrigin.Begin);
							stream.Write(msg, 0, msg.Length);
							dataOffset += (uint)msg.Length;
						};

					KeyValuePair<byte[], byte[]>[] strings = _strings
						.Select(s => new KeyValuePair<byte[], byte[]>(encodeString(s.Key), encodeString(s.Value)))
						.OrderBy(x => x.Key, new ByteArrayComparer())
						.ToArray();
					uint i;

					i = 0;
					foreach (byte[] msgid in strings.Select(s => s.Key))
					{
						writeStringIntoTable(msgidTableOffset + i * 8, msgid); i++;
					}

					i = 0;
					foreach (byte[] msgstr in strings.Select(s => s.Value))
					{
						writeStringIntoTable(msgstrTableOffset + i * 8, msgstr); i++;
					}

					return stream.ToArray();
				}
			}
		}

		private void ExtractStringPair(uint i, out string msgid, out string msgstr)
		{
			msgid = ExtractString(_offsetOfMsgidTable, i);
			msgstr = ExtractString(_offsetOfMsgstrTable, i);
		}

		private string ExtractString(uint offsetOfTable, uint i)
		{
			int offsetOfEntry = checked((int)(offsetOfTable + i * 8));
			int offsetOfString = (int)ParseUInt32(offsetOfEntry + 4);
			int lengthOfString = (int)ParseUInt32(offsetOfEntry + 0);
			string str = Encoding.GetString(_buffer, offsetOfString, lengthOfString);
			return str;
		}

		private uint ParseUInt32(int offset)
		{
			if (IsLittleEndian)
			{
				return (uint)(_buffer[offset + 0]
							| _buffer[offset + 1] << 8
							| _buffer[offset + 2] << 16
							| _buffer[offset + 3] << 24);
			}
			else
			{
				return (uint)(_buffer[offset + 3]
							| _buffer[offset + 2] << 8
							| _buffer[offset + 1] << 16
							| _buffer[offset + 0] << 24);
			}
		}

		private byte[] SaveUInt32(uint value)
		{
			if (IsLittleEndian)
			{
				return new[] { (byte)value, (byte)(value >> 8), (byte)(value >> 16), (byte)(value >> 24) };
			}
			else
			{
				return new[] { (byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value };
			}
		}

		/// <summary>
		/// <c>true</c> if the .mo file is little-endian.
		/// <c>false</c> if it is big-endian.
		/// </summary>
		public bool IsLittleEndian
		{
			get { return _littleEndian; }
			set { AboutToChange(); _littleEndian = value; }
		}
	}
}