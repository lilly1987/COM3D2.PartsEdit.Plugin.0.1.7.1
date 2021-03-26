using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

internal class UIWindow : MonoBehaviour
{
	public bool IsVisible
	{
		get
		{
			return this.isVisible;
		}
	}

	private void Start()
	{
		this.scaleButton = new ScaleButton[]
		{
			new ScaleButton(this, ScaleButton.LeftRight.Left, ScaleButton.UpperBottom.Upper)
		};
	}

	private void Update()
	{
		foreach (ScaleButton scaleButton in this.scaleButton)
		{
			scaleButton.Drag();
		}
		bool flag = this.isVisible && this.windowRect.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y));
		if (flag)
		{
			bool mouseInput = this.GetMouseInput();
			if (mouseInput)
			{
				Input.ResetInputAxes();
			}
		}
	}

	private void OnGUI()
	{
		bool flag = this.isVisible;
		if (flag)
		{
			this.windowRect = GUI.Window(this.windowID, this.windowRect, new GUI.WindowFunction(this.DoMyWindow), this.title, this.uiParams.winStyle);
			bool flag2 = this.windowRect.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y));
			if (flag2)
			{
				bool mouseInput = this.GetMouseInput();
				if (mouseInput)
				{
					Input.ResetInputAxes();
				}
			}
		}
	}

	public void SetWindowInfo(string windowName)
	{
		this.title = windowName;
		this.windowID = windowName.GetHashCode();
	}

	public void SetWindowInfo(string windowName, string additionalInfo)
	{
		this.title = windowName + " " + additionalInfo;
		this.windowID = windowName.GetHashCode();
	}

	public void SetWindowInfo(string windowName, int windowID)
	{
		this.title = windowName;
		this.windowID = windowID;
	}

	public void AddItem(IUIDrawer item)
	{
		this.drawerList.Add(item);
	}

	public void SetPosition(Vector2 position)
	{
		this.windowRect.position = position;
	}

	public void SetSize(Vector2 size)
	{
		this.windowRect.size = size;
	}

	public void SetVisible(bool visible)
	{
		bool flag = this.isVisible && !visible;
		if (flag)
		{
			this.endEvent.Invoke();
		}
		this.isVisible = visible;
	}

	public Rect GetRect()
	{
		return this.windowRect;
	}

	public Vector2 GetMinSize()
	{
		return this.minSize;
	}

	public void ExtendLeft(float x)
	{
		float num = this.windowRect.x - x;
		this.windowRect.x = x;
		this.windowRect.width = this.windowRect.width + num;
	}

	public void ExtendRight(float x)
	{
		this.windowRect.width = x - this.windowRect.x;
	}

	public void ExtendUpper(float y)
	{
		float num = this.windowRect.y - y;
		this.windowRect.y = y;
		this.windowRect.height = this.windowRect.height + num;
	}

	public void ExtendBottom(float y)
	{
		this.windowRect.height = y - this.windowRect.y;
	}

	public void AddDrawEvent(UnityAction call)
	{
		this.drawEvent.AddListener(call);
	}

	public void AddEndEvent(UnityAction call)
	{
		this.endEvent.AddListener(call);
	}

	private void DoMyWindow(int windowID)
	{
		this.DrawCloseButton();
		foreach (ScaleButton scaleButton in this.scaleButton)
		{
			scaleButton.Draw();
		}
		GUI.DragWindow(new Rect(20f, 0f, this.windowRect.width - 40f, 20f));
		GUILayout.Label("", new GUILayoutOption[0]);
		this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, new GUILayoutOption[0]);
		this.drawEvent.Invoke();
		GUILayout.EndScrollView();
	}

	private void DrawCloseButton()
	{
		Rect position = new Rect(this.windowRect.width - 20f, 0f, 20f, 20f);
		bool flag = GUI.Button(position, "×", this.uiParams.bStyle);
		if (flag)
		{
			this.SetVisible(false);
		}
	}

	private bool GetMouseInput()
	{
		return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) || Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2) || Input.GetAxis("Mouse ScrollWheel") != 0f;
	}

	private bool isVisible = false;

	private string title;

	private int windowID;

	private Rect windowRect = new Rect(20f, 20f, 400f, 600f);

	private Vector2 scrollPosition = Vector2.zero;

	private Vector2 minSize = new Vector2(100f, 100f);

	private ScaleButton[] scaleButton;

	private UIParams uiParams = UIParams.Instance;

	private UnityEvent drawEvent = new UnityEvent();

	private List<IUIDrawer> drawerList = new List<IUIDrawer>();

	private UnityEvent endEvent = new UnityEvent();
}
