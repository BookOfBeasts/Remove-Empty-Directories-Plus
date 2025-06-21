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
using System.Text.RegularExpressions;

namespace SecondLanguage
{
	/// <summary>
	/// Most Gettext files have non-.NET format strings.
	/// Pass these methods to <see cref="Translator.FormatCallback"/>.
	/// </summary>
	public static class SpecialFormatters
	{
		// See http://en.wikipedia.org/wiki/Printf#Format_placeholders for what can go here.
		// I haven't yet coded support for all of these yet, but matching them is a start, and really,
		// %s and %d covers 99% of uses.
		private static readonly Regex CPrintfRegex = new Regex
			(@"\%((?<parameter>[0-9]{1,6})\$)?(?<flags>[\+ \-\#0]{0,5})(?<width>[0-9]{0,7}|\*)(\.(?<precision>[0-9]{0,7}|\*))?(?<length>[hlLzjtIq32]{0,2})(?<type>[diufFeEgGxXoscpn\%])",
			 RegexOptions.CultureInvariant | RegexOptions.Multiline);

		/// <summary>
		/// Applies C printf style formatting.
		///
		/// The POSIX positional argument extension is supported.
		/// </summary>
		/// <param name="format">The format string.</param>
		/// <param name="args">Arguments to replace the format string's placeholders with.</param>
		/// <returns>The formatted translated string.</returns>
		public static string C(string format, params object[] args)
		{
			return CustomC(null, format, args);
		}

		/// <summary>
		/// Applies C printf style formatting, using a specific format provider.
		/// </summary>
		/// <param name="provider">The format provider to use, or <c>null</c> for the system default.</param>
		/// <param name="format">The format string.</param>
		/// <param name="args">Arguments to replace the format string's placeholders with.</param>
		/// <returns>The formatted translated string.</returns>
		public static string CustomC(IFormatProvider provider, string format, params object[] args)
		{
			Throw.If.Null(format, "format").Null(args, "args");

			int i = 0;
			Func<object> getArg = () =>
				{
					object arg = i >= 0 && i < args.Length ? args[i] : null;
					i++; return arg;
				};
			return CPrintfRegex.Replace(format, match =>
			{
				string parameter = match.Groups["parameter"].Value,
					flags = match.Groups["flags"].Value,
					widthString = match.Groups["width"].Value,
					precisionString = match.Groups["precision"].Value,
					length = match.Groups["length"].Value,
					type = match.Groups["type"].Value;

				if (match.Value == "%%") { return "%"; }

				if (parameter != "") { i = int.Parse(parameter) - 1; }

				int width = 0;
				if (widthString != "") { width = widthString == "*" ? getArg() as int? ?? 0 : int.Parse(widthString); }

				int? precision = null;
				if (precisionString != "") { precision = precisionString == "*" ? getArg() as int? : int.Parse(precisionString); }

				object value = getArg() ?? "";
				string s = value.ToString() ?? "";

				if ("xX".Contains(type) && value is IFormattable)
				{
					s = ((IFormattable)value).ToString(type, provider);
				}

				if (precision < 0) { precision = 0; }
				if ("fFeEgG".Contains(type) && value is IFormattable)
				{
					s = ((IFormattable)value).ToString(type + precision.ToString(), provider);
				}
				else if (s.Length > precision)
				{
					s = s.Substring(0, (int)precision);
				}

				int padCount = Math.Abs(width) - s.Length;
				char padChar = flags.Contains("0") ? '0' : ' ';
				if (padCount > 0)
				{
					bool left = flags.Contains("-") ^ (width < 0);
					string padding = new string(padChar, padCount);
					if (left) { s += padding; } else { s = padding + s; }
				}

				return s;
			});
		}
	}
}