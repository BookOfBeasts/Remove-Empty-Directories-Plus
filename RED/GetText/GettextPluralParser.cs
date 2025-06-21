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
using System.Linq;

namespace SecondLanguage
{
	/// <summary>
	/// Converts a value into a plural index.
	/// If need be, a function to perform this conversion can be obtained from <see cref="GettextPluralParser"/>.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>The plural index.</returns>
	public delegate int GettextPluralConverterFunc(long value);

	/// <summary>
	/// Parses the Gettext Plural-Forms header.
	/// </summary>
	public sealed class GettextPluralParser
	{
		private string _pluralFormat;
		private uint _position;

		private GettextPluralParser()
		{
		}

		private void Advance(uint count = 1)
		{
			checked { _position += count; }
		}

		private void Expect(bool condition)
		{
			if (!condition)
			{
				throw new FormatException("Bad grammar.");
			}
		}

		private char Peek(uint offset = 0)
		{
			uint pos = _position + offset;
			return pos < _pluralFormat.Length ? _pluralFormat[(int)pos] : '\0';
		}

		private bool MoreToRead()
		{
			return _position < (uint)_pluralFormat.Length;
		}

		private static Func<ulong, ulong> BinaryOp(Func<ulong, ulong> lhs,
										   Func<ulong, ulong> rhs,
										   Func<ulong, ulong, ulong> func)
		{
			return x => func(lhs(x), rhs(x));
		}

		private Func<ulong, ulong> MatchValue()
		{
			if (Peek() == '(')
			{
				Advance();
				Func<ulong, ulong> value = MatchExpression();
				Expect(Peek() == ')'); Advance();
				return value;
			}
			else if (Peek() == 'n')
			{
				Advance(); return x => x;
			}
			else
			{
				uint length = 0;
				for (length = 0; Peek(length) >= '0' && Peek(length) <= '9'; length++)
                {
                    ;
                }

                Expect(length > 0);

				ulong number;
				Expect(ulong.TryParse(_pluralFormat.Substring((int)_position, (int)length), out number));
				Advance(length); return x => number;
			}
		}

		private Func<ulong, ulong> MatchMulDivMod()
		{
			Func<ulong, ulong> value = MatchValue();

			while (MoreToRead())
			{
				if (Peek() == '*')
				{
					Advance();
					value = BinaryOp(value, MatchValue(), (x, y) => x * y);
				}
				else if (Peek() == '/')
				{
					Advance();
					value = BinaryOp(value, MatchValue(), (x, y) => x / y);
				}
				else if (Peek() == '%')
				{
					Advance();
					value = BinaryOp(value, MatchValue(), (x, y) => x % y);
				}
				else
				{
					break;
				}
			}

			return value;
		}

		private Func<ulong, ulong> MatchAddSub()
		{
			Func<ulong, ulong> value = MatchMulDivMod();

			while (MoreToRead())
			{
				if (Peek() == '+')
				{
					Advance();
					value = BinaryOp(value, MatchMulDivMod(), (x, y) => x + y);
				}
				else if (Peek() == '-')
				{
					Advance();
					value = BinaryOp(value, MatchMulDivMod(), (x, y) => x - y);
				}
				else
				{
					break;
				}
			}

			return value;
		}

		private Func<ulong, ulong> MatchComparison()
		{
			Func<ulong, ulong> value = MatchAddSub();

			while (MoreToRead())
			{
				if (Peek() == '<')
				{
					Advance();
					if (Peek() == '=')
					{
						Advance();
						value = BinaryOp(value, MatchAddSub(), (x, y) => x <= y ? 1u : 0);
					}
					else
					{
						value = BinaryOp(value, MatchAddSub(), (x, y) => x < y ? 1u : 0);
					}
				}
				else if (Peek() == '>')
				{
					Advance();
					if (Peek() == '=')
					{
						Advance();
						value = BinaryOp(value, MatchAddSub(), (x, y) => x >= y ? 1u : 0);
					}
					else
					{
						value = BinaryOp(value, MatchAddSub(), (x, y) => x > y ? 1u : 0);
					}
				}
				else
				{
					break;
				}
			}

			return value;
		}

		private Func<ulong, ulong> MatchEquality()
		{
			Func<ulong, ulong> value = MatchComparison();

			while (MoreToRead())
			{
				if (Peek() == '=' && Peek(1) == '=')
				{
					Advance(2);
					value = BinaryOp(value, MatchComparison(), (x, y) => x == y ? 1u : 0);
				}
				else if (Peek() == '!' && Peek(1) == '=')
				{
					Advance(2);
					value = BinaryOp(value, MatchComparison(), (x, y) => x != y ? 1u : 0);
				}
				else
				{
					break;
				}
			}

			return value;
		}

		private Func<ulong, ulong> MatchAndOr()
		{
			Func<ulong, ulong> value = MatchEquality();

			while (MoreToRead())
			{
				if (Peek() == '&' && Peek(1) == '&')
				{
					Advance(2);
					value = BinaryOp(value, MatchEquality(), (x, y) => x != 0 && y != 0 ? 1u : 0);
				}
				else if (Peek() == '|' && Peek(1) == '|')
				{
					Advance(2);
					value = BinaryOp(value, MatchEquality(), (x, y) => x != 0 || y != 0 ? 1u : 0);
				}
				else
				{
					break;
				}
			}

			return value;
		}

		private Func<ulong, ulong> MatchTernary()
		{
			Func<ulong, ulong> value = MatchAndOr();

			if (Peek() == '?')
			{
				Advance();

				Func<ulong, ulong> condition = value;
				Func<ulong, ulong> left = MatchTernary();
				Expect(Peek() == ':'); Advance();
				Func<ulong, ulong> right = MatchTernary();

				value = x => condition(x) != 0 ? left(x) : right(x);
			}

			return value;
		}

		private Func<ulong, ulong> MatchExpression()
		{
			return MatchTernary();
		}

		/// <summary>
		/// Parses a Gettext Plural-Forms header, returning the number of plurals and
		/// a function that converts quantities into string indices.
		/// </summary>
		/// <param name="pluralForms">The value of the Plural-Forms header.</param>
		/// <param name="nplurals">The number of plurals.</param>
		/// <param name="valueToIndexFunc">A function that converts quantities into string indices.</param>
		public static void Parse(string pluralForms,
								 out int nplurals, out GettextPluralConverterFunc valueToIndexFunc)
		{
			Throw.If.Null(pluralForms, "pluralForms");

			pluralForms = new string(pluralForms.Where(ch => ch != ' ').ToArray());

			string pluralExpression = null; nplurals = -1;
			foreach (string component in pluralForms.Split(';'))
			{
				string[] parts = component.Split(new[] { '=' }, 2);
				if (parts.Length != 2) { continue; }

				switch (parts[0])
				{
					case "nplurals":
						int npvalue;
						if (int.TryParse(parts[1], out npvalue) && npvalue >= 0) { nplurals = npvalue; }
						break;

					case "plural":
						pluralExpression = parts[1];
						break;
				}
			}

			if (nplurals < 0)
			{
				throw new FormatException("No nplurals parameter.");
			}

			if (pluralExpression == null)
			{
				throw new FormatException("No plural parameter.");
			}

			GettextPluralParser builder = new GettextPluralParser() { _pluralFormat = pluralExpression };
			Func<ulong, ulong> func = builder.MatchExpression();
			builder.Expect(!builder.MoreToRead());

			valueToIndexFunc = value =>
				{
					if (value < 0) { value = -value; }
					return (int)func((ulong)value);
				};
		}
	}
}