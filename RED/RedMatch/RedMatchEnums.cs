namespace RED.Match
{
	public enum RedMatchMethodType
	{
		Invalid = 0,
		Contains,
		Endswith,
		Startwith,
		NameExact,
		NameExactWithPath,
		RegExName,
		RegExPath
	};

	public enum RedMatchFilterType
	{ Generic = 0, Directory, Files };
}