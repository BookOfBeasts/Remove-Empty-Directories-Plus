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
using System.Linq;

namespace SecondLanguage
{
	internal sealed class Throw
	{
		private Throw()
		{
		}

		public static Throw If
		{
			get { return null; }
		}
	}

	internal static class ThrowExtensions
	{
		public static Throw Null<T>(this Throw self, T value, string paramName)
		{
			if (value == null) { throw new ArgumentNullException(paramName); }
			return null;
		}

		public static Throw NullElements<T>(this Throw self, IEnumerable<T> values, string paramName)
		{
			Throw.If.Null(values, paramName);
			if (values.Any(value => value == null)) { throw new ArgumentException("Elements may not be null.", paramName); }
			return null;
		}
	}
}