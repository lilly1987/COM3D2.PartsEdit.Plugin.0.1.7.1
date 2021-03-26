using System;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class FinishUI
	{
		public FinishUI(UIWindow fWindow)
		{
			this.window = fWindow;
		}

		public void Draw()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			bool flag = BackUpData.maidDataDic.Count == 0 && BackUpData.objectDataDic.Count == 0;
			if (flag)
			{
				GUI.enabled = false;
			}
			bool flag2 = GUILayout.Button("リセット", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag2)
			{
				BackUpData.Restore();
			}
			GUI.enabled = true;
			GUILayout.FlexibleSpace();
			bool flag3 = GUILayout.Button("終了", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag3)
			{
				CommonUIData.maid = null;
				CommonUIData.slotNo = -2;
				CommonUIData.obj = null;
				CommonUIData.bone = null;
				this.window.SetVisible(false);
			}
			GUILayout.EndHorizontal();
		}

		private UIWindow window = null;
	}
}
