#region License

/*
SecondLanguage Gettext Library for .NET
Copyright 2013, 2016 James F. Bellinger <http://www.zer7.com/software/secondlanguage>

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
using System.Globalization;
using System.IO;
using System.Text;

namespace SecondLanguage
{
	/// <summary>
	/// Loads and saves Gettext .po files and provides lower-level access to strings.
	/// </summary>
	/// TODO: Switch over to GettextKey. This earlier way of using .mo strings is a bit ugly. :)
	public class GettextPOTranslation : GettextTranslation
	{
		private sealed class Entry
		{
			public List<string> Whitespace = new List<string>();
			public string Context;
			public string ID;
			public string IDPlural;
			public List<string> Strings = new List<string>();

			public override string ToString()
			{
				return ID ?? "";
			}

			public string Key
			{
				get { return new GettextKey(ID, IDPlural, Context).ToMOKeyString(); }
			}
		}

		private List<Entry> _entries = new List<Entry>();
		private Dictionary<string, Entry> _lookup = new Dictionary<string, Entry>();

		/// <summary>
		/// Initializes a new instance of the <see cref="GettextPOTranslation"/> class.
		/// </summary>
		public GettextPOTranslation()
		{
		}

		/// <inheritdoc />
		public override IEnumerable<KeyValuePair<GettextKey, string[]>> GetGettextKeys()
		{
			foreach (Entry entry in _entries)
			{
				yield return new KeyValuePair<GettextKey, string[]>
					(new GettextKey(entry.ID, entry.IDPlural, entry.Context), entry.Strings.ToArray());
			}
		}

		protected override void ClearOverride()
		{
			base.ClearOverride();

			_entries.Clear(); _lookup.Clear();
			SetString("", DefaultHeaders);
		}

		protected override void LoadOverride(byte[] buffer)
		{
			// FIXME: This does work, but it sure ain't a work of art.
			Clear();

			int previousTarget = 0; int index = 0; Entry entry = null;

			Action initEntry = () =>
				{
					entry = new Entry(); index = 0;
				};
			Action nextEntry = () =>
				{
					if (entry.ID != null && entry.Strings.Count >= 1)
					{
						string key = entry.Key;

						Entry foundEntry; int foundIndex;
						if (_lookup.TryGetValue(key, out foundEntry)) { foundIndex = _entries.IndexOf(foundEntry); }
						else { foundIndex = _entries.Count; _entries.Add(new Entry()); }

						_entries[foundIndex] = entry; _lookup[key] = entry;
						ChangedStringEntry(key);
					}
					initEntry();
				};
			initEntry();

			int p = 0;
			if (buffer.Length >= 3 && (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)) { p = 3; } // Ignore UTF-8 BOM.
			while (true)
			{
				ignoreLine:
				if (p >= buffer.Length) { break; }

				int q;
				for (q = p; q < buffer.Length && buffer[q] != '\n'; q++)
                {
                    ;
                }

                string line = Encoding.GetString(buffer, p, q - p);
				p = q + 1;

				redoLine:
				int addOffset = 0; bool add = false;
				string trimmedLine = line.Trim(); int currentTarget = previousTarget;

				if (trimmedLine.StartsWith("msgctxt ", true, CultureInfo.InvariantCulture))
				{
					currentTarget = 1; addOffset = 8; // context
				}
				else if (trimmedLine.StartsWith("msgid ", true, CultureInfo.InvariantCulture))
				{
					currentTarget = 2; addOffset = 6; // id
				}
				else if (trimmedLine.StartsWith("msgid_plural ", true, CultureInfo.InvariantCulture))
				{
					currentTarget = 3; addOffset = 13; // plural id
				}
				else if (trimmedLine.StartsWith("msgstr", true, CultureInfo.InvariantCulture))
				{
					bool parseOK = false;

					if (trimmedLine.Length >= 9 && trimmedLine[6] == ' ')
					{
						addOffset = 7; index = 0; parseOK = true;
					}
					else if (trimmedLine.Length >= 12 && trimmedLine[6] == '[')
					{
						int j = trimmedLine.IndexOf("] ", 7);
						if (j >= 0)
						{
							string indexString = trimmedLine.Substring(7, j - 7);
							if (int.TryParse(indexString, out index) && index == entry.Strings.Count)
							{
								addOffset = j + 2; parseOK = true;
							}
						}
					}

					if (parseOK) { currentTarget = 4; } // string
					else { currentTarget = 0; } // comment
				}
				else if (GettextPOEncoding.DecodeString(trimmedLine) != null)
				{
					addOffset = 0; add = true; // add it on
				}
				else
				{
					currentTarget = 0; // comments
				}

				string argument = GettextPOEncoding.DecodeString(trimmedLine.Substring(addOffset).Trim());
				if (argument == null) { currentTarget = 0; } // invalid
				if (currentTarget < previousTarget) { previousTarget = currentTarget; nextEntry(); goto redoLine; }

				switch (currentTarget)
				{
					case 0:
						if (!line.StartsWith("#")) { goto ignoreLine; }
						entry.Whitespace.Add(line.TrimEnd());
						break;

					case 1:
						if (add) { entry.Context += argument; } else { entry.Context = argument; }
						break;

					case 2:
						if (add) { entry.ID += argument; } else { entry.ID = argument; }
						break;

					case 3:
						if (add) { entry.IDPlural += argument; } else { entry.IDPlural = argument; }
						break;

					case 4:
						if (add)
						{
							entry.Strings[index] += argument;
						}
						else
						{
							while (index >= entry.Strings.Count) { entry.Strings.Add(""); }
							entry.Strings[index] = argument;
						}
						break;
				}

				previousTarget = currentTarget;
			}

			nextEntry();
		}

		private void SaveRawString(Stream stream, string s)
		{
			byte[] bytes = Encoding.GetBytes(s);
			stream.Write(bytes, 0, bytes.Length);
		}

		private void EncodeAndSaveString(Stream stream, string prefix, string s)
		{
			SaveRawString(stream, prefix);
			foreach (string line in GettextPOEncoding.EncodeString(s))
			{
				SaveRawString(stream, line + "\n");
			}
		}

		protected override byte[] SaveOverride()
		{
			using (MemoryStream stream = new MemoryStream())
			{
				for (int i = 0; i < _entries.Count; i++)
				{
					Entry entry = _entries[i];

					foreach (string whitespace in entry.Whitespace) { SaveRawString(stream, whitespace + "\n"); }
					if (entry.Context != null) { EncodeAndSaveString(stream, "msgctxt ", entry.Context); }
					EncodeAndSaveString(stream, "msgid ", entry.ID);

					if (entry.IDPlural != null)
					{
						EncodeAndSaveString(stream, "msgid_plural ", entry.IDPlural);
					}

					for (int j = 0; j < entry.Strings.Count; j++)
					{
						EncodeAndSaveString(stream,
								   string.Format("msgstr{0} ", entry.Strings.Count > 1 ? "[" + j.ToString() + "]" : ""),
								   entry.Strings[j]);
					}

					if (i != _entries.Count - 1) { SaveRawString(stream, "\n"); }
				}

				return stream.ToArray();
			}
		}

		/// <summary>
		/// Saves the text representation of the .po file to a string.
		/// The current encoding is used.
		/// </summary>
		/// <returns>The string.</returns>
		public override string ToString()
		{
			return Encoding.GetString(Save());
		}

		protected override string GetStringOverride(string id, string context)
		{
			string key = new GettextKey(id, context: context).ToMOKeyString();

			Entry entry;
			return _lookup.TryGetValue(key, out entry) ? entry.Strings[0] : null;
		}

		private Entry GetOrCreateStringEntry(string key)
		{
			Entry entry;
			if (!_lookup.TryGetValue(key, out entry))
			{
				_lookup[key] = entry = new Entry();
				_entries.Add(entry);
			}
			return entry;
		}

		private void ChangedStringEntry(string key)
		{
			if (key == "") { ParseHeaders(); }
		}

		protected override void SetStringOverride(string id, string translation, string context)
		{
			string key = new GettextKey(id, context: context).ToMOKeyString();

			Entry entry = GetOrCreateStringEntry(key);
			entry.Context = context; entry.ID = id; entry.IDPlural = null;
			entry.Strings.Clear(); entry.Strings.Add(translation);
			ChangedStringEntry(key);
		}

		protected override string GetPluralStringByIndexOverride(string id, string idPlural, int index, string context)
		{
			string key = new GettextKey(id, idPlural: idPlural, context: context).ToMOKeyString();

			Entry entry;
			return _lookup.TryGetValue(key, out entry) && index < entry.Strings.Count ? entry.Strings[index] : null;
		}

		protected override void SetPluralStringOverride(string id, string idPlural, string[] translations, string context)
		{
			string key = new GettextKey(id, idPlural: idPlural, context: context).ToMOKeyString();

			Entry entry = GetOrCreateStringEntry(key);
			entry.Context = context; entry.ID = id; entry.IDPlural = idPlural;
			entry.Strings.Clear(); entry.Strings.AddRange(translations);
			ChangedStringEntry(key);
		}
	}
}