using System;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class MaidObjectUI
	{
		public void Draw()
		{
			this.DrawMaid();
		}

		private void DrawMaid()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("メイド:", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			this.maidSelectUI.DrawCombo();
			bool flag = !CommonUIData.maid || !BackUpData.GetMaidDataExist(CommonUIData.maid);
			if (flag)
			{
				GUI.enabled = false;
			}
			bool flag2 = GUILayout.Button("R", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag2)
			{
				BackUpData.RestoreMaid(CommonUIData.maid);
				BackUpData.Refresh();
			}
			GUI.enabled = true;
			GUILayout.EndHorizontal();
			UIUtil.BeginIndentArea();
			this.DrawSlot();
			UIUtil.EndoIndentArea();
		}

		private void DrawSlot()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("スロット:", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			this.slotSelectUI.DrawCombo();
			bool flag = CommonUIData.slotNo == -2 || !BackUpData.GetMaidSlotDataExist(CommonUIData.maid, CommonUIData.slotNo);
			if (flag)
			{
				GUI.enabled = false;
			}
			bool flag2 = GUILayout.Button("R", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag2)
			{
				BackUpData.RestoreSlot(CommonUIData.maid, CommonUIData.slotNo);
				BackUpData.Refresh();
			}
			GUI.enabled = true;
			GUILayout.EndHorizontal();
			UIUtil.BeginIndentArea();
			this.DrawObject();
			UIUtil.EndoIndentArea();
		}

		private void DrawObject()
		{
			this.DrawImportExport();
			this.DrawYure();
			this.DrawBone();
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

		private void DrawYure()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("揺れボーン", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			bool flag = !YureUtil.GetYureAble(CommonUIData.maid, CommonUIData.slotNo);
			if (flag)
			{
				GUI.enabled = false;
				GUILayout.Toggle(false, "", UIParams.Instance.tStyle, new GUILayoutOption[0]);
				GUI.enabled = true;
			}
			else
			{
				bool yureState = YureUtil.GetYureState(CommonUIData.maid, CommonUIData.slotNo);
				bool flag2 = GUILayout.Toggle(yureState, "", UIParams.Instance.tStyle, new GUILayoutOption[0]);
				bool flag3 = flag2 != yureState;
				if (flag3)
				{
					YureUtil.SetYureState(CommonUIData.maid, CommonUIData.slotNo, flag2);
					BackUpObjectData orAddMaidObjectData = BackUpData.GetOrAddMaidObjectData(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj);
					bool flag4 = orAddMaidObjectData.changedYure && flag2 == orAddMaidObjectData.bYure;
					if (flag4)
					{
						bool flag5 = orAddMaidObjectData.boneDataDic.Count == 0;
						if (flag5)
						{
							BackUpSlotData orNullMaidSlotData = BackUpData.GetOrNullMaidSlotData(CommonUIData.maid, CommonUIData.slotNo);
							orNullMaidSlotData.objectDataDic.Remove(CommonUIData.obj);
						}
						else
						{
							orAddMaidObjectData.changedYure = false;
						}
						BackUpData.Refresh();
					}
					else
					{
						orAddMaidObjectData.changedYure = true;
						orAddMaidObjectData.bYure = yureState;
					}
				}
			}
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
			bool flag2 = !CommonUIData.bone || !BackUpData.GetMaidBoneDataExist(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, CommonUIData.bone);
			if (flag2)
			{
				GUI.enabled = false;
			}
			bool flag3 = GUILayout.Button("R", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag3)
			{
				BackUpData.RestoreBone(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, CommonUIData.bone);
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
				backUpBoneData = BackUpData.GetOrNullMaidBoneData(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, CommonUIData.bone);
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
				BackUpData.RestorePosition(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, CommonUIData.bone);
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
				BackUpData.RestoreRotation(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, CommonUIData.bone);
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
				BackUpData.RestoreScale(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, CommonUIData.bone);
				BackUpData.Refresh();
			}
			GUI.enabled = true;
			GUILayout.EndHorizontal();
		}

		private MaidSelectUI maidSelectUI = new MaidSelectUI();

		private MaidSlotSelectUI slotSelectUI = new MaidSlotSelectUI();
	}
}
