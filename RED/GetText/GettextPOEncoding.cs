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

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecondLanguage
{
	internal static class GettextPOEncoding
	{
		public static string DecodeString(string s)
		{
			s = s.Trim();
			if (!(s.Length >= 2 && s[0] == '"' && s[s.Length - 1] == '"')) { return null; }
			s = s.Substring(1, s.Length - 2);

			StringBuilder sb = new StringBuilder(); char ch;
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == '\\' && i + 1 < s.Length)
				{
					i++;

					switch (s[i])
					{
						case 'n': ch = '\n'; break;
						case 't': ch = '\t'; break;
						case 'b': ch = '\b'; break;
						case 'r': ch = '\r'; break;
						case 'f': ch = '\f'; break;
						case 'v': ch = '\v'; break;
						case 'a': ch = '\a'; break;
						default: ch = s[i]; break;
					}
				}
				else
				{
					ch = s[i];
				}

				sb.Append(ch);
			}

			return sb.ToString();
		}

		// See http://stackoverflow.com/questions/10218631/printing-with-gettext
		private static string EncodeStringLine(string s)
		{
			return "\""
				+ new string(s.SelectMany(ch =>
				{
					switch (ch)
					{
						case '\n': return new[] { '\\', 'n' };
						case '\t': return new[] { '\\', 't' };
						case '\b': return new[] { '\\', 'b' };
						case '\r': return new[] { '\\', 'r' };
						case '\f': return new[] { '\\', 'f' };
						case '\v': return new[] { '\\', 'v' };
						case '\a': return new[] { '\\', 'a' };
						case '\"': return new[] { '\\', '"' };
						case '\\': return new[] { '\\', '\\' };
						default: return new[] { ch };
					}
				}).ToArray())
				+ "\"";
		}

		public static IEnumerable<string> EncodeString(string s)
		{
			int nlIndex = s.IndexOf('\n');
			if (nlIndex >= 0 && nlIndex != s.Length - 1) // If it's at the end of the line, we won't care.
			{
				yield return EncodeStringLine(""); // Use Gettext multiline string convention of starting with "" line.

				for (int i = 0; i < s.Length;)
				{
					int j = s.IndexOf('\n', i) + 1;
					if (j == 0) { j = s.Length; }
					yield return EncodeStringLine(s.Substring(i, j - i));
					i = j;
				}
			}
			else
			{
				yield return EncodeStringLine(s);
			}
		}
	}
}