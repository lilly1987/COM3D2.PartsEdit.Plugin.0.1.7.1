using System;

internal static class PluginInfo
{
	public static string Name
	{
		get
		{
			return PluginInfo.name;
		}
	}

	public static string Version
	{
		get
		{
			return PluginInfo.version;
		}
	}

	public static string NameSpace
	{
		get
		{
			return PluginInfo.nameSpace;
		}
	}

	public static void SetInfo(string name, string version, string nameSpace)
	{
		PluginInfo.name = name;
		PluginInfo.version = version;
		PluginInfo.nameSpace = nameSpace;
	}

	private static string name;

	private static string version;

	private static string nameSpace;
}
