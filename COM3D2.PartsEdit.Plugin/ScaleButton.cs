using System;
using UnityEngine;

internal class ScaleButton
{
	public ScaleButton(UIWindow parentWindow, ScaleButton.LeftRight horizon, ScaleButton.UpperBottom vertical)
	{
		this.parentWindow = parentWindow;
		this.horizon = horizon;
		this.vertical = vertical;
	}

	public void Drag()
	{
		bool flag = !this.isDrag;
		if (!flag)
		{
			Vector2 vector = Input.mousePosition;
			bool flag2 = this.horizon == ScaleButton.LeftRight.Left;
			if (flag2)
			{
				this.parentWindow.ExtendLeft(vector.x - ScaleButton.size / 2f);
				bool flag3 = this.parentWindow.GetMinSize().x > this.parentWindow.GetRect().width;
				if (flag3)
				{
					this.parentWindow.ExtendRight(this.parentWindow.GetRect().x + this.parentWindow.GetMinSize().x);
				}
			}
			else
			{
				this.parentWindow.ExtendRight(vector.x + ScaleButton.size / 2f);
				float num = this.parentWindow.GetMinSize().x - this.parentWindow.GetRect().width;
				bool flag4 = num > 0f;
				if (flag4)
				{
					this.parentWindow.ExtendLeft(this.parentWindow.GetRect().x - num);
				}
			}
			bool flag5 = this.vertical == ScaleButton.UpperBottom.Upper;
			if (flag5)
			{
				this.parentWindow.ExtendUpper((float)Screen.height - vector.y - ScaleButton.size / 2f);
				bool flag6 = this.parentWindow.GetMinSize().y > this.parentWindow.GetRect().height;
				if (flag6)
				{
					this.parentWindow.ExtendBottom(this.parentWindow.GetRect().y + this.parentWindow.GetMinSize().y);
				}
			}
			else
			{
				this.parentWindow.ExtendBottom((float)Screen.height - vector.y + ScaleButton.size / 2f);
				float num2 = this.parentWindow.GetMinSize().y - this.parentWindow.GetRect().height;
				bool flag7 = num2 > 0f;
				if (flag7)
				{
					this.parentWindow.ExtendUpper(this.parentWindow.GetRect().y - num2);
				}
			}
		}
	}

	public void Draw()
	{
		Rect rect = this.parentWindow.GetRect();
		Rect position = new Rect(0f, 0f, ScaleButton.size, ScaleButton.size);
		bool flag = this.horizon == ScaleButton.LeftRight.Right;
		if (flag)
		{
			position.x = rect.width - ScaleButton.size;
		}
		bool flag2 = this.vertical == ScaleButton.UpperBottom.Bottom;
		if (flag2)
		{
			position.y = rect.height - ScaleButton.size;
		}
		bool flag3 = GUI.RepeatButton(position, "□");
		bool flag4 = !this.isDrag && flag3;
		if (flag4)
		{
			this.isDrag = true;
		}
		bool flag5 = Event.current.type == EventType.Repaint && this.isDrag && !flag3;
		if (flag5)
		{
			this.isDrag = false;
		}
	}

	private static float size = 20f;

	private bool isDrag = false;

	private ScaleButton.LeftRight horizon;

	private ScaleButton.UpperBottom vertical;

	private UIWindow parentWindow;

	public enum LeftRight
	{
		Left,
		Right
	}

	public enum UpperBottom
	{
		Upper,
		Bottom
	}
}
