using System;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal static class CommonUIData
	{
		public static void SetMaid(Maid fMaid)
		{
			bool flag = CommonUIData.maid != fMaid || fMaid == null;
			if (flag)
			{
				CommonUIData.maid = fMaid;
				CommonUIData.SetSlot(-2);
			}
		}

		public static void SetSlot(int fSlotNo)
		{
			bool flag = CommonUIData.slotNo != fSlotNo || fSlotNo == -2;
			if (flag)
			{
				CommonUIData.slotNo = fSlotNo;
				int num = CommonUIData.slotNo;
				if (num != -2)
				{
					if (num != -1)
					{
						CommonUIData.SetObject(CommonUIData.maid.body0.goSlot[CommonUIData.slotNo].obj);
					}
					else
					{
						CommonUIData.SetObject(CommonUIData.maid.body0.m_Bones.gameObject);
					}
				}
				else
				{
					CommonUIData.SetObject(null);
				}
			}
		}

		public static void SetObject(GameObject fObj)
		{
			bool flag = CommonUIData.obj != fObj || fObj == null;
			if (flag)
			{
				CommonUIData.obj = fObj;
				CommonUIData.SetBone(null);
			}
		}

		public static void SetBone(Transform fBone)
		{
			bool flag = CommonUIData.bone != fBone;
			if (flag)
			{
				CommonUIData.bone = fBone;
			}
		}

		public static Maid maid = null;

		public static int slotNo = -2;

		public static GameObject obj = null;

		public static Transform bone = null;

		public static bool yureExist = false;

		public static bool yureEnable = false;
	}
}
