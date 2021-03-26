using System;
using System.Linq;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class BoneDisplaySettingUI
	{
		public BoneDisplaySettingUI()
		{
			this.InitColorList();
			this.InitKeyList();
		}

		public void Draw()
		{
			GUILayout.Label("ボーン表示設定", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			UIUtil.BeginIndentArea();
			this.DrawNormalBoneColorSetting();
			this.DrawSelectBoneColorSetting();
			this.DrawSelectKeySetting();
			this.DrawBodyBoneDisplaySetting();
			GUILayout.FlexibleSpace();
			this.DrawReturnButton();
			UIUtil.EndoIndentArea();
		}

		private void DrawNormalBoneColorSetting()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("通常ボーン色", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			int num = this.normalColorCombo.ShowScroll(GUILayout.ExpandHeight(false));
			bool flag = this.normalColorIndex != num;
			if (flag)
			{
				this.normalColorIndex = num;
				Setting.normalBoneColor.SetValue(this.colorList[this.normalColorIndex]);
			}
			GUILayout.EndHorizontal();
		}

		private void DrawSelectBoneColorSetting()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("選択ボーン色", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			int num = this.selectColorCombo.ShowScroll(GUILayout.ExpandHeight(false));
			bool flag = this.selectColorIndex != num;
			if (flag)
			{
				this.selectColorIndex = num;
				Setting.selectBoneColor.SetValue(this.colorList[this.selectColorIndex]);
			}
			GUILayout.EndHorizontal();
		}

		private void DrawSelectKeySetting()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("選択キー", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			int num = this.keyCombo.ShowScroll(GUILayout.ExpandHeight(false));
			bool flag = this.keyIndex != num;
			if (flag)
			{
				this.keyIndex = num;
				Setting.boneSelectKey = this.keyCodeList[this.keyIndex];
			}
			GUILayout.EndHorizontal();
		}

		private void DrawBodyBoneDisplaySetting()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("メイド体ボーン表示", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			int num = GUILayout.Toolbar((int)Setting.bodyBoneDisplay.GetValue(), this.bodyBoneDsiplayContents, UIParams.Instance.tStyle, new GUILayoutOption[0]);
			bool flag = this.bodyBoneDisplayIndex != num;
			if (flag)
			{
				this.bodyBoneDisplayIndex = num;
				Setting.bodyBoneDisplay.SetValue((BodyBoneDisplay)num);
			}
			GUILayout.EndHorizontal();
		}

		private void DrawReturnButton()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			bool flag = GUILayout.Button("戻る", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag)
			{
				Setting.mode = Mode.Edit;
				Setting.SaveIni();
			}
			GUILayout.EndHorizontal();
		}

		private void InitKeyList()
		{
			this.keyCodeList = (KeyCode[])Enum.GetValues(typeof(KeyCode));
			this.keyIndex = Array.IndexOf<KeyCode>(this.keyCodeList, Setting.boneSelectKey);
			this.keyContentList = (from code in this.keyCodeList
			select new GUIContent(code.ToString())).ToArray<GUIContent>();
			this.keyCombo = new ComboBoxLO(this.keyContentList[this.keyIndex], this.keyContentList, UIParams.Instance.bStyle, UIParams.Instance.winStyle, UIParams.Instance.listStyle, false);
		}

		private void InitColorList()
		{
			string[] array = new string[]
			{
				"white",
				"black",
				"gray",
				"red",
				"green",
				"blue",
				"yellow",
				"cyan"
			};
			this.colorList = (from str in array
			select ColorUtil.GetColorFromName(str)).ToArray<Color>();
			this.normalColorIndex = Array.IndexOf<string>(array, ColorUtil.GetNameFromColor(Setting.normalBoneColor.GetValue()));
			this.selectColorIndex = Array.IndexOf<string>(array, ColorUtil.GetNameFromColor(Setting.selectBoneColor.GetValue()));
			this.colorContentList = (from str in array
			select new GUIContent(str)).ToArray<GUIContent>();
			this.normalColorCombo = new ComboBoxLO(this.colorContentList[this.normalColorIndex], this.colorContentList, UIParams.Instance.bStyle, UIParams.Instance.winStyle, UIParams.Instance.listStyle, false);
			this.selectColorCombo = new ComboBoxLO(this.colorContentList[this.selectColorIndex], this.colorContentList, UIParams.Instance.bStyle, UIParams.Instance.winStyle, UIParams.Instance.listStyle, false);
		}

		private int keyIndex = 0;

		private KeyCode[] keyCodeList = null;

		private GUIContent[] keyContentList = null;

		private ComboBoxLO keyCombo = null;

		private int normalColorIndex = 0;

		private int selectColorIndex = 0;

		private Color[] colorList = null;

		private GUIContent[] colorContentList = null;

		private ComboBoxLO normalColorCombo = null;

		private ComboBoxLO selectColorCombo = null;

		private int bodyBoneDisplayIndex = 0;

		private GUIContent[] bodyBoneDsiplayContents = new GUIContent[]
		{
			new GUIContent("非表示"),
			new GUIContent("表示")
		};
	}
}
