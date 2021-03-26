using System;
using System.IO;
using System.Reflection;
using UnityEngine;

internal class PngData
{
	public PngData(string name)
	{
		Debug.Log("test1");
		Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(PluginInfo.NameSpace + ".PngResource." + name);
		Debug.Log("test2");
		int num = (int)manifestResourceStream.Length;
		Debug.Log("test3");
		this.pngByteData = new byte[num];
		Debug.Log("test4");
		manifestResourceStream.Read(this.pngByteData, 0, num);
		Debug.Log("test5");
	}

	public byte[] GetData()
	{
		return this.pngByteData;
	}

	private byte[] pngByteData;
}
