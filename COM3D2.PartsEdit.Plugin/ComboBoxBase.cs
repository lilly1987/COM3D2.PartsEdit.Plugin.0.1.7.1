using System;
using UnityEngine;

internal class ComboBoxBase
{
	protected ComboBoxBase(GUIContent buttonContent, GUIContent[] listContent, GUIStyle listStyle) : this(buttonContent, listContent, "button", "box", listStyle)
	{
	}

	protected ComboBoxBase(GUIContent buttonContent, GUIContent[] listContent, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle)
	{
		this.buttonContent = buttonContent;
		this.listContent = listContent;
		this.buttonStyle = buttonStyle;
		this.boxStyle = boxStyle;
		this.listStyle = listStyle;
		this.InitIndex();
		this.InitSize();
	}

	protected void InitSize()
	{
		int num = 0;
		foreach (GUIContent guicontent in this.listContent)
		{
			bool flag = num < guicontent.text.Length;
			if (flag)
			{
				num = guicontent.text.Length;
			}
		}
		this.itemWidth = (float)num * 9f;
		this.itemHeight = this.listStyle.CalcHeight(this.listContent[0], 1f);
	}

	protected void InitIndex()
	{
		for (int i = 0; i < this.listContent.Length; i++)
		{
			bool flag = this.buttonContent.text == this.listContent[i].text;
			if (flag)
			{
				this.selectedItemIndex = i;
				return;
			}
		}
		this.selectedItemIndex = -1;
	}

	public int SelectItem(string item)
	{
		string b = item.ToLower();
		for (int i = 0; i < this.listContent.Length; i++)
		{
			bool flag = this.listContent[i].text.ToLower() == b;
			if (flag)
			{
				this.selectedItemIndex = i;
				return i;
			}
		}
		return -1;
	}

	public bool IsClickedComboButton
	{
		get
		{
			return this.isClickedComboButton;
		}
	}

	public int ItemCount
	{
		get
		{
			return this.listContent.Length;
		}
	}

	public int SelectedItemIndex
	{
		get
		{
			return this.selectedItemIndex;
		}
		set
		{
			bool flag = this.selectedItemIndex != value;
			if (flag)
			{
				bool flag2 = value < this.listContent.Length && value >= 0;
				if (flag2)
				{
					this.selectedItemIndex = value;
					this.buttonContent = this.listContent[this.selectedItemIndex];
				}
				else
				{
					this.buttonContent = GUIContent.none;
					this.selectedItemIndex = -1;
				}
			}
		}
	}

	protected static bool forceToUnShow = false;

	protected static int useControlID = -1;

	protected bool isClickedComboButton = false;

	protected int selectedItemIndex = 0;

	protected int backSelectedItemIndex = 0;

	protected float itemWidth;

	protected float itemHeight;

	protected GUIContent buttonContent;

	protected GUIContent[] listContent;

	protected GUIStyle buttonStyle;

	protected GUIStyle boxStyle;

	protected GUIStyle listStyle;

	protected Vector2 scrollPosition = Vector2.zero;
}
