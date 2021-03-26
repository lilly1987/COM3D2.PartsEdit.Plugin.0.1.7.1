using System;
using GearMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class GearWindowSystem : MonoBehaviour
{
	private void Awake()
	{
		this.uiWindow = base.gameObject.AddComponent<UIWindow>();
	}

	private void OnLevelWasLoaded(int level)
	{
		bool flag = this.registered;
		if (flag)
		{
			bool flag2 = this.sceneChangeHide;
			if (flag2)
			{
				this.SetVisible(false);
			}
		}
		else
		{
			bool flag3 = SceneManager.GetActiveScene().name == "SceneTitle";
			if (flag3)
			{
				string label = PluginInfo.Name + " " + PluginInfo.Version;
				this.iconGO = Buttons.Add(PluginInfo.Name, label, this.scIcon.GetData(), new Action<GameObject>(this.ClickGearButton));
				Buttons.SetFrameColor(this.iconGO, Color.black);
				this.registered = true;
			}
		}
	}

	private void Update()
	{
		bool flag = this.visible != this.uiWindow.IsVisible;
		if (flag)
		{
			this.visible = this.uiWindow.IsVisible;
			bool flag2 = this.visible;
			if (flag2)
			{
				Buttons.SetFrameColor(this.iconGO, Color.red);
			}
			else
			{
				Buttons.SetFrameColor(this.iconGO, Color.black);
			}
		}
	}

	public UIWindow GetUIWindow()
	{
		return this.uiWindow;
	}

	public void SetVisible(bool fVisible)
	{
		bool flag = this.visible == fVisible;
		if (!flag)
		{
			this.visible = fVisible;
			bool flag2 = this.visible;
			if (flag2)
			{
				Buttons.SetFrameColor(this.iconGO, Color.red);
			}
			else
			{
				Buttons.SetFrameColor(this.iconGO, Color.black);
			}
			this.uiWindow.SetVisible(this.visible);
		}
	}

	private void ClickGearButton(GameObject goButton)
	{
		this.SetVisible(!this.uiWindow.IsVisible);
	}

	private bool sceneChangeHide = true;

	private bool visible = false;

	private GameObject iconGO;

	private PngData scIcon = new PngData("GearIcon.png");

	private bool registered = false;

	private UIWindow uiWindow;
}
