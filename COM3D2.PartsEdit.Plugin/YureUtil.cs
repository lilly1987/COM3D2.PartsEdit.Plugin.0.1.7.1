using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

internal static class YureUtil
{
	public static bool GetYureAble(Maid maid, int slotNo)
	{
		bool flag = !maid;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = slotNo == -2 || slotNo == -1;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = maid.body0.goSlot[slotNo].obj == null;
				if (flag3)
				{
					result = false;
				}
				else
				{
					DynamicBone component = maid.body0.goSlot[slotNo].obj.GetComponent<DynamicBone>();
					DynamicSkirtBone component2 = maid.body0.goSlot[slotNo].obj.GetComponent<DynamicSkirtBone>();
					bool flag4 = component || component2;
					if (flag4)
					{
						result = true;
					}
					else
					{
						TBoneHair_ bonehair = maid.body0.goSlot[slotNo].bonehair;
						bool flag5 = bonehair == null;
						if (flag5)
						{
							result = false;
						}
						else
						{
							List<THair1> list = (List<THair1>)YureUtil.fi_hair1list.GetValue(bonehair);
							bool flag6 = list == null || (list.Count == 0 && !bonehair.boSkirt);
							result = !flag6;
						}
					}
				}
			}
		}
		return result;
	}

	public static bool GetYureState(Maid maid, int slotNo)
	{
		bool flag = !maid;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = slotNo == -2 || slotNo == -1;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = maid.body0.goSlot[slotNo].obj == null;
				if (flag3)
				{
					result = false;
				}
				else
				{
					TBoneHair_ bonehair = maid.body0.goSlot[slotNo].bonehair;
					bool flag4 = (bool)YureUtil.fi_m_bEnable.GetValue(bonehair);
					TBodySkin tbodySkin = maid.body0.goSlot[slotNo];
					DynamicBone component = maid.body0.goSlot[slotNo].obj.GetComponent<DynamicBone>();
					DynamicSkirtBone component2 = maid.body0.goSlot[slotNo].obj.GetComponent<DynamicSkirtBone>();
					BoneHair3 bonehair2 = tbodySkin.bonehair3;
					flag4 = (flag4 || (component && component.enabled) || (bonehair2 != null && YureUtil.fi_m_SkirtBone.GetValue(bonehair2) != null));
					result = flag4;
				}
			}
		}
		return result;
	}

	public static void SetYureState(Maid maid, int slotNo, bool state)
	{
		bool flag = !maid;
		if (!flag)
		{
			bool flag2 = slotNo == -2 || slotNo == -1;
			if (!flag2)
			{
				bool flag3 = maid.body0.goSlot[slotNo].obj == null;
				if (!flag3)
				{
					TBoneHair_ bonehair = maid.body0.goSlot[slotNo].bonehair;
					YureUtil.fi_m_bEnable.SetValue(bonehair, state);
					TBodySkin tbodySkin = maid.body0.goSlot[slotNo];
					DynamicBone component = maid.body0.goSlot[slotNo].obj.GetComponent<DynamicBone>();
					DynamicSkirtBone component2 = maid.body0.goSlot[slotNo].obj.GetComponent<DynamicSkirtBone>();
					BoneHair3 bonehair2 = tbodySkin.bonehair3;
					bool flag4 = component;
					if (flag4)
					{
						Dictionary<Transform, Vector3> dictionary = new Dictionary<Transform, Vector3>();
						Dictionary<Transform, Quaternion> dictionary2 = new Dictionary<Transform, Quaternion>();
						Dictionary<Transform, Vector3> dictionary3 = new Dictionary<Transform, Vector3>();
						foreach (DynamicBone.Particle particle in component.m_Particles)
						{
							dictionary[particle.m_Transform] = particle.m_Transform.localPosition;
							dictionary2[particle.m_Transform] = particle.m_Transform.localRotation;
							dictionary3[particle.m_Transform] = particle.m_Transform.localScale;
						}
						component.enabled = state;
						foreach (DynamicBone.Particle particle2 in component.m_Particles)
						{
							particle2.m_Transform.localPosition = dictionary[particle2.m_Transform];
							particle2.m_Transform.localRotation = dictionary2[particle2.m_Transform];
							particle2.m_Transform.localScale = dictionary3[particle2.m_Transform];
						}
					}
					bool flag5 = component2;
					if (flag5)
					{
						component2.enabled = state;
						if (state)
						{
							YureUtil.fi_m_SkirtBone.SetValue(bonehair2, component2);
						}
						else
						{
							YureUtil.fi_m_SkirtBone.SetValue(bonehair2, null);
						}
					}
				}
			}
		}
	}

	private static FieldInfo fi_hair1list = Helper.GetFieldInfo(typeof(TBoneHair_), "hair1list");

	private static FieldInfo fi_m_bEnable = Helper.GetFieldInfo(typeof(TBoneHair_), "m_bEnable");

	private static FieldInfo fi_m_SkirtBone = Helper.GetFieldInfo(typeof(BoneHair3), "m_SkirtBone");
}
