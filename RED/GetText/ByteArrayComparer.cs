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

namespace SecondLanguage
{
	internal sealed class ByteArrayComparer : IComparer<byte[]>
	{
		public int Compare(byte[] x, byte[] y)
		{
			for (int i = 0; i < Math.Min(x.Length, y.Length); i++)
			{
				if (x[i] < y[i]) { return -1; }
				if (x[i] > y[i]) { return 1; }
			}

			if (x.Length < y.Length) { return -1; }
			if (x.Length > y.Length) { return 1; }
			return 0;
		}
	}
}