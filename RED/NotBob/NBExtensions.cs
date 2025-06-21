using System;
using System.Windows.Forms;

namespace NotBob.Lib
{
	internal static class NBExtensions
	{
		#region String Extensions

		/// <summary>
		/// Returns a cardinal number's ordinal equivalent
		/// (eg 1 returns 1st, 2 returns 2nd etc)
		/// </summary>
		public static string ToOrdinal(this int value)
		{
			// Start with the most common extension.
			string extension = "th";
			// Examine the last 2 digits.
			int last_digits = value % 100;
			// If the last digits are 11, 12, or 13, use th. Otherwise:
			if (last_digits < 11 || last_digits > 13)
			{
				// Check the last digit.
				switch (last_digits % 10)
				{
					case 1:
						extension = "st";
						break;
					case 2:
						extension = "nd";
						break;
					case 3:
						extension = "rd";
						break;
				}
			}
			return value.ToString() + extension;
		}

		// This method gets the enum value from its Description MetaData attribute
		// usage - var panda = EnumEx.GetValueFromDescription<Animal>("Giant Panda");
		//public static T GetEnumFromDescription<T>(this string description) where T : Enum
		//{
		//    foreach (var field in typeof(T).GetFields())
		//    {
		//        if (Attribute.GetCustomAttribute(field,
		//        typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
		//        {
		//            if (attribute.Description == description)
		//                return (T)field.GetValue(null);
		//        }
		//        else
		//        {
		//            if (field.Name == description)
		//                return (T)field.GetValue(null);
		//        }
		//    }
		//    return default(T);
		//}

		#endregion String Extensions

		public static void ForAllControls(this Control parent, Action<Control> action)
		{
			foreach (Control c in parent.Controls)
			{
				action(c);
				ForAllControls(c, action);
			}
		}
	}
}