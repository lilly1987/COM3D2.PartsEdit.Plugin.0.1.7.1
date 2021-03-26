using System;
using UnityEngine;

internal class ComboBox : ComboBoxBase
{
	public ComboBox(Rect rect, GUIContent buttonContent, GUIContent[] listContent, GUIStyle listStyle) : base(buttonContent, listContent, listStyle)
	{
		this.rect = rect;
	}

	public ComboBox(Rect rect, GUIContent buttonContent, GUIContent[] listContent, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle) : base(buttonContent, listContent, buttonStyle, boxStyle, listStyle)
	{
		this.rect = rect;
	}

	public int Show()
	{
		bool forceToUnShow = ComboBoxBase.forceToUnShow;
		if (forceToUnShow)
		{
			ComboBoxBase.forceToUnShow = false;
			this.isClickedComboButton = false;
		}
		bool flag = false;
		int controlID = GUIUtility.GetControlID(FocusType.Passive);
		EventType typeForControl = Event.current.GetTypeForControl(controlID);
		if (typeForControl == EventType.MouseUp)
		{
			flag |= this.isClickedComboButton;
		}
		bool flag2 = GUI.Button(this.rect, this.buttonContent, this.buttonStyle);
		if (flag2)
		{
			bool flag3 = ComboBoxBase.useControlID == -1;
			if (flag3)
			{
				ComboBoxBase.useControlID = controlID;
				this.isClickedComboButton = false;
			}
			bool flag4 = ComboBoxBase.useControlID != controlID;
			if (flag4)
			{
				ComboBoxBase.forceToUnShow = true;
				ComboBoxBase.useControlID = controlID;
			}
			this.isClickedComboButton = true;
		}
		bool isClickedComboButton = this.isClickedComboButton;
		if (isClickedComboButton)
		{
			Rect position = new Rect(this.rect.x, this.rect.y + this.itemHeight, this.rect.width, this.itemHeight * (float)this.listContent.Length);
			GUI.Box(position, string.Empty, this.boxStyle);
			int num = GUI.SelectionGrid(position, this.selectedItemIndex, this.listContent, 1, this.listStyle);
			bool flag5 = num != this.selectedItemIndex;
			if (flag5)
			{
				base.SelectedItemIndex = num;
			}
		}
		this.isClickedComboButton &= !flag;
		return this.selectedItemIndex;
	}

	public Rect rect;
}
