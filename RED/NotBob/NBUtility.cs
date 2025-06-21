using System;

namespace NotBob.Lib
{
	internal class NBUtility
	{
		internal static T ParseEnum<T>(string value)
		{
			return (T)Enum.Parse(typeof(T), value, true);
		}
	}
}