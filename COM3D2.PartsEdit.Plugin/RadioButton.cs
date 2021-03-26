using System;
using System.Collections.Generic;
using UnityEngine;

internal class RadioButton
{
	public RadioButton(int num, bool allOff)
	{
		this.allOffAble = allOff;
		for (int i = 0; i < num; i++)
		{
			this.radioList.Add(false);
		}
		bool flag = !this.allOffAble;
		if (flag)
		{
			this.radioList[0] = true;
		}
	}

	public RadioButton(int num, int onNum, bool allOff)
	{
		this.allOffAble = allOff;
		for (int i = 0; i < num; i++)
		{
			this.radioList.Add(false);
		}
		this.Set(onNum, true);
	}

	public void Draw(int index)
	{
		bool flg = GUILayout.Toggle(this.radioList[index], "", new GUILayoutOption[]
		{
			GUILayout.MinWidth(20f),
			GUILayout.MaxWidth(20f)
		});
		this.Set(index, flg);
	}

	public void Set(int index, bool flg)
	{
		bool flag = flg && !this.radioList[index];
		if (flag)
		{
			for (int i = 0; i < this.radioList.Count; i++)
			{
				bool flag2 = i == index;
				if (flag2)
				{
					this.radioList[i] = true;
				}
				else
				{
					this.radioList[i] = false;
				}
			}
		}
		else
		{
			bool flag3 = !flg && this.radioList[index];
			if (flag3)
			{
				bool flag4 = this.allOffAble;
				if (flag4)
				{
					this.radioList[index] = false;
				}
			}
		}
	}

	public void SetAllOff()
	{
		bool flag = this.allOffAble;
		if (flag)
		{
			for (int i = 0; i < this.radioList.Count; i++)
			{
				this.radioList[i] = false;
			}
		}
	}

	public int GetIndex()
	{
		return this.radioList.IndexOf(true);
	}

	public bool GetBool(int index)
	{
		return this.radioList[index];
	}

	private List<bool> radioList = new List<bool>();

	private bool allOffAble;
}
