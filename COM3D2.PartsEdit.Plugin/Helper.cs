using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;

internal static class Helper
{
	public static void Log(string s)
	{
		bool flag = !Helper.bLogEnable;
		if (!flag)
		{
			bool flag2 = Helper.logStreamWriter == null;
			if (flag2)
			{
				string path = ".\\Log_" + Helper.now.ToString("yyyyMMdd_HHmmss") + ".log";
				Helper.logStreamWriter = new StreamWriter(path, true);
			}
			Helper.logStreamWriter.Write(s);
			Helper.logStreamWriter.Write("\n");
			Helper.logStreamWriter.Flush();
		}
	}

	public static void Log(string format, params object[] args)
	{
		bool flag = !Helper.bLogEnable;
		if (!flag)
		{
			Helper.Log(string.Format(format, args));
		}
	}

	public static bool StringToBool(string s, bool defaultValue)
	{
		bool flag = s == null;
		bool result;
		if (flag)
		{
			result = defaultValue;
		}
		else
		{
			bool flag3;
			bool flag2 = bool.TryParse(s, out flag3);
			if (flag2)
			{
				result = flag3;
			}
			else
			{
				float num;
				bool flag4 = float.TryParse(s, out num);
				if (flag4)
				{
					result = (num > 0.5f);
				}
				else
				{
					int num2;
					bool flag5 = int.TryParse(s, out num2);
					if (flag5)
					{
						result = (num2 > 0);
					}
					else
					{
						result = defaultValue;
					}
				}
			}
		}
		return result;
	}

	public static int StringToInt(string s, int defaultValue)
	{
		int result;
		if (s == null || !int.TryParse(s, out result))
		{
			result = defaultValue;
		}
		return result;
	}

	public static float StringToFloat(string s, float defaultValue)
	{
		float result;
		if (s == null || !float.TryParse(s, out result))
		{
			result = defaultValue;
		}
		return result;
	}

	public static XmlDocument LoadXmlDocument(string xmlFilePath)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			bool flag = File.Exists(xmlFilePath);
			if (flag)
			{
				xmlDocument.Load(xmlFilePath);
			}
		}
		catch (Exception ex)
		{
			Helper.ShowException(ex);
		}
		return xmlDocument;
	}

	public static TEnum ToEnum<TEnum>(this string strEnumValue, TEnum defaultValue)
	{
		bool flag = !Enum.IsDefined(typeof(TEnum), strEnumValue);
		TEnum result;
		if (flag)
		{
			result = defaultValue;
		}
		else
		{
			result = (TEnum)((object)Enum.Parse(typeof(TEnum), strEnumValue));
		}
		return result;
	}

	public static FieldInfo GetFieldInfo(Type type, string fieldName)
	{
		return type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
	}

	public static object GetInstanceField(Type type, object instance, string fieldName)
	{
		FieldInfo fieldInfo = Helper.GetFieldInfo(type, fieldName);
		return (fieldInfo == null) ? null : fieldInfo.GetValue(instance);
	}

	public static void SetInstanceField(Type type, object instance, string fieldName, object val)
	{
		FieldInfo fieldInfo = Helper.GetFieldInfo(type, fieldName);
		bool flag = fieldInfo != null;
		if (flag)
		{
			fieldInfo.SetValue(instance, val);
		}
	}

	public static void ShowStackFrames(StackFrame[] stackFrames)
	{
		foreach (StackFrame stackFrame in stackFrames)
		{
			Console.WriteLine("{0}({1}.{2}) : {3}.{4}", new object[]
			{
				stackFrame.GetFileName(),
				stackFrame.GetFileLineNumber(),
				stackFrame.GetFileColumnNumber(),
				stackFrame.GetMethod().DeclaringType,
				stackFrame.GetMethod()
			});
		}
	}

	public static void ShowException(Exception ex)
	{
		Console.WriteLine("{0}", ex.Message);
		StackTrace stackTrace = new StackTrace(ex, true);
		Helper.ShowStackFrames(stackTrace.GetFrames());
	}

	public static Assembly GetCurrentAssembly()
	{
		return Assembly.GetExecutingAssembly();
	}

	public static FileVersionInfo GetCurrentAssemblyFileVersionInfo()
	{
		return FileVersionInfo.GetVersionInfo(Helper.GetCurrentAssembly().Location);
	}

	private static StreamWriter logStreamWriter = null;

	public static readonly DateTime now = DateTime.Now;

	public static bool bLogEnable = true;
}
