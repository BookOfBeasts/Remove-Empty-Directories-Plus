using System.Collections;
using System.Collections.Generic;
using Alphaleonis.Win32.Filesystem;

namespace RED.Match
{
	public class RedMatchItemList : IEnumerable<RedMatchItem>
	{
		public RedMatchItemList()
		{
			FilterType = RedMatchFilterType.Generic;
			Items = new List<RedMatchItem>();
		}

		public RedMatchItemList(RedMatchFilterType filterType) : this()
		{
			FilterType = filterType;
		}

		// Default Indexer for this object
		public RedMatchItem this[int index]
		{
			get { return Items[index]; }
			set { Items[index] = value; }
		}

		public IEnumerator<RedMatchItem> GetEnumerator()
		{
			return Items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public RedMatchFilterType FilterType { get; private set; }

		public List<RedMatchItem> Items { get; private set; }

		public List<string> ToStringList()
		{
			List<string> list = new List<string>();
			foreach (RedMatchItem item in Items)
			{
				list.Add(item.ToString());
			}
			return list;
		}

		public void AddItem(bool enabled, RedMatchMethodType matchMethod, string matchText)
		{
			AddItem(RedMatchItem.FormatToString(enabled, matchMethod, matchText));
		}

		private void AddItem(string v)
		{
			/// The text is a pipe delimited string in the order "EnabledFlag|MatchCode|MatchText"
			/// EnabledFflag is a single character. + for enabled, - for disabled
			/// MatchCode is a short code indicating MatchMethod to be used
			/// MatchText is the text to be matched against directory or file names
			string enabledFlag;
			string matchCode;
			string matchText;

			if (!string.IsNullOrWhiteSpace(v))
			{
				string[] items = v.Trim().Split('|');
				switch (items.Length)
				{
					case 1:
						enabledFlag = "+";
						matchCode = "N";
						matchText = v;
						break;
					case 2:
						enabledFlag = "+";
						matchCode = items[0];
						matchText = items[1];
						break;
					case 3:
						enabledFlag = items[0];
						matchCode = items[1];
						matchText = items[2];
						break;
					default:
						// invalid
						enabledFlag = "-";
						matchCode = "?";
						matchText = v;
						break;
				}

				// MatchText only
				if (matchText.Contains("*") || (matchText.StartsWith("/") && matchText.EndsWith("/")))
				{
					matchCode = "RN";
				}

				RedMatchMethodType matchMethod = RedMatchItem.CodeToMatchMethod(matchCode);

				if (matchMethod != RedMatchMethodType.Invalid && !string.IsNullOrWhiteSpace(matchText))
				{
					Items.Add(new RedMatchItem(matchMethod, matchText, enabledFlag == "+" ? true : false));
				}
			}
		}

		public void Transform(List<string> v, RedMatchFilterType filterType)
		{
			FilterType = filterType;
			Transform(v);
		}

		public void Transform(List<string> v)
		{
			Items.Clear();
			foreach (string item in v)
			{
				if (!string.IsNullOrWhiteSpace(item))
				{
					AddItem(item);
				}
			}
		}

		public bool IsOnList(DirectoryInfo dirinfo)
		{
			string nameToCheck = dirinfo.Name.ToLowerInvariant();
			string pathToCheck = dirinfo.FullName.ToLowerInvariant();
			return IsOnList(nameToCheck, pathToCheck, 0, false, out _);
		}

		public bool IsOnList(FileInfo fileinfo, int filesize, bool ignoreEmptyFiles, out string delPattern)
		{
			string nameToCheck = fileinfo.Name.ToLowerInvariant();
			string pathToCheck = fileinfo.FullName.ToLowerInvariant();
			return IsOnList(nameToCheck, pathToCheck, filesize, ignoreEmptyFiles, out delPattern);
		}

		private string GetCheckText(string matchText, string nameToCheck, string pathToCheck)
		{
			if (FilterType == RedMatchFilterType.Directory)
			{
				return matchText.Contains(Path.DirectorySeparatorChar.ToString()) ? pathToCheck : nameToCheck;
			}
			else
			{
				return nameToCheck;
			}
		}

		private bool IsOnList(string nameToCheck, string pathToCheck, int filesize, bool ignoreEmptyFiles, out string delPattern)
		{
			bool matched = false;

			delPattern = "";

			if (Items.Count > 0)
			{
				string textToCheck;

				if (!matched && FilterType == RedMatchFilterType.Files)
				{
					if (ignoreEmptyFiles && filesize == 0)
					{
						delPattern = "[Empty file]";
						matched = true;
					}
				}
				if (!matched)
				{
					foreach (RedMatchItem matchItem in Items.FindAll(x => x.MatchMethod == RedMatchMethodType.NameExact && x.Enabled))
					{
						if (nameToCheck == matchItem.MatchTextToCompare)
						{
							matched = true;
							delPattern = matchItem.MatchText;
							break;
						}
					}
				}
				if (!matched)
				{
					foreach (RedMatchItem matchItem in Items.FindAll(x => x.MatchMethod == RedMatchMethodType.Contains && x.Enabled))
					{
						textToCheck = GetCheckText(matchItem.MatchTextToCompare, nameToCheck, pathToCheck);
						if (textToCheck.Contains(matchItem.MatchTextToCompare))
						{
							matched = true;
							delPattern = matchItem.MatchText;
							break;
						}
					}
				}
				if (!matched)
				{
					foreach (RedMatchItem matchItem in Items.FindAll(x => x.MatchMethod == RedMatchMethodType.Endswith && x.Enabled))
					{
						textToCheck = GetCheckText(matchItem.MatchTextToCompare, nameToCheck, pathToCheck);
						if (textToCheck.EndsWith(matchItem.MatchTextToCompare))
						{
							matched = true;
							delPattern = matchItem.MatchText;
							break;
						}
					}
				}
				if (!matched)
				{
					foreach (RedMatchItem matchItem in Items.FindAll(x => x.MatchMethod == RedMatchMethodType.Startwith && x.Enabled))
					{
						textToCheck = GetCheckText(matchItem.MatchTextToCompare, nameToCheck, pathToCheck);
						if (textToCheck.StartsWith(matchItem.MatchTextToCompare))
						{
							matched = true;
							delPattern = matchItem.MatchText;
							break;
						}
					}
				}
				if (!matched)
				{
					foreach (RedMatchItem matchItem in Items.FindAll(x => x.MatchMethod == RedMatchMethodType.RegExName && x.Enabled))
					{
						if (matchItem.RegEx.IsMatch(nameToCheck))
						{
							matched = true;
							delPattern = matchItem.MatchText;
							break;
						}
					}
				}
				if (!matched && FilterType == RedMatchFilterType.Directory)
				{
					if (!matched)
					{
						foreach (RedMatchItem matchItem in Items.FindAll(x => x.MatchMethod == RedMatchMethodType.RegExPath && x.Enabled))
						{
							if (matchItem.RegEx.IsMatch(pathToCheck))
							{
								matched = true;
								delPattern = matchItem.MatchText;
								break;
							}
						}
					}
					if (!matched)
					{
						foreach (RedMatchItem matchItem in Items.FindAll(x => x.MatchMethod == RedMatchMethodType.NameExactWithPath && x.Enabled))
						{
							if (pathToCheck == matchItem.MatchTextToCompare)
							{
								matched = true;
								delPattern = matchItem.MatchText;
								break;
							}
						}
					}
				}
			}
			return matched;
		}
	}
}