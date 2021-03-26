using System;
using System.Diagnostics;
using ExIni;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal static class Setting
	{
		public static void LoadIni()
		{
			Setting.LoadSetting();
			Setting.LoadBoneDisplay();
			Setting.LoadGizmoDisplay();
		}

		private static void LoadSetting()
		{
			Setting.boneDisplay = (BoneDisplay)IniUtil.GetIntValue("Setting", "BoneDisplay", 1);
			Setting.coordinateType = (ExGizmoRenderer.COORDINATE)IniUtil.GetIntValue("Setting", "CoordinateType", 0);
			Setting.gizmoType = (GizmoType)IniUtil.GetIntValue("Setting", "GizmoType", 2);
		}

		private static void LoadBoneDisplay()
		{
			Setting.boneSelectKey = (KeyCode)IniUtil.GetIntValue("BoneDisplay", "BoneSelectKey", 308);
			Setting.normalBoneColor = new Setting.SettingValue<Color>(ColorUtil.GetColorFromName(IniUtil.GetStringValue("BoneDisplay", "NormalBoneColor", "white")));
			Setting.selectBoneColor = new Setting.SettingValue<Color>(ColorUtil.GetColorFromName(IniUtil.GetStringValue("BoneDisplay", "SelectBoneColor", "red")));
			Setting.bodyBoneDisplay = new Setting.SettingValue<BodyBoneDisplay>((BodyBoneDisplay)IniUtil.GetIntValue("BoneDisplay", "BodyBoneDisplay", 1));
		}

		private static void LoadGizmoDisplay()
		{
			Setting.gizmoSmallKey = (KeyCode)IniUtil.GetIntValue("GizmoDisplay", "GizmoSmallKey", 304);
			Setting.gizmoBigKey = (KeyCode)IniUtil.GetIntValue("GizmoDisplay", "GizmoBigKey", 0);
		}

		public static void SaveIni()
		{
			Setting.SaveSetting();
			Setting.SaveBoneDisplay();
			Setting.SaveGizmoDisplay();
			IniUtil.Save();
		}

		private static void SaveSetting()
		{
			IniKey iniKey = IniUtil.preferences["Setting"]["BoneDisplay"];
			int num = (int)Setting.boneDisplay;
			iniKey.Value = num.ToString();
			IniKey iniKey2 = IniUtil.preferences["Setting"]["CoordinateType"];
			num = (int)Setting.coordinateType;
			iniKey2.Value = num.ToString();
			IniKey iniKey3 = IniUtil.preferences["Setting"]["GizmoType"];
			num = (int)Setting.gizmoType;
			iniKey3.Value = num.ToString();
		}

		private static void SaveBoneDisplay()
		{
			IniKey iniKey = IniUtil.preferences["BoneDisplay"]["BoneSelectKey"];
			int num = (int)Setting.boneSelectKey;
			iniKey.Value = num.ToString();
			IniUtil.preferences["BoneDisplay"]["NormalBoneColor"].Value = ColorUtil.GetNameFromColor(Setting.normalBoneColor.GetValue());
			IniUtil.preferences["BoneDisplay"]["SelectBoneColor"].Value = ColorUtil.GetNameFromColor(Setting.selectBoneColor.GetValue());
			IniUtil.preferences["BoneDisplay"]["BodyBoneDisplay"].Value = ((int)Setting.bodyBoneDisplay.GetValue()).ToString();
		}

		private static void SaveGizmoDisplay()
		{
			IniKey iniKey = IniUtil.preferences["GizmoDisplay"]["GizmoSmallKey"];
			int num = (int)Setting.gizmoSmallKey;
			iniKey.Value = num.ToString();
			IniKey iniKey2 = IniUtil.preferences["GizmoDisplay"]["GizmoBigKey"];
			num = (int)Setting.gizmoBigKey;
			iniKey2.Value = num.ToString();
		}

		public static Mode mode = Mode.Edit;

		public static BoneDisplay boneDisplay = BoneDisplay.Visible;

		public static ExGizmoRenderer.COORDINATE coordinateType = ExGizmoRenderer.COORDINATE.Local;

		public static GizmoType gizmoType = GizmoType.Rotation;

		public static int targetSelectMode = 0;

		public static KeyCode boneSelectKey = KeyCode.LeftAlt;

		public static KeyCode gizmoSmallKey = KeyCode.LeftShift;

		public static KeyCode gizmoBigKey = KeyCode.None;

		public static Setting.SettingValue<Color> normalBoneColor = new Setting.SettingValue<Color>(Color.white);

		public static Setting.SettingValue<Color> selectBoneColor = new Setting.SettingValue<Color>(Color.red);

		public static Setting.SettingValue<BodyBoneDisplay> bodyBoneDisplay = new Setting.SettingValue<BodyBoneDisplay>(BodyBoneDisplay.Visible);

		public class SettingValue<T>
		{
			//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			public event Setting.SettingValue<T>.Delegate mDelegate = delegate(T ret)
			{
			};

			public SettingValue(T initValue)
			{
				this.value = initValue;
			}

			public T GetValue()
			{
				return this.value;
			}

			public void SetValue(T fValue)
			{
				this.value = fValue;
				this.mDelegate(this.value);
			}

			private T value;

			public delegate void Delegate(T ret);
		}
	}
}
