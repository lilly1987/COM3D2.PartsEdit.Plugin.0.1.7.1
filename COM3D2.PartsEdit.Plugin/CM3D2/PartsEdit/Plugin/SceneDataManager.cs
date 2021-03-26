using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class SceneDataManager
	{
		public static XElement GetSceneXmlData()
		{
			XElement xelement = new XElement("SceneData");
			xelement.Add(SceneDataManager.GetMaidListData());
			xelement.Add(SceneDataManager.GetObjectListData());
			return xelement;
		}

		private static XElement GetMaidListData()
		{
			XElement xelement = new XElement("Maids");
			CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
			int maidCount = characterMgr.GetMaidCount();
			for (int i = 0; i < maidCount; i++)
			{
				Maid maid = characterMgr.GetMaid(i);
				bool flag = !maid;
				if (!flag)
				{
					XElement maidData = SceneDataManager.GetMaidData(maid);
					bool flag2 = maidData == null;
					if (!flag2)
					{
						xelement.Add(maidData);
					}
				}
			}
			return xelement;
		}

		private static XElement GetMaidData(Maid maid)
		{
			BackUpMaidData orNullMaidData = BackUpData.GetOrNullMaidData(maid);
			bool flag = orNullMaidData == null;
			XElement result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XElement xelement = new XElement("Maid");
				XElement xelement2 = new XElement("GUID");
				xelement2.Add(maid.status.guid);
				XElement xelement3 = new XElement("Name");
				xelement3.Add(maid.status.fullNameJpStyle);
				xelement.Add(xelement2);
				xelement.Add(xelement3);
				XElement xelement4 = new XElement("Slots");
				foreach (int slotID in orNullMaidData.slotDataDic.Keys)
				{
					XElement slotData = SceneDataManager.GetSlotData(maid, slotID);
					bool flag2 = slotData == null;
					if (!flag2)
					{
						xelement4.Add(slotData);
					}
				}
				xelement.Add(xelement4);
				result = xelement;
			}
			return result;
		}

		private static XElement GetSlotData(Maid maid, int slotID)
		{
			bool flag = slotID == -1;
			TBodySkin tbodySkin;
			GameObject gameObject;
			if (flag)
			{
				tbodySkin = maid.body0.goSlot[0];
				gameObject = maid.body0.m_Bones.gameObject;
			}
			else
			{
				tbodySkin = maid.body0.GetSlot(slotID);
				gameObject = tbodySkin.obj;
			}
			bool flag2 = !gameObject;
			XElement result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				BackUpSlotData orNullMaidSlotData = BackUpData.GetOrNullMaidSlotData(maid, slotID);
				bool flag3 = orNullMaidSlotData == null;
				if (flag3)
				{
					result = null;
				}
				else
				{
					bool flag4 = !orNullMaidSlotData.objectDataDic.ContainsKey(gameObject);
					if (flag4)
					{
						result = null;
					}
					else
					{
						XElement xelement = new XElement("Slot");
						XElement xelement2 = new XElement("Category");
						bool flag5 = slotID == -1;
						if (flag5)
						{
							xelement2.Add("base");
						}
						else
						{
							XContainer xcontainer = xelement2;
							TBody.SlotID slotID2 = (TBody.SlotID)slotID;
							xcontainer.Add(slotID2.ToString());
						}
						xelement.Add(xelement2);
						XElement xelement3 = new XElement("ModelFileName");
						xelement3.Add(tbodySkin.m_strModelFileName);
						xelement.Add(xelement3);
						XElement maidObjectData = SceneDataManager.GetMaidObjectData(maid, slotID);
						xelement.Add(maidObjectData);
						result = xelement;
					}
				}
			}
			return result;
		}

		private static XElement GetObjectListData()
		{
			return new XElement("ObjectListData");
		}

		private static XElement GetMaidObjectData(Maid maid, int slotID)
		{
			SceneDataManager.trsHash = new HashSet<Transform>();
			bool flag = false;
			bool flag2 = slotID == -1;
			GameObject gameObject;
			if (flag2)
			{
				TBodySkin tbodySkin = maid.body0.goSlot[0];
				gameObject = maid.body0.m_Bones.gameObject;
			}
			else
			{
				TBodySkin tbodySkin = maid.body0.GetSlot(slotID);
				gameObject = tbodySkin.obj;
			}
			bool flag3 = !gameObject;
			XElement result;
			if (flag3)
			{
				result = null;
			}
			else
			{
				BackUpSlotData orNullMaidSlotData = BackUpData.GetOrNullMaidSlotData(maid, slotID);
				bool flag4 = orNullMaidSlotData == null;
				if (flag4)
				{
					result = null;
				}
				else
				{
					bool flag5 = !orNullMaidSlotData.objectDataDic.ContainsKey(gameObject);
					if (flag5)
					{
						result = null;
					}
					else
					{
						BackUpObjectData backUpObjectData = orNullMaidSlotData.objectDataDic[gameObject];
						XElement xelement = new XElement("ObjectData");
						bool changedYure = backUpObjectData.changedYure;
						if (changedYure)
						{
							XElement xelement2 = new XElement("Yure");
							bool yureState = YureUtil.GetYureState(maid, slotID);
							xelement2.Add(yureState);
							xelement.Add(xelement2);
							bool flag6 = !yureState;
							if (flag6)
							{
								flag = true;
							}
						}
						bool flag7 = slotID == -1;
						Transform rootBone;
						Transform[] array;
						if (flag7)
						{
							SkinnedMeshRenderer componentInChildren = maid.body0.goSlot[0].obj.GetComponentInChildren<SkinnedMeshRenderer>();
							rootBone = maid.body0.m_Bones.transform;
							array = (from bone in componentInChildren.bones
							where bone != null
							select CMT.SearchObjName(rootBone, bone.name, true) into bone
							where bone != null
							select bone).ToArray<Transform>();
						}
						else
						{
							rootBone = maid.body0.goSlot[slotID].obj_tr;
							SkinnedMeshRenderer componentInChildren2 = rootBone.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
							array = (from bone in componentInChildren2.bones
							where bone != null
							select bone).ToArray<Transform>();
						}
						foreach (Transform transform in array)
						{
							Transform transform2 = transform;
							while (transform2 != rootBone)
							{
								bool flag8 = !transform2;
								if (flag8)
								{
									Debug.Log("ルートオブジェクト配下にありません:" + transform.name);
									return null;
								}
								bool flag9 = SceneDataManager.trsHash.Contains(transform2);
								if (flag9)
								{
									break;
								}
								bool flag10 = flag || BackUpData.GetOrNullMaidBoneData(maid, slotID, gameObject, transform2) != null;
								if (flag10)
								{
									XElement xelement3 = new XElement("TransformData");
									XElement xelement4 = new XElement("Name");
									xelement4.Add(transform2.name);
									xelement3.Add(xelement4);
									xelement3.Add(SceneDataManager.GetVector3Data("Scale", transform2.localScale));
									xelement3.Add(SceneDataManager.GetVector3Data("Position", transform2.localPosition));
									xelement3.Add(SceneDataManager.GetVector3Data("Rotation", transform2.localEulerAngles));
									xelement.Add(xelement3);
								}
								SceneDataManager.trsHash.Add(transform2);
								transform2 = transform2.parent;
							}
						}
						result = xelement;
					}
				}
			}
			return result;
		}

		private static XElement GetVector3Data(string str, Vector3 vec)
		{
			XElement xelement = new XElement(str);
			XElement xelement2 = new XElement("x");
			xelement2.Add(vec.x);
			xelement.Add(xelement2);
			XElement xelement3 = new XElement("y");
			xelement3.Add(vec.y);
			xelement.Add(xelement3);
			XElement xelement4 = new XElement("z");
			xelement4.Add(vec.z);
			xelement.Add(xelement4);
			return xelement;
		}

		public static void SetSceneXmlData(XElement xmlData)
		{
			BackUpData.Refresh();
			XElement xelement = xmlData.Element("Maids");
			bool flag = xelement == null;
			if (!flag)
			{
				SceneDataManager.SetMaidListData(xelement);
			}
		}

		private static void SetMaidListData(XElement maidListXml)
		{
			IEnumerable<XElement> enumerable = maidListXml.Elements("Maid");
			bool flag = enumerable == null;
			if (!flag)
			{
				foreach (XElement maidData in enumerable)
				{
					SceneDataManager.SetMaidData(maidData);
				}
			}
		}

		private static void SetMaidData(XElement maidXml)
		{
			XElement xelement = maidXml.Element("GUID");
			bool flag = xelement == null;
			if (!flag)
			{
				Maid maid = GameMain.Instance.CharacterMgr.GetMaid((string)xelement);
				bool flag2 = !maid;
				if (!flag2)
				{
					XElement xelement2 = maidXml.Element("Slots");
					bool flag3 = xelement2 == null;
					if (!flag3)
					{
						IEnumerable<XElement> enumerable = xelement2.Elements("Slot");
						bool flag4 = enumerable == null;
						if (!flag4)
						{
							foreach (XElement slotXml in enumerable)
							{
								SceneDataManager.SetSlotData(maid, slotXml);
							}
						}
					}
				}
			}
		}

		private static void SetSlotData(Maid maid, XElement slotXml)
		{
			XElement xelement = slotXml.Element("Category");
			bool flag = xelement == null;
			if (!flag)
			{
				int categoryID = SceneDataManager.GetCategoryID((string)xelement);
				bool flag2 = categoryID == -2;
				if (!flag2)
				{
					XElement xelement2 = slotXml.Element("ModelFileName");
					bool flag3 = xelement2 == null;
					if (!flag3)
					{
						string text = (string)xelement2;
						bool flag4 = categoryID == -1;
						TBodySkin tbodySkin;
						if (flag4)
						{
							tbodySkin = maid.body0.goSlot[0];
						}
						else
						{
							tbodySkin = maid.body0.GetSlot(categoryID);
						}
						bool flag5 = tbodySkin.m_strModelFileName != text;
						if (flag5)
						{
							Debug.Log("PartsEdit:" + (string)xelement + "カテゴリのmodelファイルがセーブ時と変わっているため、ロードされません");
							Debug.Log(text + "→" + tbodySkin.m_strModelFileName);
						}
						else
						{
							XElement xelement3 = slotXml.Element("ObjectData");
							bool flag6 = xelement3 == null;
							if (!flag6)
							{
								SceneDataManager.SetMaidObjectData(maid, categoryID, xelement3);
							}
						}
					}
				}
			}
		}

		private static void SetMaidObjectData(Maid maid, int slotID, XElement objectXml)
		{
			bool flag = slotID == -1;
			GameObject gameObject;
			if (flag)
			{
				TBodySkin tbodySkin = maid.body0.goSlot[0];
				gameObject = maid.body0.m_Bones.gameObject;
			}
			else
			{
				TBodySkin tbodySkin = maid.body0.GetSlot(slotID);
				gameObject = tbodySkin.obj;
			}
			XElement xelement = objectXml.Element("Yure");
			bool flag2 = xelement != null;
			if (flag2)
			{
				bool flag3 = (bool)xelement;
				bool yureAble = YureUtil.GetYureAble(maid, slotID);
				if (yureAble)
				{
					bool flag4 = YureUtil.GetYureState(maid, slotID) != flag3;
					if (flag4)
					{
						BackUpObjectData orAddMaidObjectData = BackUpData.GetOrAddMaidObjectData(maid, slotID, gameObject);
						bool changedYure = orAddMaidObjectData.changedYure;
						if (changedYure)
						{
							orAddMaidObjectData.changedYure = false;
							orAddMaidObjectData.bYure = true;
						}
						else
						{
							orAddMaidObjectData.changedYure = true;
							orAddMaidObjectData.bYure = YureUtil.GetYureState(maid, slotID);
						}
						YureUtil.SetYureState(maid, slotID, flag3);
					}
				}
			}
			IEnumerable<XElement> enumerable = objectXml.Elements("TransformData");
			bool flag5 = enumerable != null;
			if (flag5)
			{
				foreach (XElement xelement2 in enumerable)
				{
					XElement xelement3 = xelement2.Element("Name");
					bool flag6 = xelement3 == null;
					if (flag6)
					{
						break;
					}
					bool flag7 = slotID == -1;
					Transform transform;
					if (flag7)
					{
						transform = CMT.SearchObjName(gameObject.transform, (string)xelement3, true);
					}
					else
					{
						transform = CMT.SearchObjName(gameObject.transform, (string)xelement3, false);
					}
					bool flag8 = transform == null;
					if (!flag8)
					{
						BackUpBoneData orAddMaidBoneData = BackUpData.GetOrAddMaidBoneData(maid, slotID, gameObject, transform);
						XElement xelement4 = xelement2.Element("Scale");
						bool flag9 = xelement4 != null;
						if (flag9)
						{
							bool flag10 = !orAddMaidBoneData.changedScl;
							if (flag10)
							{
								orAddMaidBoneData.changedScl = true;
								orAddMaidBoneData.scale = transform.localScale;
							}
							Vector3 vectorData = SceneDataManager.GetVectorData(xelement4);
							transform.localScale = vectorData;
						}
						XElement xelement5 = xelement2.Element("Position");
						bool flag11 = xelement5 != null;
						if (flag11)
						{
							bool flag12 = !orAddMaidBoneData.changedPos;
							if (flag12)
							{
								orAddMaidBoneData.changedPos = true;
								orAddMaidBoneData.position = transform.localPosition;
							}
							Vector3 vectorData2 = SceneDataManager.GetVectorData(xelement5);
							transform.localPosition = vectorData2;
						}
						XElement xelement6 = xelement2.Element("Rotation");
						bool flag13 = xelement6 != null;
						if (flag13)
						{
							bool flag14 = !orAddMaidBoneData.changedRot;
							if (flag14)
							{
								orAddMaidBoneData.changedRot = true;
								orAddMaidBoneData.rotation = transform.localRotation;
							}
							Vector3 vectorData3 = SceneDataManager.GetVectorData(xelement6);
							transform.localEulerAngles = vectorData3;
						}
					}
				}
			}
		}

		private static Vector3 GetVectorData(XElement vecXml)
		{
			XElement element = vecXml.Element("x");
			XElement element2 = vecXml.Element("y");
			XElement element3 = vecXml.Element("z");
			return new Vector3((float)element, (float)element2, (float)element3);
		}

		private static int GetCategoryID(string categoryName)
		{
			bool flag = categoryName == "base";
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				try
				{
					result = (int)Enum.Parse(typeof(TBody.SlotID), categoryName, true);
				}
				catch (OverflowException ex)
				{
					result = -2;
				}
			}
			return result;
		}

		private static HashSet<Transform> trsHash = null;
	}
}
