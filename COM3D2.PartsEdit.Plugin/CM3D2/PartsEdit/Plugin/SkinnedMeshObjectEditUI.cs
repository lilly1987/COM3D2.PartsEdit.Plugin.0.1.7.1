using System;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class SkinnedMeshObjectEditUI
	{
		public void Draw()
		{
			this.DrawObject();
		}

		private void DrawObject()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			bool flag = CommonUIData.obj;
			if (flag)
			{
				string name = CommonUIData.obj.name;
			}
			GUILayout.Label("オブジェクト:", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			this.objectSelectUI.DrawCombo();
			bool flag2 = !CommonUIData.obj || !BackUpData.GetObjectDataExist(CommonUIData.obj);
			if (flag2)
			{
				GUI.enabled = false;
			}
			bool flag3 = GUILayout.Button("R", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag3)
			{
				BackUpData.RestoreObject(CommonUIData.obj);
				BackUpData.Refresh();
			}
			GUI.enabled = true;
			GUILayout.EndHorizontal();
			UIUtil.BeginIndentArea();
			this.DrawImportExport();
			UIUtil.EndoIndentArea();
			UIUtil.BeginIndentArea();
			this.DrawBone();
			UIUtil.EndoIndentArea();
		}

		private void DrawImportExport()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("プリセット", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			bool flag = !CommonUIData.obj;
			if (flag)
			{
				GUI.enabled = false;
			}
			bool flag2 = GUILayout.Button("Import", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag2)
			{
				Setting.mode = Mode.Import;
			}
			bool flag3 = GUILayout.Button("Export", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag3)
			{
				Setting.mode = Mode.Export;
			}
			GUI.enabled = true;
			GUILayout.EndHorizontal();
		}

		private void DrawBone()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			string str = "未選択";
			bool flag = CommonUIData.bone;
			if (flag)
			{
				str = CommonUIData.bone.name;
			}
			GUILayout.Label("ボーン:" + str, UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			bool flag2 = !CommonUIData.bone || !CommonUIData.obj || !BackUpData.GetBoneDataExist(CommonUIData.obj, CommonUIData.bone);
			if (flag2)
			{
				GUI.enabled = false;
			}
			bool flag3 = GUILayout.Button("R", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag3)
			{
				BackUpData.RestoreBone(CommonUIData.obj, CommonUIData.bone);
				BackUpData.Refresh();
			}
			GUI.enabled = true;
			GUILayout.EndHorizontal();
			UIUtil.BeginIndentArea();
			this.DrawTransform();
			UIUtil.EndoIndentArea();
		}

		private void DrawTransform()
		{
			BackUpBoneData backUpBoneData = null;
			bool flag = CommonUIData.bone;
			if (flag)
			{
				backUpBoneData = BackUpData.GetOrNullBoneData(CommonUIData.obj, CommonUIData.bone);
			}
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("位置", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			bool flag2 = backUpBoneData == null || !backUpBoneData.changedPos;
			if (flag2)
			{
				GUI.enabled = false;
			}
			bool flag3 = GUILayout.Button("R", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag3)
			{
				BackUpData.RestorePosition(CommonUIData.obj, CommonUIData.bone);
				BackUpData.Refresh();
			}
			GUI.enabled = true;
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("回転", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			bool flag4 = backUpBoneData == null || !backUpBoneData.changedRot;
			if (flag4)
			{
				GUI.enabled = false;
			}
			bool flag5 = GUILayout.Button("R", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag5)
			{
				BackUpData.RestoreRotation(CommonUIData.obj, CommonUIData.bone);
				BackUpData.Refresh();
			}
			GUI.enabled = true;
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("拡縮", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			bool flag6 = backUpBoneData == null || !backUpBoneData.changedScl;
			if (flag6)
			{
				GUI.enabled = false;
			}
			bool flag7 = GUILayout.Button("R", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag7)
			{
				BackUpData.RestoreScale(CommonUIData.obj, CommonUIData.bone);
				BackUpData.Refresh();
			}
			GUI.enabled = true;
			GUILayout.EndHorizontal();
		}

		private MultipleMaidObjectSelectUI objectSelectUI = new MultipleMaidObjectSelectUI();
	}
}
