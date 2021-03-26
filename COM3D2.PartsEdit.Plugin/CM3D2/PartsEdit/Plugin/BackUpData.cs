using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal static class BackUpData
	{
		public static void Refresh()
		{
			foreach (Maid maid in BackUpData.maidDataDic.Keys)
			{
				BackUpData.RefreshMaid(maid);
			}
			BackUpData.maidDataDic = (from kvp in BackUpData.maidDataDic
			where kvp.Value.slotDataDic.Count != 0
			select kvp).ToDictionary((KeyValuePair<Maid, BackUpMaidData> kvp) => kvp.Key, (KeyValuePair<Maid, BackUpMaidData> kvp) => kvp.Value);
			foreach (GameObject obj in BackUpData.objectDataDic.Keys)
			{
				BackUpData.RefreshObject(obj);
			}
			BackUpData.objectDataDic = (from kvp in BackUpData.objectDataDic
			where kvp.Value.boneDataDic.Count != 0
			select kvp).ToDictionary((KeyValuePair<GameObject, BackUpObjectData> kvp) => kvp.Key, (KeyValuePair<GameObject, BackUpObjectData> kvp) => kvp.Value);
		}

		public static void RefreshMaid(Maid maid)
		{
			BackUpMaidData orNullMaidData = BackUpData.GetOrNullMaidData(maid);
			bool flag = orNullMaidData == null;
			if (!flag)
			{
				foreach (int slotNo in orNullMaidData.slotDataDic.Keys)
				{
					BackUpData.RefreshSlot(maid, slotNo);
				}
				orNullMaidData.slotDataDic = (from kvp in orNullMaidData.slotDataDic
				where kvp.Value.objectDataDic.Count != 0
				select kvp).ToDictionary((KeyValuePair<int, BackUpSlotData> kvp) => kvp.Key, (KeyValuePair<int, BackUpSlotData> kvp) => kvp.Value);
			}
		}

		public static void RefreshSlot(Maid maid, int slotNo)
		{
			BackUpSlotData orNullMaidSlotData = BackUpData.GetOrNullMaidSlotData(maid, slotNo);
			bool flag = orNullMaidSlotData == null;
			if (!flag)
			{
				foreach (GameObject obj in orNullMaidSlotData.objectDataDic.Keys)
				{
					BackUpData.RefreshObject(maid, slotNo, obj);
				}
				orNullMaidSlotData.objectDataDic = (from kvp in orNullMaidSlotData.objectDataDic
				where kvp.Key != null && (kvp.Value.changedYure || kvp.Value.boneDataDic.Count != 0)
				select kvp).ToDictionary((KeyValuePair<GameObject, BackUpObjectData> kvp) => kvp.Key, (KeyValuePair<GameObject, BackUpObjectData> kvp) => kvp.Value);
			}
		}

		public static void RefreshObject(Maid maid, int slotNo, GameObject obj)
		{
			BackUpObjectData orNullMaidObjectData = BackUpData.GetOrNullMaidObjectData(maid, slotNo, obj);
			bool flag = orNullMaidObjectData == null;
			if (!flag)
			{
				orNullMaidObjectData.boneDataDic = (from kvp in orNullMaidObjectData.boneDataDic
				where kvp.Key != null && (kvp.Value.changedPos || kvp.Value.changedRot || kvp.Value.changedScl)
				select kvp).ToDictionary((KeyValuePair<Transform, BackUpBoneData> kvp) => kvp.Key, (KeyValuePair<Transform, BackUpBoneData> kvp) => kvp.Value);
			}
		}

		public static void RefreshObject(GameObject obj)
		{
			BackUpObjectData orNullObjectData = BackUpData.GetOrNullObjectData(obj);
			bool flag = orNullObjectData == null;
			if (!flag)
			{
				orNullObjectData.boneDataDic = (from kvp in orNullObjectData.boneDataDic
				where kvp.Key != null && (kvp.Value.changedPos || kvp.Value.changedRot || kvp.Value.changedScl)
				select kvp).ToDictionary((KeyValuePair<Transform, BackUpBoneData> kvp) => kvp.Key, (KeyValuePair<Transform, BackUpBoneData> kvp) => kvp.Value);
			}
		}

		public static void Restore()
		{
			List<Maid> list = new List<Maid>(BackUpData.maidDataDic.Keys);
			foreach (Maid maid in list)
			{
				bool flag = !maid;
				if (!flag)
				{
					BackUpData.RestoreMaid(maid);
				}
			}
			BackUpData.maidDataDic.Clear();
		}

		public static void RestoreMaid(Maid maid)
		{
			BackUpMaidData orNullMaidData = BackUpData.GetOrNullMaidData(maid);
			bool flag = orNullMaidData == null;
			if (!flag)
			{
				List<int> list = new List<int>(orNullMaidData.slotDataDic.Keys);
				foreach (int slotNo in list)
				{
					BackUpData.RestoreSlot(maid, slotNo);
				}
				BackUpData.maidDataDic.Remove(maid);
			}
		}

		public static void RestoreSlot(Maid maid, int slotNo)
		{
			BackUpSlotData orNullMaidSlotData = BackUpData.GetOrNullMaidSlotData(maid, slotNo);
			bool flag = orNullMaidSlotData == null;
			if (!flag)
			{
				List<GameObject> list = new List<GameObject>(orNullMaidSlotData.objectDataDic.Keys);
				foreach (GameObject gameObject in list)
				{
					bool flag2 = !gameObject;
					if (!flag2)
					{
						BackUpData.RestoreObject(maid, slotNo, gameObject);
					}
				}
				BackUpMaidData orNullMaidData = BackUpData.GetOrNullMaidData(maid);
				orNullMaidData.slotDataDic.Remove(slotNo);
			}
		}

		public static void RestoreObject(Maid maid, int slotNo, GameObject obj)
		{
			BackUpObjectData orNullMaidObjectData = BackUpData.GetOrNullMaidObjectData(maid, slotNo, obj);
			bool flag = orNullMaidObjectData == null;
			if (!flag)
			{
				bool changedYure = orNullMaidObjectData.changedYure;
				if (changedYure)
				{
					bool flag2 = slotNo != -1;
					if (flag2)
					{
						YureUtil.SetYureState(maid, slotNo, orNullMaidObjectData.bYure);
					}
				}
				List<Transform> list = new List<Transform>(orNullMaidObjectData.boneDataDic.Keys);
				foreach (Transform transform in list)
				{
					bool flag3 = !transform;
					if (!flag3)
					{
						BackUpData.RestoreBone(maid, slotNo, obj, transform);
					}
				}
				BackUpSlotData orNullMaidSlotData = BackUpData.GetOrNullMaidSlotData(maid, slotNo);
				orNullMaidSlotData.objectDataDic.Remove(obj);
			}
		}

		public static void RestoreBone(Maid maid, int slotNo, GameObject obj, Transform bone)
		{
			BackUpBoneData orNullMaidBoneData = BackUpData.GetOrNullMaidBoneData(maid, slotNo, obj, bone);
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
				BackUpObjectData orNullMaidObjectData = BackUpData.GetOrNullMaidObjectData(maid, slotNo, obj);
				orNullMaidObjectData.boneDataDic.Remove(bone);
			}
		}

		public static void RestorePosition(Maid maid, int slotNo, GameObject obj, Transform bone)
		{
			BackUpBoneData orNullMaidBoneData = BackUpData.GetOrNullMaidBoneData(maid, slotNo, obj, bone);
			bool flag = orNullMaidBoneData == null;
			if (!flag)
			{
				bool changedPos = orNullMaidBoneData.changedPos;
				if (changedPos)
				{
					bone.localPosition = orNullMaidBoneData.position;
					bool flag2 = orNullMaidBoneData.changedRot || orNullMaidBoneData.changedScl;
					if (flag2)
					{
						orNullMaidBoneData.changedPos = false;
					}
					else
					{
						BackUpData.GetOrNullMaidObjectData(maid, slotNo, obj).boneDataDic.Remove(bone);
					}
				}
			}
		}

		public static void RestoreRotation(Maid maid, int slotNo, GameObject obj, Transform bone)
		{
			BackUpBoneData orNullMaidBoneData = BackUpData.GetOrNullMaidBoneData(maid, slotNo, obj, bone);
			bool flag = orNullMaidBoneData == null;
			if (!flag)
			{
				bool changedRot = orNullMaidBoneData.changedRot;
				if (changedRot)
				{
					bone.localRotation = orNullMaidBoneData.rotation;
					bool flag2 = orNullMaidBoneData.changedPos || orNullMaidBoneData.changedScl;
					if (flag2)
					{
						orNullMaidBoneData.changedRot = false;
					}
					else
					{
						BackUpData.GetOrNullMaidObjectData(maid, slotNo, obj).boneDataDic.Remove(bone);
					}
				}
			}
		}

		public static void RestoreScale(Maid maid, int slotNo, GameObject obj, Transform bone)
		{
			BackUpBoneData orNullMaidBoneData = BackUpData.GetOrNullMaidBoneData(maid, slotNo, obj, bone);
			bool flag = orNullMaidBoneData == null;
			if (!flag)
			{
				bool changedScl = orNullMaidBoneData.changedScl;
				if (changedScl)
				{
					bone.localScale = orNullMaidBoneData.scale;
					bool flag2 = orNullMaidBoneData.changedPos || orNullMaidBoneData.changedRot;
					if (flag2)
					{
						orNullMaidBoneData.changedScl = false;
					}
					else
					{
						BackUpData.GetOrNullMaidObjectData(maid, slotNo, obj).boneDataDic.Remove(bone);
					}
				}
			}
		}

		public static void RestoreObject(GameObject obj)
		{
			BackUpObjectData orNullObjectData = BackUpData.GetOrNullObjectData(obj);
			bool flag = orNullObjectData == null;
			if (!flag)
			{
				List<Transform> list = new List<Transform>(orNullObjectData.boneDataDic.Keys);
				foreach (Transform transform in list)
				{
					bool flag2 = !transform;
					if (!flag2)
					{
						BackUpData.RestoreBone(obj, transform);
					}
				}
				BackUpData.objectDataDic.Remove(obj);
			}
		}

		public static void RestoreBone(GameObject obj, Transform bone)
		{
			BackUpBoneData orNullBoneData = BackUpData.GetOrNullBoneData(obj, bone);
			bool flag = orNullBoneData == null;
			if (!flag)
			{
				bool changedPos = orNullBoneData.changedPos;
				if (changedPos)
				{
					bone.localPosition = orNullBoneData.position;
				}
				bool changedRot = orNullBoneData.changedRot;
				if (changedRot)
				{
					bone.localRotation = orNullBoneData.rotation;
				}
				bool changedScl = orNullBoneData.changedScl;
				if (changedScl)
				{
					bone.localScale = orNullBoneData.scale;
				}
				BackUpObjectData orNullObjectData = BackUpData.GetOrNullObjectData(obj);
				orNullObjectData.boneDataDic.Remove(bone);
			}
		}

		public static void RestorePosition(GameObject obj, Transform bone)
		{
			BackUpBoneData orNullBoneData = BackUpData.GetOrNullBoneData(obj, bone);
			bool flag = orNullBoneData == null;
			if (!flag)
			{
				bool changedPos = orNullBoneData.changedPos;
				if (changedPos)
				{
					bone.localPosition = orNullBoneData.position;
					bool flag2 = orNullBoneData.changedRot || orNullBoneData.changedScl;
					if (flag2)
					{
						orNullBoneData.changedPos = false;
					}
					else
					{
						BackUpData.GetOrNullObjectData(obj).boneDataDic.Remove(bone);
					}
				}
			}
		}

		public static void RestoreRotation(GameObject obj, Transform bone)
		{
			BackUpBoneData orNullBoneData = BackUpData.GetOrNullBoneData(obj, bone);
			bool flag = orNullBoneData == null;
			if (!flag)
			{
				bool changedRot = orNullBoneData.changedRot;
				if (changedRot)
				{
					bone.localRotation = orNullBoneData.rotation;
					bool flag2 = orNullBoneData.changedPos || orNullBoneData.changedScl;
					if (flag2)
					{
						orNullBoneData.changedRot = false;
					}
					else
					{
						BackUpData.GetOrNullObjectData(obj).boneDataDic.Remove(bone);
					}
				}
			}
		}

		public static void RestoreScale(GameObject obj, Transform bone)
		{
			BackUpBoneData orNullBoneData = BackUpData.GetOrNullBoneData(obj, bone);
			bool flag = orNullBoneData == null;
			if (!flag)
			{
				bool changedScl = orNullBoneData.changedScl;
				if (changedScl)
				{
					bone.localScale = orNullBoneData.scale;
					bool flag2 = orNullBoneData.changedPos || orNullBoneData.changedRot;
					if (flag2)
					{
						orNullBoneData.changedScl = false;
					}
					else
					{
						BackUpData.GetOrNullObjectData(obj).boneDataDic.Remove(bone);
					}
				}
			}
		}

		public static bool GetMaidDataExist(Maid maid)
		{
			bool flag = !BackUpData.maidDataDic.ContainsKey(maid);
			return !flag;
		}

		public static bool GetMaidSlotDataExist(Maid maid, int slotNo)
		{
			bool flag = !BackUpData.GetMaidDataExist(maid);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !BackUpData.maidDataDic[maid].slotDataDic.ContainsKey(slotNo);
				result = !flag2;
			}
			return result;
		}

		public static bool GetMaidObjectDataExist(Maid maid, int slotNo, GameObject obj)
		{
			bool flag = !BackUpData.GetMaidDataExist(maid);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !BackUpData.GetMaidSlotDataExist(maid, slotNo);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = !BackUpData.maidDataDic[maid].slotDataDic[slotNo].objectDataDic.ContainsKey(obj);
					result = !flag3;
				}
			}
			return result;
		}

		public static bool GetMaidBoneDataExist(Maid maid, int slotNo, GameObject obj, Transform bone)
		{
			bool flag = !BackUpData.GetMaidDataExist(maid);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !BackUpData.GetMaidSlotDataExist(maid, slotNo);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = !BackUpData.GetMaidObjectDataExist(maid, slotNo, obj);
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = !BackUpData.maidDataDic[maid].slotDataDic[slotNo].objectDataDic[obj].boneDataDic.ContainsKey(bone);
						result = !flag4;
					}
				}
			}
			return result;
		}

		public static bool GetObjectDataExist(GameObject obj)
		{
			bool flag = !BackUpData.objectDataDic.ContainsKey(obj);
			return !flag;
		}

		public static bool GetBoneDataExist(GameObject obj, Transform bone)
		{
			bool flag = !BackUpData.GetObjectDataExist(obj);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !BackUpData.objectDataDic[obj].boneDataDic.ContainsKey(bone);
				result = !flag2;
			}
			return result;
		}

		public static BackUpMaidData GetOrNullMaidData(Maid maid)
		{
			BackUpMaidData result = null;
			bool flag = BackUpData.maidDataDic.ContainsKey(maid);
			if (flag)
			{
				result = BackUpData.maidDataDic[maid];
			}
			return result;
		}

		public static BackUpSlotData GetOrNullMaidSlotData(Maid maid, int slotNo)
		{
			BackUpSlotData result = null;
			BackUpMaidData orNullMaidData = BackUpData.GetOrNullMaidData(maid);
			bool flag = orNullMaidData != null && orNullMaidData.slotDataDic.ContainsKey(slotNo);
			if (flag)
			{
				result = orNullMaidData.slotDataDic[slotNo];
			}
			return result;
		}

		public static BackUpObjectData GetOrNullMaidObjectData(Maid maid, int slotNo, GameObject obj)
		{
			BackUpObjectData result = null;
			BackUpSlotData orNullMaidSlotData = BackUpData.GetOrNullMaidSlotData(maid, slotNo);
			bool flag = orNullMaidSlotData != null && orNullMaidSlotData.objectDataDic.ContainsKey(obj);
			if (flag)
			{
				result = orNullMaidSlotData.objectDataDic[obj];
			}
			return result;
		}

		public static BackUpBoneData GetOrNullMaidBoneData(Maid maid, int slotNo, GameObject obj, Transform bone)
		{
			BackUpBoneData result = null;
			BackUpObjectData orNullMaidObjectData = BackUpData.GetOrNullMaidObjectData(maid, slotNo, obj);
			bool flag = orNullMaidObjectData != null && orNullMaidObjectData.boneDataDic.ContainsKey(bone);
			if (flag)
			{
				result = orNullMaidObjectData.boneDataDic[bone];
			}
			return result;
		}

		public static BackUpObjectData GetOrNullObjectData(GameObject obj)
		{
			BackUpObjectData result = null;
			bool flag = BackUpData.objectDataDic.ContainsKey(obj);
			if (flag)
			{
				result = BackUpData.objectDataDic[obj];
			}
			return result;
		}

		public static BackUpBoneData GetOrNullBoneData(GameObject obj, Transform bone)
		{
			BackUpBoneData result = null;
			BackUpObjectData orNullObjectData = BackUpData.GetOrNullObjectData(obj);
			bool flag = orNullObjectData != null && orNullObjectData.boneDataDic.ContainsKey(bone);
			if (flag)
			{
				result = orNullObjectData.boneDataDic[bone];
			}
			return result;
		}

		public static BackUpMaidData GetOrAddMaidData(Maid maid)
		{
			bool flag = BackUpData.maidDataDic.ContainsKey(maid);
			BackUpMaidData backUpMaidData;
			if (flag)
			{
				backUpMaidData = BackUpData.maidDataDic[maid];
			}
			else
			{
				backUpMaidData = new BackUpMaidData();
				BackUpData.maidDataDic.Add(maid, backUpMaidData);
			}
			return backUpMaidData;
		}

		public static BackUpSlotData GetOrAddMaidSlotData(Maid maid, int slotNo)
		{
			BackUpMaidData orAddMaidData = BackUpData.GetOrAddMaidData(maid);
			bool flag = orAddMaidData.slotDataDic.ContainsKey(slotNo);
			BackUpSlotData backUpSlotData;
			if (flag)
			{
				backUpSlotData = orAddMaidData.slotDataDic[slotNo];
			}
			else
			{
				backUpSlotData = new BackUpSlotData();
				orAddMaidData.slotDataDic.Add(slotNo, backUpSlotData);
			}
			return backUpSlotData;
		}

		public static BackUpObjectData GetOrAddMaidObjectData(Maid maid, int slotNo, GameObject obj)
		{
			BackUpSlotData orAddMaidSlotData = BackUpData.GetOrAddMaidSlotData(maid, slotNo);
			bool flag = orAddMaidSlotData.objectDataDic.ContainsKey(obj);
			BackUpObjectData backUpObjectData;
			if (flag)
			{
				backUpObjectData = orAddMaidSlotData.objectDataDic[obj];
			}
			else
			{
				backUpObjectData = new BackUpObjectData();
				orAddMaidSlotData.objectDataDic.Add(obj, backUpObjectData);
			}
			return backUpObjectData;
		}

		public static BackUpBoneData GetOrAddMaidBoneData(Maid maid, int slotNo, GameObject obj, Transform bone)
		{
			BackUpObjectData orAddMaidObjectData = BackUpData.GetOrAddMaidObjectData(maid, slotNo, obj);
			bool flag = orAddMaidObjectData.boneDataDic.ContainsKey(bone);
			BackUpBoneData backUpBoneData;
			if (flag)
			{
				backUpBoneData = orAddMaidObjectData.boneDataDic[bone];
			}
			else
			{
				backUpBoneData = new BackUpBoneData();
				orAddMaidObjectData.boneDataDic.Add(bone, backUpBoneData);
			}
			return backUpBoneData;
		}

		public static BackUpObjectData GetOrAddObjectData(GameObject obj)
		{
			bool flag = BackUpData.objectDataDic.ContainsKey(obj);
			BackUpObjectData backUpObjectData;
			if (flag)
			{
				backUpObjectData = BackUpData.objectDataDic[obj];
			}
			else
			{
				backUpObjectData = new BackUpObjectData();
				BackUpData.objectDataDic.Add(obj, backUpObjectData);
			}
			return backUpObjectData;
		}

		public static BackUpBoneData GetOrAddBoneData(GameObject obj, Transform bone)
		{
			BackUpObjectData orAddObjectData = BackUpData.GetOrAddObjectData(obj);
			bool flag = orAddObjectData.boneDataDic.ContainsKey(bone);
			BackUpBoneData backUpBoneData;
			if (flag)
			{
				backUpBoneData = orAddObjectData.boneDataDic[bone];
			}
			else
			{
				backUpBoneData = new BackUpBoneData();
				orAddObjectData.boneDataDic.Add(bone, backUpBoneData);
			}
			return backUpBoneData;
		}

		public static Dictionary<Maid, BackUpMaidData> maidDataDic = new Dictionary<Maid, BackUpMaidData>();

		public static Dictionary<GameObject, BackUpObjectData> objectDataDic = new Dictionary<GameObject, BackUpObjectData>();
	}
}
