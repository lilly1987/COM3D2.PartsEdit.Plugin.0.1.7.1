using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityInjector;
using UnityInjector.Attributes;

namespace GearMenu
{
	public static class Buttons
	{
		public static string Name
		{
			get
			{
				return Buttons.Name_;
			}
		}

		public static string Version
		{
			get
			{
				return Buttons.Version_;
			}
		}

		public static GameObject Add(PluginBase plugin, byte[] pngData, Action<GameObject> action)
		{
			return Buttons.Add(null, plugin, pngData, action);
		}

		public static GameObject Add(string name, PluginBase plugin, byte[] pngData, Action<GameObject> action)
		{
			PluginNameAttribute pluginNameAttribute = Attribute.GetCustomAttribute(plugin.GetType(), typeof(PluginNameAttribute)) as PluginNameAttribute;
			PluginVersionAttribute pluginVersionAttribute = Attribute.GetCustomAttribute(plugin.GetType(), typeof(PluginVersionAttribute)) as PluginVersionAttribute;
			string arg = (pluginNameAttribute == null) ? plugin.Name : pluginNameAttribute.Name;
			string arg2 = (pluginVersionAttribute == null) ? string.Empty : pluginVersionAttribute.Version;
			string label = string.Format("{0} {1}", arg, arg2);
			return Buttons.Add(name, label, pngData, action);
		}

		public static GameObject Add(string label, byte[] pngData, Action<GameObject> action)
		{
			return Buttons.Add(null, label, pngData, action);
		}

		public static GameObject Add(string name, string label, byte[] pngData, Action<GameObject> action)
		{
			GameObject goButton = null;
			bool flag = Buttons.Contains(name);
			if (flag)
			{
				Buttons.Remove(name);
			}
			bool flag2 = action == null;
			GameObject goButton2;
			if (flag2)
			{
				goButton2 = goButton;
			}
			else
			{
				try
				{
					goButton = NGUITools.AddChild(Buttons.Grid, UTY.GetChildObject(Buttons.Grid, "Config", true));
					bool flag3 = name != null;
					if (flag3)
					{
						goButton.name = name;
					}
					EventDelegate.Set(goButton.GetComponent<UIButton>().onClick, delegate()
					{
						action(goButton);
					});
					UIEventTrigger component = goButton.GetComponent<UIEventTrigger>();
					EventDelegate.Add(component.onHoverOut, delegate()
					{
						Buttons.SysShortcut.VisibleExplanation(null, false);
					});
					EventDelegate.Add(component.onDragStart, delegate()
					{
						Buttons.SysShortcut.VisibleExplanation(null, false);
					});
					Buttons.SetText(goButton, label);
					bool flag4 = pngData == null;
					if (flag4)
					{
						pngData = DefaultIcon.Png;
					}
					UISprite component2 = goButton.GetComponent<UISprite>();
					component2.type = UIBasicSprite.Type.Filled;
					component2.fillAmount = 0f;
					Texture2D texture2D = new Texture2D(1, 1);
					texture2D.LoadImage(pngData);
					UITexture uitexture = NGUITools.AddWidget<UITexture>(goButton);
					uitexture.material = new Material(uitexture.shader);
					uitexture.material.mainTexture = texture2D;
					uitexture.MakePixelPerfect();
					Buttons.Reposition();
				}
				catch
				{
					bool flag5 = goButton != null;
					if (flag5)
					{
						NGUITools.Destroy(goButton);
						goButton = null;
					}
					throw;
				}
				goButton2 = goButton;
			}
			return goButton2;
		}

		public static void Remove(string name)
		{
			Buttons.Remove(Buttons.Find(name));
		}

		public static void Remove(GameObject go)
		{
			NGUITools.Destroy(go);
			Buttons.Reposition();
		}

		public static bool Contains(string name)
		{
			return Buttons.Find(name) != null;
		}

		public static bool Contains(GameObject go)
		{
			return Buttons.Contains(go.name);
		}

		public static void SetFrameColor(string name, Color color)
		{
			Buttons.SetFrameColor(Buttons.Find(name), color);
		}

		public static void SetFrameColor(GameObject go, Color color)
		{
			UITexture componentInChildren = go.GetComponentInChildren<UITexture>();
			bool flag = componentInChildren == null;
			if (!flag)
			{
				Texture2D texture2D = componentInChildren.mainTexture as Texture2D;
				bool flag2 = texture2D == null;
				if (!flag2)
				{
					for (int i = 1; i < texture2D.width - 1; i++)
					{
						texture2D.SetPixel(i, 0, color);
						texture2D.SetPixel(i, texture2D.height - 1, color);
					}
					for (int j = 1; j < texture2D.height - 1; j++)
					{
						texture2D.SetPixel(0, j, color);
						texture2D.SetPixel(texture2D.width - 1, j, color);
					}
					texture2D.Apply();
				}
			}
		}

		public static void ResetFrameColor(string name)
		{
			Buttons.ResetFrameColor(Buttons.Find(name));
		}

		public static void ResetFrameColor(GameObject go)
		{
			Buttons.SetFrameColor(go, Buttons.DefaultFrameColor);
		}

		public static void SetText(string name, string label)
		{
			Buttons.SetText(Buttons.Find(name), label);
		}

		public static void SetText(GameObject go, string label)
		{
			UIEventTrigger component = go.GetComponent<UIEventTrigger>();
			component.onHoverOver.Clear();
			EventDelegate.Add(component.onHoverOver, delegate()
			{
				Buttons.SysShortcut.VisibleExplanation(label, label != null);
			});
			UIButton component2 = go.GetComponent<UIButton>();
			bool flag = component2.state == UIButtonColor.State.Hover;
			if (flag)
			{
				Buttons.SysShortcut.VisibleExplanation(label, label != null);
			}
		}

		private static GameObject Find(string name)
		{
			Transform transform = Buttons.GridUI.GetChildList().FirstOrDefault((Transform c) => c.gameObject.name == name);
			return (transform == null) ? null : transform.gameObject;
		}

		private static void Reposition()
		{
			Buttons.SetAndCallOnReposition(Buttons.GridUI);
			Buttons.GridUI.repositionNow = true;
		}

		private static void SetAndCallOnReposition(UIGrid uiGrid)
		{
			string onRepositionVersion = Buttons.GetOnRepositionVersion(uiGrid);
			bool flag = onRepositionVersion == null;
			if (!flag)
			{
				bool flag2 = onRepositionVersion == string.Empty || string.Compare(onRepositionVersion, Buttons.Version, false) < 0;
				if (flag2)
				{
					uiGrid.onReposition = new UIGrid.OnReposition(new Buttons.OnRepositionHandler(Buttons.Version).OnReposition);
				}
				bool flag3 = uiGrid.onReposition != null;
				if (flag3)
				{
					object target = uiGrid.onReposition.Target;
					bool flag4 = target != null;
					if (flag4)
					{
						Type type = target.GetType();
						MethodInfo method = type.GetMethod("PreOnReposition");
						bool flag5 = method != null;
						if (flag5)
						{
							method.Invoke(target, new object[0]);
						}
					}
				}
			}
		}

		private static string GetOnRepositionVersion(UIGrid uiGrid)
		{
			bool flag = uiGrid.onReposition == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				object target = uiGrid.onReposition.Target;
				bool flag2 = target == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					Type type = target.GetType();
					bool flag3 = type == null;
					if (flag3)
					{
						result = null;
					}
					else
					{
						FieldInfo field = type.GetField("Version", BindingFlags.Instance | BindingFlags.Public);
						bool flag4 = field == null;
						if (flag4)
						{
							result = null;
						}
						else
						{
							string text = field.GetValue(target) as string;
							bool flag5 = text == null || !text.StartsWith(Buttons.Name);
							if (flag5)
							{
								result = null;
							}
							else
							{
								result = text;
							}
						}
					}
				}
			}
			return result;
		}

		public static SystemShortcut SysShortcut
		{
			get
			{
				return GameMain.Instance.SysShortcut;
			}
		}

		public static UIPanel SysShortcutPanel
		{
			get
			{
				return Buttons.SysShortcut.GetComponent<UIPanel>();
			}
		}

		public static UISprite SysShortcutExplanation
		{
			get
			{
				Type typeFromHandle = typeof(SystemShortcut);
				FieldInfo field = typeFromHandle.GetField("m_spriteExplanation", BindingFlags.Instance | BindingFlags.NonPublic);
				bool flag = field == null;
				UISprite result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = (field.GetValue(Buttons.SysShortcut) as UISprite);
				}
				return result;
			}
		}

		public static GameObject Base
		{
			get
			{
				return Buttons.SysShortcut.gameObject.transform.Find("Base").gameObject;
			}
		}

		public static UISprite BaseSprite
		{
			get
			{
				return Buttons.Base.GetComponent<UISprite>();
			}
		}

		public static GameObject Grid
		{
			get
			{
				return Buttons.Base.gameObject.transform.Find("Grid").gameObject;
			}
		}

		public static UIGrid GridUI
		{
			get
			{
				return Buttons.Grid.GetComponent<UIGrid>();
			}
		}

		private static string Name_ = "CM3D2.GearMenu.Buttons";

		private static string Version_ = Buttons.Name_ + " 0.0.2.0";

		public static readonly Color DefaultFrameColor = new Color(1f, 1f, 1f, 0f);

		private class OnRepositionHandler
		{
			public OnRepositionHandler(string version)
			{
				this.Version = version;
			}

			public void OnReposition()
			{
			}

			public void PreOnReposition()
			{
				UIGrid gridUI = Buttons.GridUI;
				UISprite baseSprite = Buttons.BaseSprite;
				float num = 0.75f;
				float pixelSizeAdjustment = UIRoot.GetPixelSizeAdjustment(Buttons.Base);
				gridUI.cellHeight = gridUI.cellWidth;
				gridUI.arrangement = UIGrid.Arrangement.CellSnap;
				gridUI.sorting = UIGrid.Sorting.None;
				gridUI.pivot = UIWidget.Pivot.TopRight;
				gridUI.maxPerLine = (int)((float)Screen.width / (gridUI.cellWidth / pixelSizeAdjustment) * num);
				List<Transform> childList = gridUI.GetChildList();
				int count = childList.Count;
				int num2 = Math.Min(gridUI.maxPerLine, count);
				int num3 = Math.Max(1, (count - 1) / gridUI.maxPerLine + 1);
				int num4 = (int)(gridUI.cellWidth * 3f / 2f + 8f);
				int num5 = (int)(gridUI.cellHeight / 2f);
				float num6 = (float)num5 * 1.5f + 1f;
				baseSprite.pivot = UIWidget.Pivot.TopRight;
				baseSprite.width = (int)((float)num4 + gridUI.cellWidth * (float)num2);
				baseSprite.height = (int)((float)num5 + gridUI.cellHeight * (float)num3 + 2f);
				Buttons.Base.transform.localPosition = new Vector3(946f, 502f + num6, 0f);
				Buttons.Grid.transform.localPosition = new Vector3(-2f + (float)(-(float)num2 - 1 + num3 - 1) * gridUI.cellWidth, -1f - num6, 0f);
				int num7 = 0;
				string[] array = GameMain.Instance.CMSystem.NetUse ? Buttons.OnRepositionHandler.OnlineButtonNames : Buttons.OnRepositionHandler.OfflineButtonNames;
				foreach (Transform transform in childList)
				{
					int num8 = num7++;
					int num9 = Array.IndexOf<string>(array, transform.gameObject.name);
					bool flag = num9 >= 0;
					if (flag)
					{
						num8 = num9;
					}
					float x = (float)(-(float)num8 % gridUI.maxPerLine + num2 - 1) * gridUI.cellWidth;
					float num10 = (float)(num8 / gridUI.maxPerLine) * gridUI.cellHeight;
					transform.localPosition = new Vector3(x, -num10, 0f);
				}
				UISprite sysShortcutExplanation = Buttons.SysShortcutExplanation;
				Vector3 localPosition = sysShortcutExplanation.gameObject.transform.localPosition;
				localPosition.y = Buttons.Base.transform.localPosition.y - (float)baseSprite.height - (float)sysShortcutExplanation.height;
				sysShortcutExplanation.gameObject.transform.localPosition = localPosition;
			}

			public string Version;

			private static string[] OnlineButtonNames = new string[]
			{
				"Config",
				"Ss",
				"SsUi",
				"Shop",
				"ToTitle",
				"Info",
				"Exit"
			};

			private static string[] OfflineButtonNames = new string[]
			{
				"Config",
				"Ss",
				"SsUi",
				"ToTitle",
				"Info",
				"Exit"
			};
		}
	}
}
