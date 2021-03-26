using System;
using System.Diagnostics;
using ExIni;
using UnityInjector;

internal static class IniUtil
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event IniUtil.Delegate saveMethod;

	public static void Init(PluginBase pBase)
	{
		IniUtil.preferences = pBase.Preferences;
	}

	public static int GetIntValue(string section, string key, int defaultValue)
	{
		IniKey iniKey = IniUtil.preferences[section][key];
		int num;
		int result;
		if (iniKey == null || string.IsNullOrEmpty(iniKey.Value) || !int.TryParse(iniKey.Value, out num))
		{
			result = defaultValue;
		}
		else
		{
			result = num;
		}
		return result;
	}

	public static float GetFloatValue(string section, string key, float defaultValue)
	{
		IniKey iniKey = IniUtil.preferences[section][key];
		float num;
		float result;
		if (iniKey == null || string.IsNullOrEmpty(iniKey.Value) || !float.TryParse(iniKey.Value, out num))
		{
			result = defaultValue;
		}
		else
		{
			result = num;
		}
		return result;
	}

	public static string GetStringValue(string section, string key, string defaultValue)
	{
		IniKey iniKey = IniUtil.preferences[section][key];
		bool flag = iniKey == null || iniKey.Value == null;
		string result;
		if (flag)
		{
			result = defaultValue;
		}
		else
		{
			result = iniKey.Value;
		}
		return result;
	}

	public static void Save()
	{
		IniUtil.saveMethod();
	}

	// Note: this type is marked as 'beforefieldinit'.
	static IniUtil()
	{
		IniUtil.saveMethod = delegate()
		{
		};
	}

	public static IniFile preferences;

	public delegate void Delegate();
}
