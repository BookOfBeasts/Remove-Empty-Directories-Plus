#region License

/*
SecondLanguage Gettext Library for .NET
Copyright 2013, 2017 James F. Bellinger <http://www.zer7.com/software/secondlanguage>

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
using System.Linq;
using System.Text;

namespace SecondLanguage
{
	/// <summary>
	/// Provides common functionality for Gettext files.
	/// </summary>
	public abstract class GettextTranslation : Translation
	{
		private Encoding _encoding;
		private Dictionary<string, string> _headers = new Dictionary<string, string>();
		private string _pluralForms; private int _pluralFormsCount; private GettextPluralConverterFunc _pluralFormsFunc;

		protected GettextTranslation()
		{
		}

		/// <summary>
		/// Retrieves the <see cref="GettextKey"/> of each untranslated string and its corresponding translated strings.
		/// </summary>
		/// <returns>Untranslated and translated strings.</returns>
		public abstract IEnumerable<KeyValuePair<GettextKey, string[]>> GetGettextKeys();

		protected override void ClearOverride()
		{
			_headers.Clear();
		}

		/// <summary>
		/// Gets the value of a header entry.
		/// </summary>
		/// <param name="key">The key of the header entry.</param>
		/// <returns>The value of the header entry.</returns>
		public string GetHeader(string key)
		{
			Throw.If.Null(key, "key");

			string value;
			return _headers.TryGetValue(key, out value) ? value : null;
		}

		protected override sealed string GetPluralStringOverride(string id, string idPlural, long value, string context)
		{
			int index = GetPluralIndex(value);
			return GetPluralStringByIndexOverride(id, idPlural, index, context);
		}

		/// <summary>
		/// Gets the translated string corresponding to <paramref name="id"/> and <paramref name="idPlural"/>.
		/// <paramref name="index"/> should be determined by calling <see cref="GetPluralIndex"/>.
		/// </summary>
		/// <param name="id">The singular translation key.</param>
		/// <param name="idPlural">The plural translation key.</param>
		/// <param name="index">The index of the plural string.</param>
		/// <param name="context">The context, if any, or <c>null</c>.</param>
		/// <returns>The translated string, or <c>null</c> if none is set.</returns>
		public string GetPluralStringByIndex(string id, string idPlural, int index, string context = null)
		{
			Throw.If.Null(id, "id").Null(idPlural, "idPlural");

			return GetPluralStringByIndexOverride(id, idPlural, index, context);
		}

		protected abstract string GetPluralStringByIndexOverride(string id, string idPlural, int index, string context);

		/// <summary>
		/// Sets the value of a header entry.
		/// </summary>
		/// <param name="key">The key of the header entry.</param>
		/// <param name="value">The value of the header entry.</param>
		public void SetHeader(string key, string value)
		{
			AboutToChange();
			Throw.If.Null(key, "key").Null(value, "value");

			if (GetHeader(key) == value) { return; }
			_headers[key] = value;
			SetString("", string.Join("\n", _headers.Select(kvp => kvp.Key + ": " + kvp.Value).ToArray()));
		}

		/// <summary>
		/// Sets the translated strings corresponding to <paramref name="id"/> and <paramref name="idPlural"/>.
		/// </summary>
		/// <param name="id">The singular translation key.</param>
		/// <param name="idPlural">The plural translation key.</param>
		/// <param name="translations">The translated strings.</param>
		/// <param name="context">The context, if any, or <c>null</c>.</param>
		public void SetPluralString(string id, string idPlural, string[] translations, string context = null)
		{
			AboutToChange();
			Throw.If.Null(id, "id").Null(idPlural, "idPlural").NullElements(translations, "translations");
			if (translations.Length == 0) { throw new ArgumentException("Must have at least one translation.", "translations"); }

			SetPluralStringOverride(id, idPlural, translations, context);
		}

		protected abstract void SetPluralStringOverride(string id, string idPlural, string[] translations, string context);

		/// <summary>
		/// Gets the index of the plural string for a particular value.
		/// </summary>
		/// <param name="value">The value a translation is needed for.</param>
		/// <returns>The index of the plural string.</returns>
		public int GetPluralIndex(long value)
		{
			return _pluralFormsFunc(value);
		}

		protected void ParseHeaders()
		{
			AboutToChange();
			string headers = GetString(""); Throw.If.Null(headers, "headers");

			_headers.Clear();
			string contentType = DefaultContentType;
			string pluralForms = DefaultPluralForms;

			foreach (string line in headers.Split('\n'))
			{
				int index = line.IndexOf(": ");
				if (index >= 0)
				{
					string key = line.Substring(0, index);
					string value = line.Substring(index + 2);
					_headers[key] = value;

					if (key == "Content-Type")
					{
						contentType = value;
					}
					else if (key == "Plural-Forms")
					{
						pluralForms = value;
					}
				}
			}

			ParseContentType(contentType);
			ParsePluralForms(pluralForms);
		}

		private void ParseContentType(string contentType)
		{
			AboutToChange();
			Throw.If.Null(contentType, "contentType");

			_encoding = Encoding.ASCII;

			const string prefix = "text/plain; charset=";
			if (contentType.StartsWith(prefix, true, CultureInfo.InvariantCulture))
			{
				string encodingName = contentType.Substring(prefix.Length);

				if (encodingName == "utf-8")
				{
					_encoding = Encoding.UTF8;
				}
				else
				{
					foreach (EncodingInfo f in Encoding.GetEncodings())
					{
						try
						{
							Encoding encoding = f.GetEncoding();
							if (encoding.BodyName.Equals(encodingName, StringComparison.OrdinalIgnoreCase))
							{
								_encoding = encoding;
								break;
							}
						}
						catch (InvalidOperationException)
						{
						}
						catch (NotSupportedException)
						{
							// This can happen if Mono I18N is not installed.
						}
					}
				}
			}
		}

		private void ParsePluralForms(string pluralForms)
		{
			AboutToChange();
			Throw.If.Null(pluralForms, "pluralForms");

			if (_pluralForms != pluralForms)
			{
				_pluralForms = pluralForms;

				try
				{
					GettextPluralParser.Parse(pluralForms, out _pluralFormsCount, out _pluralFormsFunc);
				}
				catch (FormatException)
				{
					GettextPluralParser.Parse(DefaultPluralForms, out _pluralFormsCount, out _pluralFormsFunc);
				}
			}
		}

		protected string DefaultContentType
		{
			get { return "text/plain; charset=ascii"; }
		}

		protected string DefaultHeaders
		{
			get
			{
				return "Project-Id-Version: \n"
					 + "PO-Revision-Date: \n"
					 + "Last-Translator: \n"
					 + "Language-Team: \n"
					 + "MIME-Version: 1.0\n"
					 + "Content-Type: text/plain; charset=utf-8\n"
					 + "Content-Transfer-Encoding: 8bit\n"
					 + "Plural-Forms: " + DefaultPluralForms + "\n"
					 ;
			}
		}

		protected string DefaultPluralForms
		{
			get { return "nplurals=2; plural=(n != 1);"; }
		}

		/// <summary>
		/// The encoding of the Gettext file.
		/// A good choice is UTF-8.
		/// </summary>
		public Encoding Encoding
		{
			get
			{
				return _encoding;
			}
			set
			{
				AboutToChange();
				Throw.If.Null(value, "value");
				SetHeader("Content-Type", "text/plain; charset=" + value.BodyName);
			}
		}
	}
}