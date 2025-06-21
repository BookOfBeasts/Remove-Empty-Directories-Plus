#region License

/*
SecondLanguage Gettext Library for .NET
Copyright 2013, 2015 James F. Bellinger <http://www.zer7.com/software/secondlanguage>

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

namespace SecondLanguage
{
	/// <summary>
	/// Used to refer to sets of strings in Gettext-style translations.
	/// </summary>
	public struct GettextKey : IEquatable<GettextKey>
	{
		private string _id, _idPlural, _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="GettextKey"/> structure.
		/// </summary>
		/// <param name="id">The singular translation key.</param>
		/// <param name="idPlural">The plural translation key, if any, or <c>null</c>.</param>
		/// <param name="context">The context, if any, or <c>null</c>.</param>
		public GettextKey(string id, string idPlural = null, string context = null)
		{
			_id = id; _idPlural = idPlural; _context = context;
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			return obj is GettextKey && Equals((GettextKey)obj);
		}

		/// <summary>
		/// Tests the <see cref="GettextKey"/> for equality with another <see cref="GettextKey"/>.
		/// </summary>
		/// <param name="key">The key to compare with.</param>
		/// <returns>True if the keys are identical.</returns>
		public bool Equals(GettextKey key)
		{
			return ID == key.ID && IDPlural == key.IDPlural && Context == key.Context;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}

		/// <summary>
		/// Converts from the .mo file binary key string format to a <see cref="GettextKey"/>.
		/// </summary>
		/// <param name="keyString">The key string.</param>
		/// <returns>A <see cref="GettextKey"/>.</returns>
		public static GettextKey FromMOKeyString(string keyString)
		{
			Throw.If.Null(keyString, "keyString");

			GettextKey key = new GettextKey();
			string[] pluralSplit = keyString.Split(new[] { '\0' }, 2);
			string[] contextSplit = pluralSplit[0].Split(new[] { '\x04' }, 2);

			key.ID = pluralSplit[0];

			if (contextSplit.Length >= 2)
			{
				key.Context = contextSplit[0];
				key.ID = contextSplit[1];
			}

			if (pluralSplit.Length >= 2)
			{
				key.IDPlural = pluralSplit[1];
			}

			return key;
		}

		/// <summary>
		/// Converts the <see cref="GettextKey"/> into the binary key string format used by .mo files.
		/// </summary>
		/// <returns>A key string.</returns>
		public string ToMOKeyString()
		{
			string keyString = "";

			if (Context != null) { keyString += Context + '\x04'; }
			keyString += ID;

			if (IDPlural != null)
			{
				keyString += '\0';
				keyString += IDPlural;
			}

			return keyString;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return ID
				+ (IDPlural != null ? string.Format(" (plural: {0})", IDPlural) : "")
				+ (Context != null ? string.Format(" (context: {0})", Context) : "")
				;
		}

		/// <summary>
		/// The singular translation key.
		/// </summary>
		public string ID
		{
			get { return _id ?? ""; }
			set { _id = value; }
		}

		/// <summary>
		/// The plural translation key, if any, or <c>null</c>.
		/// </summary>
		public string IDPlural
		{
			get { return _idPlural; }
			set { _idPlural = value; }
		}

		/// <summary>
		/// The context, if any, or <c>null</c>.
		/// </summary>
		public string Context
		{
			get { return _context; }
			set { _context = value; }
		}

		/// <summary>
		/// Converts a translation key into a <see cref="GettextKey"/>.
		/// This implicit conversion does not include a plural translation key or a context.
		/// </summary>
		/// <param name="id">The translation key.</param>
		/// <returns>A new <see cref="GettextKey"/>.</returns>
		public static implicit operator GettextKey(string id)
		{
			return new GettextKey(id);
		}
	}
}