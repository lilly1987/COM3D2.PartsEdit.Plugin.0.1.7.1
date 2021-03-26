using System;
using System.Linq;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class GizmoSettingUI
	{
		public GizmoSettingUI()
		{
			this.keyCodeList = (KeyCode[])Enum.GetValues(typeof(KeyCode));
			this.smallIndex = Array.IndexOf<KeyCode>(this.keyCodeList, Setting.gizmoSmallKey);
			this.bigIndex = Array.IndexOf<KeyCode>(this.keyCodeList, Setting.gizmoBigKey);
			this.contentList = (from code in this.keyCodeList
			select new GUIContent(code.ToString())).ToArray<GUIContent>();
			this.smallCombo = new ComboBoxLO(this.contentList[this.smallIndex], this.contentList, UIParams.Instance.bStyle, UIParams.Instance.winStyle, UIParams.Instance.listStyle, false);
			this.bigCombo = new ComboBoxLO(this.contentList[this.bigIndex], this.contentList, UIParams.Instance.bStyle, UIParams.Instance.winStyle, UIParams.Instance.listStyle, false);
		}

		public void Draw()
		{
			GUILayout.Label("ギズモ表示設定", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			UIUtil.BeginIndentArea();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("移動量減少キー", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			int num = this.smallCombo.ShowScroll(GUILayout.ExpandHeight(false));
			bool flag = this.smallIndex != num;
			if (flag)
			{
				this.smallIndex = num;
				Setting.gizmoSmallKey = this.keyCodeList[this.smallIndex];
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("移動量増加キー", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			int num2 = this.bigCombo.ShowScroll(GUILayout.ExpandHeight(false));
			bool flag2 = this.bigIndex != num2;
			if (flag2)
			{
				this.bigIndex = num2;
				Setting.gizmoBigKey = this.keyCodeList[this.bigIndex];
			}
			GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			bool flag3 = GUILayout.Button("戻る", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag3)
			{
				Setting.mode = Mode.Edit;
				Setting.SaveIni();
			}
			GUILayout.EndHorizontal();
			UIUtil.EndoIndentArea();
		}

		private int smallIndex = 0;

		private int bigIndex = 0;

		private KeyCode[] keyCodeList = null;

		private GUIContent[] contentList = null;

		private ComboBoxLO smallCombo = null;

		private ComboBoxLO bigCombo = null;
	}
}
