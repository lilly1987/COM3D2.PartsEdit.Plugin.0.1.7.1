using System;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class SettingUI
	{
		public void Draw()
		{
			GUILayout.Label("設定", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			UIUtil.BeginIndentArea();
			this.DrawBoneDisplay();
			this.DrawCoordinateType();
			this.DrawGizmoType();
			UIUtil.EndoIndentArea();
		}

		private void DrawBoneDisplay()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("ボーン表示", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			Setting.boneDisplay = (BoneDisplay)GUILayout.Toolbar((int)Setting.boneDisplay, this.boneDsiplayContents, UIParams.Instance.tStyle, new GUILayoutOption[0]);
			bool flag = GUILayout.Button("詳細", new GUILayoutOption[0]);
			if (flag)
			{
				Setting.mode = Mode.BoneDisplaySetting;
			}
			GUILayout.EndHorizontal();
		}

		private void DrawCoordinateType()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			bool flag = Setting.gizmoType == GizmoType.Scale;
			if (flag)
			{
				GUI.enabled = false;
			}
			GUILayout.Label("座標タイプ", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			Setting.coordinateType = (ExGizmoRenderer.COORDINATE)GUILayout.Toolbar((int)Setting.coordinateType, this.coordinateTypeContents, UIParams.Instance.tStyle, new GUILayoutOption[0]);
			GUI.enabled = true;
			GUILayout.EndHorizontal();
		}

		private void DrawGizmoType()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("ギズモタイプ", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			bool flag = Setting.coordinateType == ExGizmoRenderer.COORDINATE.Local;
			if (flag)
			{
				Setting.gizmoType = (GizmoType)GUILayout.Toolbar((int)Setting.gizmoType, this.gizmoTypeLocalContents, UIParams.Instance.tStyle, new GUILayoutOption[0]);
			}
			else
			{
				Setting.gizmoType = (GizmoType)GUILayout.Toolbar((int)Setting.gizmoType, this.gizmoTypeContents, UIParams.Instance.tStyle, new GUILayoutOption[0]);
			}
			bool flag2 = GUILayout.Button("詳細", new GUILayoutOption[0]);
			if (flag2)
			{
				Setting.mode = Mode.GizmoSetting;
			}
			GUILayout.EndHorizontal();
		}

		private GUIContent[] boneDsiplayContents = new GUIContent[]
		{
			new GUIContent("非表示"),
			new GUIContent("表示")
		};

		private GUIContent[] coordinateTypeContents = new GUIContent[]
		{
			new GUIContent("Local"),
			new GUIContent("Global"),
			new GUIContent("View")
		};

		private GUIContent[] gizmoTypeContents = new GUIContent[]
		{
			new GUIContent("無し"),
			new GUIContent("位置"),
			new GUIContent("回転")
		};

		private GUIContent[] gizmoTypeLocalContents = new GUIContent[]
		{
			new GUIContent("無し"),
			new GUIContent("位置"),
			new GUIContent("回転"),
			new GUIContent("拡縮")
		};
	}
}
