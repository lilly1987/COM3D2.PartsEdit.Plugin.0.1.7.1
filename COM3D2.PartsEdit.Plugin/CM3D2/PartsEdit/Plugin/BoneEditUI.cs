using System;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class BoneEditUI
	{
		public void Draw()
		{
			GUILayout.Label("ボーン編集", new GUILayoutOption[0]);
			UIUtil.BeginIndentArea();
			bool flag = CommonUIData.bone;
			if (flag)
			{
				bool maidBoneDataExist = BackUpData.GetMaidBoneDataExist(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, CommonUIData.bone);
				if (maidBoneDataExist)
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					bool flag2 = GUILayout.Button("トランスフォームリセット", UIParams.Instance.bStyle, new GUILayoutOption[0]);
					if (flag2)
					{
						this.ResetTransform();
					}
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					UIUtil.BeginIndentArea();
					BackUpBoneData orNullMaidBoneData = BackUpData.GetOrNullMaidBoneData(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, CommonUIData.bone);
					bool flag3 = orNullMaidBoneData != null;
					if (flag3)
					{
						bool changedPos = orNullMaidBoneData.changedPos;
						if (changedPos)
						{
							GUILayout.BeginHorizontal(new GUILayoutOption[0]);
							bool flag4 = GUILayout.Button("位置リセット", UIParams.Instance.bStyle, new GUILayoutOption[0]);
							if (flag4)
							{
								this.ResetPosition();
							}
							GUILayout.FlexibleSpace();
							GUILayout.EndHorizontal();
						}
						bool changedRot = orNullMaidBoneData.changedRot;
						if (changedRot)
						{
							GUILayout.BeginHorizontal(new GUILayoutOption[0]);
							bool flag5 = GUILayout.Button("回転リセット", UIParams.Instance.bStyle, new GUILayoutOption[0]);
							if (flag5)
							{
								this.ResetRotation();
							}
							GUILayout.FlexibleSpace();
							GUILayout.EndHorizontal();
						}
						bool changedScl = orNullMaidBoneData.changedScl;
						if (changedScl)
						{
							GUILayout.BeginHorizontal(new GUILayoutOption[0]);
							bool flag6 = GUILayout.Button("拡縮リセット", UIParams.Instance.bStyle, new GUILayoutOption[0]);
							if (flag6)
							{
								this.ResetScale();
							}
							GUILayout.FlexibleSpace();
							GUILayout.EndHorizontal();
						}
					}
					UIUtil.EndoIndentArea();
				}
			}
			UIUtil.EndoIndentArea();
		}

		private void ResetTransform()
		{
			Transform bone = CommonUIData.bone;
			BackUpBoneData orNullMaidBoneData = BackUpData.GetOrNullMaidBoneData(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, bone);
			bool flag = orNullMaidBoneData == null;
			if (!flag)
			{
				bool changedPos = orNullMaidBoneData.changedPos;
				if (changedPos)
				{
					bone.localPosition = orNullMaidBoneData.position;
				}
				bool changedRot = orNullMaidBoneData.changedRot;
				if (changedRot)
				{
					bone.localRotation = orNullMaidBoneData.rotation;
				}
				bool changedScl = orNullMaidBoneData.changedScl;
				if (changedScl)
				{
					bone.localScale = orNullMaidBoneData.scale;
				}
				BackUpData.GetOrNullMaidObjectData(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj).boneDataDic.Remove(bone);
			}
		}

		private void ResetPosition()
		{
			Transform bone = CommonUIData.bone;
			BackUpBoneData orNullMaidBoneData = BackUpData.GetOrNullMaidBoneData(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, bone);
			bool flag = orNullMaidBoneData == null;
			if (!flag)
			{
				bool changedPos = orNullMaidBoneData.changedPos;
				if (changedPos)
				{
					bone.localPosition = orNullMaidBoneData.position;
				}
				bool flag2 = orNullMaidBoneData.changedRot || orNullMaidBoneData.changedScl;
				if (flag2)
				{
					orNullMaidBoneData.changedPos = false;
				}
				else
				{
					BackUpData.GetOrNullMaidObjectData(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj).boneDataDic.Remove(bone);
				}
			}
		}

		private void ResetRotation()
		{
			Transform bone = CommonUIData.bone;
			BackUpBoneData orNullMaidBoneData = BackUpData.GetOrNullMaidBoneData(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, bone);
			bool flag = orNullMaidBoneData == null;
			if (!flag)
			{
				bool changedRot = orNullMaidBoneData.changedRot;
				if (changedRot)
				{
					bone.localRotation = orNullMaidBoneData.rotation;
				}
				bool flag2 = orNullMaidBoneData.changedPos || orNullMaidBoneData.changedScl;
				if (flag2)
				{
					orNullMaidBoneData.changedRot = false;
				}
				else
				{
					BackUpData.GetOrNullMaidObjectData(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj).boneDataDic.Remove(bone);
				}
			}
		}

		private void ResetScale()
		{
			Transform bone = CommonUIData.bone;
			BackUpBoneData orNullMaidBoneData = BackUpData.GetOrNullMaidBoneData(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, bone);
			bool flag = orNullMaidBoneData == null;
			if (!flag)
			{
				bool changedScl = orNullMaidBoneData.changedScl;
				if (changedScl)
				{
					bone.localScale = orNullMaidBoneData.scale;
				}
				bool flag2 = orNullMaidBoneData.changedPos || orNullMaidBoneData.changedRot;
				if (flag2)
				{
					orNullMaidBoneData.changedScl = false;
				}
				else
				{
					BackUpData.GetOrNullMaidObjectData(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj).boneDataDic.Remove(bone);
				}
			}
		}
	}
}
