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
using System.IO;

namespace SecondLanguage
{
	/// <summary>
	/// Provides functionality common to all translation types.
	/// </summary>
	public abstract class Translation
	{
		protected Translation()
		{
			Clear();
		}

		/// <summary>
		/// Resets the translation to a default state.
		/// All translated strings will be removed.
		/// </summary>
		public void Clear()
		{
			AboutToChange();

			ClearOverride();
		}

		protected abstract void ClearOverride();

		/// <summary>
		/// Loads a file from a buffer.
		/// </summary>
		/// <param name="buffer">The buffer to load from.</param>
		public void Load(byte[] buffer)
		{
			LoadOverride(buffer);
		}

		protected abstract void LoadOverride(byte[] buffer);

		/// <summary>
		/// Loads a file from disk.
		/// </summary>
		/// <param name="filename">The name of the file to load.</param>
		public void Load(string filename)
		{
			Throw.If.Null(filename, "filename");

			using (FileStream stream = File.OpenRead(filename))
			{
				Load(stream);
			}
		}

		/// <summary>
		/// Loads a file from a stream.
		/// </summary>
		/// <param name="stream">The stream to load from.</param>
		public void Load(Stream stream)
		{
			AboutToChange();
			Throw.If.Null(stream, "stream");

			byte[] buffer = new byte[stream.Length];
			stream.Seek(0, SeekOrigin.Begin);
			stream.Read(buffer, 0, buffer.Length);
			Load(buffer);
		}

		/// <summary>
		/// Saves the file into a buffer.
		/// </summary>
		/// <returns>A buffer containing the saved Gettext file.</returns>
		public byte[] Save()
		{
			return SaveOverride();
		}

		protected abstract byte[] SaveOverride();

		/// <summary>
		/// Saves the file to disk.
		/// </summary>
		/// <param name="filename">The filename to save to.</param>
		public void Save(string filename)
		{
			Throw.If.Null(filename, "filename");

			using (FileStream stream = File.Create(filename))
			{
				Save(stream);
			}
		}

		/// <summary>
		/// Saves the file to a stream.
		/// </summary>
		/// <param name="stream">The stream to save to.</param>
		public void Save(Stream stream)
		{
			Throw.If.Null(stream, "stream");

			byte[] buffer = Save();
			stream.Write(buffer, 0, buffer.Length);
		}

		/// <summary>
		/// Gets the translated string corresponding to <paramref name="id"/>.
		/// </summary>
		/// <param name="id">The translation key.</param>
		/// <param name="context">The context, if any, or <c>null</c>.</param>
		/// <returns>The translated string, or <c>null</c> if none is set.</returns>
		public string GetString(string id, string context = null)
		{
			return GetStringOverride(id, context);
		}

		protected abstract string GetStringOverride(string id, string context);

		/// <summary>
		/// Gets the translated string corresponding to <paramref name="id"/> and <paramref name="idPlural"/>
		/// appropriate for the numeric value <paramref name="value"/>.
		/// </summary>
		/// <param name="id">The singular translation key.</param>
		/// <param name="idPlural">The plural translation key.</param>
		/// <param name="value">The value to look up the plural string for.</param>
		/// <param name="context">The context, if any, or <c>null</c>.</param>
		/// <returns>The translated string, or <c>null</c> if none is set.</returns>
		public string GetPluralString(string id, string idPlural, long value, string context = null)
		{
			return GetPluralStringOverride(id, idPlural, value, context);
		}

		protected abstract string GetPluralStringOverride(string id, string idPlural, long value, string context);

		/// <summary>
		/// Sets the translated string corresponding to <paramref name="id"/>.
		/// </summary>
		/// <param name="id">The translation key.</param>
		/// <param name="translation">The translated string.</param>
		/// <param name="context">The context, if any, or <c>null</c>.</param>
		public void SetString(string id, string translation, string context = null)
		{
			AboutToChange();
			SetStringOverride(id, translation, context);
		}

		protected abstract void SetStringOverride(string id, string translation, string context);

		/// <summary>
		/// Makes the <see cref="GettextTranslation"/> read-only.
		/// </summary>
		public void MakeReadOnly()
		{
			IsReadOnly = true;
		}

		protected void AboutToChange()
		{
			if (IsReadOnly) { throw new InvalidOperationException("Translation is read-only."); }
		}

		/// <summary>
		/// <c>null</c> if the file cannot be modified.
		/// </summary>
		public bool IsReadOnly
		{
			get;
			private set;
		}
	}
}