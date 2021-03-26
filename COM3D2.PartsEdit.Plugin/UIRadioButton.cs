using System;
using System.Collections.Generic;
using UnityEngine;

internal class UIRadioButton : IUIDrawer
{
	public UIRadioButton(int num)
	{
		this.size = num;
		this.radioButton = new RadioButton(this.size, false);
		this.nameList = new List<string>(this.size);
		this.nameList.ForEach(delegate(string x)
		{
		});
	}

	public UIRadioButton(List<string> fNameList)
	{
		this.size = fNameList.Count;
		this.radioButton = new RadioButton(this.size, false);
		this.nameList = fNameList;
	}

	public void DrawItem()
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		for (int i = 0; i < this.size; i++)
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			this.radioButton.Draw(i);
			GUILayout.Label(this.nameList[i], new GUILayoutOption[0]);
			GUILayout.EndHorizontal();
		}
		GUILayout.EndHorizontal();
	}

	private int size;

	private List<string> nameList;

	private RadioButton radioButton;
}
