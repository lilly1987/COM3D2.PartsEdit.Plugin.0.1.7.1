using System;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class ExportUI
	{
		public void Draw()
		{
			bool flag = this.objectData == null;
			if (flag)
			{
				this.objectData = PresetManager.GetObjectDataFromObject();
			}
			this.DrawFileNameInput();
			GUILayout.FlexibleSpace();
			this.DrawBackButton();
		}

		private void DrawFileNameInput()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("ファイル名", UIParams.Instance.lStyle, new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			});
			this.fileName = GUILayout.TextField(this.fileName, UIParams.Instance.textStyle, new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(true)
			});
			bool flag = this.fileName == "";
			if (flag)
			{
				GUI.enabled = false;
			}
			bool flag2 = GUILayout.Button("Export", UIParams.Instance.bStyle, new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			});
			if (flag2)
			{
				PresetManager.SaveObjectData(this.fileName);
				Setting.mode = Mode.Edit;
			}
			GUI.enabled = true;
			GUILayout.EndHorizontal();
		}

		private void DrawObjectData()
		{
			bool flag = this.objectData.rootData != null;
			if (flag)
			{
				GUILayout.Label("RootObject", new GUILayoutOption[0]);
			}
		}

		private void DrawBackButton()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			bool flag = GUILayout.Button("戻る", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag)
			{
				Setting.mode = Mode.Edit;
				this.objectData = null;
			}
			GUILayout.EndHorizontal();
		}

		private string fileName = "";

		private ObjectData objectData = null;
	}
}
