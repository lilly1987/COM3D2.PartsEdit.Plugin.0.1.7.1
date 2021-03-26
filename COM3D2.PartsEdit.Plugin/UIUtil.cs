using System;
using UnityEngine;

internal static class UIUtil
{
	public static void BeginIndentArea()
	{
		UIUtil.BeginIndentArea(UIUtil.indentWidth);
	}

	public static void BeginIndentArea(int width)
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label("", new GUILayoutOption[]
		{
			GUILayout.Width((float)width)
		});
		GUILayout.BeginVertical(new GUILayoutOption[0]);
	}

	public static void EndoIndentArea()
	{
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
	}

	private static int indentWidth = 5;
}
