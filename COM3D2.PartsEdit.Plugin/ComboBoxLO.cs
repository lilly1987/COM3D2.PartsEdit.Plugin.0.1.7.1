using System;
using UnityEngine;

internal class ComboBoxLO : ComboBoxBase
{
	public ComboBoxLO(GUIContent buttonContent, GUIContent[] listContent, GUIStyle listStyle) : base(buttonContent, listContent, listStyle)
	{
	}

	public ComboBoxLO(GUIContent buttonContent, GUIContent[] listContent, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle, bool labelFixed) : base(buttonContent, listContent, buttonStyle, boxStyle, listStyle)
	{
		this.labelFixed = labelFixed;
	}

	public void SetItemWidth(float itemWidth)
	{
		this.itemWidth = itemWidth;
	}

	public int Show(GUILayoutOption buttonOpt)
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
		bool isClickedComboButton = this.isClickedComboButton;
		bool flag2 = isClickedComboButton;
		if (flag2)
		{
			GUILayout.BeginVertical(this.boxStyle, new GUILayoutOption[]
			{
				GUILayout.Width(this.itemWidth)
			});
		}
		try
		{
			bool flag3 = GUILayout.Button(this.buttonContent, this.buttonStyle, new GUILayoutOption[]
			{
				buttonOpt,
				GUILayout.Width(this.itemWidth)
			});
			if (flag3)
			{
				bool flag4 = ComboBoxBase.useControlID == -1;
				if (flag4)
				{
					ComboBoxBase.useControlID = controlID;
					this.isClickedComboButton = false;
				}
				bool flag5 = ComboBoxBase.useControlID != controlID;
				if (flag5)
				{
					ComboBoxBase.forceToUnShow = true;
					ComboBoxBase.useControlID = controlID;
				}
				this.isClickedComboButton = true;
			}
			bool isClickedComboButton2 = this.isClickedComboButton;
			if (isClickedComboButton2)
			{
				float height = this.itemHeight * (float)this.listContent.Length;
				int num = GUILayout.SelectionGrid(this.selectedItemIndex, this.listContent, 1, this.listStyle, new GUILayoutOption[]
				{
					GUILayout.Width(this.itemWidth),
					GUILayout.Height(height)
				});
				bool flag6 = num != this.selectedItemIndex;
				if (flag6)
				{
					bool flag7 = !this.labelFixed;
					if (flag7)
					{
						base.SelectedItemIndex = num;
					}
					else
					{
						this.selectedItemIndex = num;
					}
				}
			}
		}
		finally
		{
			bool flag8 = isClickedComboButton;
			if (flag8)
			{
				GUILayout.EndVertical();
			}
		}
		this.isClickedComboButton &= !flag;
		return this.selectedItemIndex;
	}

	public int ShowScroll(GUILayoutOption buttonOpt)
	{
		bool isClickedComboButton = this.isClickedComboButton;
		bool flag = isClickedComboButton;
		if (flag)
		{
			this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, new GUILayoutOption[0]);
			GUILayout.BeginVertical(this.boxStyle, new GUILayoutOption[]
			{
				GUILayout.Width(this.itemWidth)
			});
		}
		try
		{
			bool flag2 = GUILayout.Button(this.buttonContent, this.buttonStyle, new GUILayoutOption[]
			{
				buttonOpt
			});
			if (flag2)
			{
				bool isClickedComboButton2 = base.IsClickedComboButton;
				if (isClickedComboButton2)
				{
					this.isClickedComboButton = false;
					base.SelectedItemIndex = this.backSelectedItemIndex;
				}
				else
				{
					this.isClickedComboButton = true;
					this.backSelectedItemIndex = this.selectedItemIndex;
					this.selectedItemIndex = -1;
				}
			}
			bool isClickedComboButton3 = this.isClickedComboButton;
			if (isClickedComboButton3)
			{
				float height = this.itemHeight * (float)this.listContent.Length;
				int num = GUILayout.SelectionGrid(this.selectedItemIndex, this.listContent, 1, this.listStyle, new GUILayoutOption[]
				{
					GUILayout.Height(height)
				});
				bool flag3 = num != this.selectedItemIndex;
				if (flag3)
				{
					base.SelectedItemIndex = num;
					this.isClickedComboButton = false;
				}
			}
		}
		finally
		{
			bool flag4 = isClickedComboButton;
			if (flag4)
			{
				GUILayout.EndVertical();
				GUILayout.EndScrollView();
			}
		}
		bool isClickedComboButton4 = base.IsClickedComboButton;
		int result;
		if (isClickedComboButton4)
		{
			result = this.backSelectedItemIndex;
		}
		else
		{
			result = this.selectedItemIndex;
		}
		return result;
	}

	private readonly bool labelFixed;
}
