using System;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class TargetSelectModeUI
	{
		public void Draw()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("オブジェクト選択モード", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			Setting.targetSelectMode = GUILayout.Toolbar(Setting.targetSelectMode, this.targetSelectModeContents, UIParams.Instance.tStyle, new GUILayoutOption[0]);
			GUILayout.EndHorizontal();
		}

		private GUIContent[] targetSelectModeContents = new GUIContent[]
		{
			new GUIContent("メイドパーツ"),
			new GUIContent("複数メイド")
		};
	}
}
