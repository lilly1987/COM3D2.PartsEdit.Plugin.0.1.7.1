using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal static class PresetManager
	{
		public static SortedDictionary<string, PresetFileData> PresetFileDataDic
		{
			get
			{
				bool flag = PresetManager.presetFileDataDic == null;
				if (flag)
				{
					PresetManager.LoadPresetFileList();
				}
				return PresetManager.presetFileDataDic;
			}
			set
			{
				PresetManager.presetFileDataDic = value;
			}
		}

		public static void SaveObjectData(string fileName)
		{
			PresetManager.DirectoryCheckAndCreate();
			ObjectData objectDataFromObject = PresetManager.GetObjectDataFromObject();
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObjectData));
			StreamWriter streamWriter = new StreamWriter(PresetManager.directoryPath + fileName + ".xml", false, new UTF8Encoding(false));
			xmlSerializer.Serialize(streamWriter, objectDataFromObject);
			streamWriter.Close();
			PresetFileData presetFileData = new PresetFileData();
			presetFileData.filename = fileName;
			presetFileData.objectData = objectDataFromObject;
			PresetManager.PresetFileDataDic[fileName] = presetFileData;
		}

		public static void LoadObjectData(string fileName)
		{
			ObjectData objectData = PresetManager.PresetFileDataDic[fileName].objectData;
			PresetManager.ApplyObjectDataToObject(objectData);
		}

		public static string[] GetFileList()
		{
			bool flag = !Directory.Exists(PresetManager.directoryPath);
			string[] result;
			if (flag)
			{
				result = new string[0];
			}
			else
			{
				result = PresetManager.PresetFileDataDic.Keys.ToArray<string>();
			}
			return result;
		}

		public static string[] GetFileList(string category, string name)
		{
			PresetFileData[] source = PresetManager.PresetFileDataDic.Values.ToArray<PresetFileData>();
			bool flag = category != null;
			if (flag)
			{
				source = (from data in source
				where data.objectData.slotName == category
				select data).ToArray<PresetFileData>();
			}
			bool flag2 = name != null;
			if (flag2)
			{
				source = (from data in source
				where data.objectData.rootData.name == name
				select data).ToArray<PresetFileData>();
			}
			return (from data in source
			select data.filename).ToArray<string>();
		}

		private static void LoadPresetFileList()
		{
			PresetManager.presetFileDataDic = new SortedDictionary<string, PresetFileData>();
			bool flag = !Directory.Exists(PresetManager.directoryPath);
			if (!flag)
			{
				string[] files = Directory.GetFiles(PresetManager.directoryPath, "*.xml", SearchOption.TopDirectoryOnly);
				foreach (string path in files)
				{
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
					PresetFileData presetFileData = new PresetFileData();
					presetFileData.filename = fileNameWithoutExtension;
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObjectData));
					StreamReader streamReader = new StreamReader(path, new UTF8Encoding(false));
					presetFileData.objectData = (ObjectData)xmlSerializer.Deserialize(streamReader);
					streamReader.Close();
					PresetManager.presetFileDataDic[fileNameWithoutExtension] = presetFileData;
				}
			}
		}

		private static void DirectoryCheckAndCreate()
		{
			bool flag = !Directory.Exists(PresetManager.directoryPath);
			if (flag)
			{
				Directory.CreateDirectory(PresetManager.directoryPath);
			}
		}

		public static ObjectData GetObjectDataFromObject()
		{
			PresetManager.trsHash = new HashSet<Transform>();
			ObjectData objectData = new ObjectData();
			objectData.version = "0.1.6";
			objectData.bMaidParts = (Setting.targetSelectMode == 0);
			ObjectData.TransformData transformData = new ObjectData.TransformData();
			transformData.name = CommonUIData.obj.name;
			bool flag = Setting.targetSelectMode == 0;
			if (flag)
			{
				transformData.position = CommonUIData.obj.transform.localPosition;
				transformData.rotation = CommonUIData.obj.transform.localRotation;
			}
			transformData.scale = CommonUIData.obj.transform.localScale;
			objectData.rootData = transformData;
			bool flag2 = Setting.targetSelectMode == 0;
			if (flag2)
			{
				bool flag3 = CommonUIData.slotNo == -1;
				if (flag3)
				{
					objectData.bYure = false;
					objectData.slotName = "base";
				}
				else
				{
					objectData.bYure = YureUtil.GetYureState(CommonUIData.maid, CommonUIData.slotNo);
					objectData.slotName = SlotUtil.GetSlotName(CommonUIData.slotNo);
				}
			}
			else
			{
				objectData.bYure = false;
			}
			bool flag4 = Setting.targetSelectMode == 0 && CommonUIData.slotNo == -1;
			Transform[] array;
			if (flag4)
			{
				SkinnedMeshRenderer componentInChildren = CommonUIData.maid.body0.goSlot[0].obj.GetComponentInChildren<SkinnedMeshRenderer>();
				array = (from bone in componentInChildren.bones
				where bone != null
				select CMT.SearchObjName(CommonUIData.obj.transform, bone.name, true) into bone
				where bone != null
				select bone).ToArray<Transform>();
			}
			else
			{
				SkinnedMeshRenderer componentInChildren2 = CommonUIData.obj.GetComponentInChildren<SkinnedMeshRenderer>();
				array = (from bone in componentInChildren2.bones
				where bone != null
				select bone).ToArray<Transform>();
			}
			foreach (Transform transform in array)
			{
				Transform transform2 = transform;
				while (transform2 != CommonUIData.obj.transform)
				{
					bool flag5 = !transform2;
					if (flag5)
					{
						Debug.Log("ルートオブジェクト配下にありません:" + transform.name);
						return null;
					}
					bool flag6 = PresetManager.trsHash.Contains(transform2);
					if (flag6)
					{
						break;
					}
					ObjectData.TransformData transformData2 = new ObjectData.TransformData();
					transformData2.name = transform2.name;
					transformData2.scale = transform2.localScale;
					transformData2.position = transform2.localPosition;
					transformData2.rotation = transform2.localRotation;
					objectData.transformDataList.Add(transformData2);
					PresetManager.trsHash.Add(transform2);
					transform2 = transform2.parent;
				}
			}
			return objectData;
		}

		private static void ApplyObjectDataToObject(ObjectData objectData)
		{
			bool flag = Setting.targetSelectMode == 0;
			BackUpObjectData backUpObjectData;
			BackUpBoneData backUpBoneData;
			if (flag)
			{
				backUpObjectData = BackUpData.GetOrAddMaidObjectData(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj);
				backUpBoneData = BackUpData.GetOrAddMaidBoneData(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, CommonUIData.obj.transform);
			}
			else
			{
				backUpObjectData = BackUpData.GetOrAddObjectData(CommonUIData.obj);
				backUpBoneData = BackUpData.GetOrAddBoneData(CommonUIData.obj, CommonUIData.obj.transform);
			}
			bool flag2 = Setting.targetSelectMode == 0;
			if (flag2)
			{
				bool flag3 = CommonUIData.slotNo != -1;
				if (flag3)
				{
					bool changedYure = backUpObjectData.changedYure;
					if (changedYure)
					{
						bool flag4 = YureUtil.GetYureState(CommonUIData.maid, CommonUIData.slotNo) != objectData.bYure;
						if (flag4)
						{
							backUpObjectData.changedYure = false;
							backUpObjectData.bYure = true;
							YureUtil.SetYureState(CommonUIData.maid, CommonUIData.slotNo, objectData.bYure);
						}
					}
					else
					{
						bool flag5 = YureUtil.GetYureState(CommonUIData.maid, CommonUIData.slotNo) != objectData.bYure;
						if (flag5)
						{
							backUpObjectData.changedYure = true;
							backUpObjectData.bYure = YureUtil.GetYureState(CommonUIData.maid, CommonUIData.slotNo);
							YureUtil.SetYureState(CommonUIData.maid, CommonUIData.slotNo, objectData.bYure);
						}
					}
				}
				bool bMaidParts = objectData.bMaidParts;
				if (bMaidParts)
				{
					bool flag6 = !backUpBoneData.changedPos;
					if (flag6)
					{
						backUpBoneData.position = CommonUIData.obj.transform.localPosition;
						backUpBoneData.changedPos = true;
					}
					CommonUIData.obj.transform.localPosition = objectData.rootData.position;
					bool flag7 = !backUpBoneData.changedRot;
					if (flag7)
					{
						backUpBoneData.rotation = CommonUIData.obj.transform.localRotation;
						backUpBoneData.changedRot = true;
					}
					CommonUIData.obj.transform.localRotation = objectData.rootData.rotation;
				}
			}
			bool flag8 = !backUpBoneData.changedScl;
			if (flag8)
			{
				backUpBoneData.scale = CommonUIData.obj.transform.localScale;
				backUpBoneData.changedScl = true;
			}
			CommonUIData.obj.transform.localScale = objectData.rootData.scale;
			foreach (ObjectData.TransformData transformData in objectData.transformDataList)
			{
				bool flag9 = Setting.targetSelectMode == 0 && CommonUIData.slotNo == -1;
				Transform transform;
				if (flag9)
				{
					transform = CMT.SearchObjName(CommonUIData.obj.transform, transformData.name, true);
				}
				else
				{
					transform = CMT.SearchObjName(CommonUIData.obj.transform, transformData.name, false);
				}
				bool flag10 = !transform;
				if (!flag10)
				{
					bool flag11 = Setting.targetSelectMode == 0;
					BackUpBoneData backUpBoneData2;
					if (flag11)
					{
						backUpBoneData2 = BackUpData.GetOrAddMaidBoneData(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, transform);
					}
					else
					{
						backUpBoneData2 = BackUpData.GetOrAddBoneData(CommonUIData.obj, transform);
					}
					bool flag12 = !backUpBoneData2.changedPos;
					if (flag12)
					{
						backUpBoneData2.position = transform.localPosition;
						backUpBoneData2.changedPos = true;
					}
					transform.localPosition = transformData.position;
					bool flag13 = !backUpBoneData2.changedRot;
					if (flag13)
					{
						backUpBoneData2.rotation = transform.localRotation;
						backUpBoneData2.changedRot = true;
					}
					transform.localRotation = transformData.rotation;
					bool flag14 = !backUpBoneData2.changedScl;
					if (flag14)
					{
						backUpBoneData2.scale = transform.localScale;
						backUpBoneData2.changedScl = true;
					}
					transform.localScale = transformData.scale;
				}
			}
		}

		public static readonly string directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Config\\PartsEdit\\";

		public static SortedDictionary<string, PresetFileData> presetFileDataDic = null;

		private static FieldInfo fi_m_bEnable = Helper.GetFieldInfo(typeof(TBoneHair_), "m_bEnable");

		private static HashSet<Transform> trsHash = null;
	}
}
