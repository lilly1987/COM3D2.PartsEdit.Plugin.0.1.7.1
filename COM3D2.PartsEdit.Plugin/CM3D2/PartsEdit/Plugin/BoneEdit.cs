using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class BoneEdit : MonoBehaviour
	{
		private void Start()
		{
			this.boneRendererRoot = new GameObject().transform;
			this.boneRendererRoot.parent = base.transform;
			this.boneRendererRoot.name = "BoneRendererRoot";
			this.InitGizmo();
			Setting.normalBoneColor.mDelegate += this.OnChangeNormalBoneColor;
			Setting.selectBoneColor.mDelegate += this.OnChangeSelectBoneColor;
			Setting.bodyBoneDisplay.mDelegate += this.OnChangeBodyBoneDisplay;
		}

		private void Update()
		{
			this.CommonDataChangeCheck();
			if (this.braList != null)
			{
				foreach (BoneRendererAssist boneRendererAssist in this.braList)
				{
					boneRendererAssist.BRAUpdate();
				}
				if (Setting.boneSelectKey != KeyCode.None && Input.GetKey(Setting.boneSelectKey))
				{
					this.bgr.Visible = false;
					if (this.selectable && Input.GetMouseButtonDown(0))
					{
						Ray ray = default(Ray);
						RaycastHit hit = default(RaycastHit);
						ray = Camera.main.ScreenPointToRay(Input.mousePosition);
						int layerMask = 1;
						if (Physics.Raycast(ray.origin, ray.direction, out hit, float.PositiveInfinity, layerMask, QueryTriggerInteraction.Collide) && this.braList.Exists((BoneRendererAssist bra) => bra.transform == hit.collider.transform))
						{
							this.BoneClick(hit.collider.transform);
						}
					}
				}
				else
				{
					this.bgr.Visible = this.bEdit;
				}
				this.bgr.smallMoveKey = Setting.gizmoSmallKey;
				this.bgr.bigMoveKey = Setting.gizmoBigKey;
			}
		}

		private void CopyBoneConstruction(Transform root, Transform[] fromBones)
		{
			this.copyBoneList = new List<Transform>();
			this.fromToBoneDic = new Dictionary<Transform, Transform>();
			this.toFromBoneDic = new Dictionary<Transform, Transform>();
			this.braList = new List<BoneRendererAssist>();
			this.SetRoot(root);
			foreach (Transform fromBone in fromBones)
			{
				this.GetOrSetCopyBone(root, fromBone);
			}
		}

		private void SetRoot(Transform root)
		{
			this.rootTrs = new GameObject().transform;
			this.rootTrs.name = root.name;
			this.copyBoneList.Add(this.rootTrs);
			this.fromToBoneDic.Add(root, this.rootTrs);
			this.toFromBoneDic.Add(this.rootTrs, root);
			this.rootTrs.parent = this.boneRendererRoot;
			CopyTransform copyTransform = this.rootTrs.gameObject.AddComponent<CopyTransform>();
			copyTransform.bScl = false;
			copyTransform.SetTarget(this.toFromBoneDic[this.rootTrs]);
			BoneRendererAssist boneRendererAssist = this.rootTrs.gameObject.AddComponent<BoneRendererAssist>();
			boneRendererAssist.SetSelectable(this.selectable);
			boneRendererAssist.SetVisible(this.boneDisplay == BoneDisplay.Visible);
			boneRendererAssist.SetColor(Setting.normalBoneColor.GetValue());
			this.braList.Add(boneRendererAssist);
		}

		private Transform GetOrSetCopyBone(Transform root, Transform fromBone)
		{
			Transform result;
			if (this.fromToBoneDic.ContainsKey(fromBone))
			{
				result = this.fromToBoneDic[fromBone];
			}
			else if (fromBone.parent == null)
			{
				result = null;
			}
			else
			{
				BoneRendererAssist component;
				if (this.fromToBoneDic.ContainsKey(fromBone.parent))
				{
					component = this.fromToBoneDic[fromBone.parent].GetComponent<BoneRendererAssist>();
					if (this.parentChildDic.ContainsKey(fromBone.name))
					{
						string name = this.parentChildDic[fromBone.name];
						Transform transform2 = CMT.SearchObjName(root.transform, name, false);
						if (transform2)
						{
							component = this.GetOrSetCopyBone(root, transform2).GetComponent<BoneRendererAssist>();
						}
					}
				}
				else
				{
					Transform transform3 = this.GetOrSetCopyBone(root, fromBone.parent);
					if (transform3 == null)
					{
						return null;
					}
					component = transform3.GetComponent<BoneRendererAssist>();
				}
				Transform transform4 = new GameObject().transform;
				transform4.name = fromBone.name;
				this.copyBoneList.Add(transform4);
				this.fromToBoneDic.Add(fromBone, transform4);
				this.toFromBoneDic.Add(transform4, fromBone);
				transform4.parent = this.boneRendererRoot;
				CopyTransform copyTransform = transform4.gameObject.AddComponent<CopyTransform>();
				copyTransform.bScl = false;
				copyTransform.SetTarget(this.toFromBoneDic[transform4]);
				BoneRendererAssist boneRendererAssist = transform4.gameObject.AddComponent<BoneRendererAssist>();
				boneRendererAssist.SetSelectable(this.selectable);
				if (component)
				{
					boneRendererAssist.SetParent(component);
					if (this.firstChildDic.ContainsKey(transform4.name) && this.firstChildDic[transform4.name] == component.transform.name)
					{
						component.SetFirstChild(boneRendererAssist);
					}
					else
					{
						component.SetChild(boneRendererAssist);
					}
				}
				if (this.boneDisplay == BoneDisplay.Visible)
				{
					boneRendererAssist.SetVisible(this.JudgeVisibleBone(boneRendererAssist));
				}
				else
				{
					boneRendererAssist.SetVisible(false);
				}
				boneRendererAssist.SetColor(Setting.normalBoneColor.GetValue());
				this.braList.Add(boneRendererAssist);
				result = transform4;
			}
			return result;
		}

		private bool JudgeVisibleBone(BoneRendererAssist bra)
		{
			return !bra.transform.name.EndsWith("_nub") && (Setting.targetSelectMode != 0 || !this.exclusiveBoneList.Contains(bra.transform.name)) && (Setting.targetSelectMode != 0 || Setting.bodyBoneDisplay.GetValue() != BodyBoneDisplay.Invisible || !BoneUtil.IsBodyBone(bra.transform.name)) && (!bra.transform.name.EndsWith("_SCL_") || !(bra.parent.transform.name == bra.transform.name.Substring(0, bra.transform.name.Length - 5)));
		}

		private void Clear()
		{
			this.fromToBoneDic = null;
			this.toFromBoneDic = null;
			this.copyBoneList = null;
			this.braList = null;
			foreach (object obj in this.boneRendererRoot)
			{
				UnityEngine.Object.Destroy(((Transform)obj).gameObject);
			}
			CommonUIData.bone = null;
			this.bgr.Visible = false;
		}

		private void BoneClick(Transform bone)
		{
			CommonUIData.bone = this.toFromBoneDic[bone];
		}

		private void InitGizmo()
		{
			this.bgr = BoneGizmoRenderer.AddGizmo(base.transform, "CommonLamia");
			this.bgr.Visible = false;
			this.bgr.offsetScale = 0.5f;
			this.bgr.eDragUndo = true;
			this.bgr.dragEndDelegate += this.GizmoDragEnd;
		}

		private void CommonDataChangeCheck()
		{
			try{ this.TargetObjectChangeCheck(); } catch (Exception e) { Debug.Log("CommonDataChangeCheck1 "+e.ToString() ); }
			try { this.TargetBoneChangeCheck();	   }catch(Exception e) { Debug.Log("CommonDataChangeCheck2 "+e.ToString() ); }
			try{ this.BoneDisplayChange();		   }catch(Exception e) { Debug.Log("CommonDataChangeCheck3 "+e.ToString() ); }
			try{ this.GizmoEditChangeCheck();	   }catch(Exception e) { Debug.Log("CommonDataChangeCheck4 "+e.ToString() ); }
			try{ this.CoordinateTypeChangeCheck(); }catch(Exception e) { Debug.Log("CommonDataChangeCheck5 "+e.ToString() ); }
			try { this.GizmoTypeChangeCheck();	   }catch(Exception e) { Debug.Log("CommonDataChangeCheck6 "+e.ToString() ); }
		}

		private void TargetObjectChangeCheck()
		{
			if (Setting.targetSelectMode == 0)
			{
				if (CommonUIData.maid == null || CommonUIData.slotNo == -2)
				{
					CommonUIData.SetObject(null);
				}
				else if (CommonUIData.slotNo == -1)
				{
					CommonUIData.SetObject(CommonUIData.maid.body0.m_Bones);
				}
				else
				{
					CommonUIData.SetObject(CommonUIData.maid.body0.goSlot[CommonUIData.slotNo].obj);
				}
			}
			if (!(this.targetObj == CommonUIData.obj) || (this.targetObj == null && this.braList != null))
			{
				this.targetObj = CommonUIData.obj;
				this.Clear();
				if (this.targetObj)
				{
					Transform[] fromBones;
					if (Setting.targetSelectMode == 0 && CommonUIData.slotNo == -1)
					{
						fromBones = (from bone in CommonUIData.maid.body0.goSlot[0].obj.GetComponentInChildren<SkinnedMeshRenderer>().bones
						where bone != null
						select CMT.SearchObjName(CommonUIData.obj.transform, bone.name, true) into bone
						where bone != null
						select bone).ToArray<Transform>();
					}
					else
					{
						SkinnedMeshRenderer componentInChildren = this.targetObj.GetComponentInChildren<SkinnedMeshRenderer>();
						if (componentInChildren == null)
						{
							return;
						}
						fromBones = (from bone in componentInChildren.bones
						where bone != null
						select bone).ToArray<Transform>();
					}
					this.CopyBoneConstruction(this.targetObj.transform, fromBones);
				}
			}
		}

		private void TargetBoneChangeCheck()
		{
			if (!(this.targetBone == CommonUIData.bone))
			{
				this.targetBone = CommonUIData.bone;
				if (this.targetBone)
				{
					this.bgr.SetTarget(this.targetBone);
					this.bgr.Visible = this.bEdit;
					using (List<BoneRendererAssist>.Enumerator enumerator = this.braList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							BoneRendererAssist boneRendererAssist = enumerator.Current;
							if (this.toFromBoneDic[boneRendererAssist.transform] == this.targetBone)
							{
								boneRendererAssist.SetColor(Setting.selectBoneColor.GetValue());
							}
							else
							{
								boneRendererAssist.SetColor(Setting.normalBoneColor.GetValue());
							}
						}
						return;
					}
				}
				this.bgr.SetTarget(null);
				this.bgr.Visible = false;
			}
		}

		private void GizmoEditChangeCheck()
		{
			if (this.bEdit != Setting.gizmoType > GizmoType.None)
			{
				this.bEdit = (Setting.gizmoType > GizmoType.None);
				this.bgr.Visible = this.bEdit;
			}
		}

		private void CoordinateTypeChangeCheck()
		{
			if (this.coordinateType != Setting.coordinateType)
			{
				this.coordinateType = Setting.coordinateType;
				this.bgr.coordinate = this.coordinateType;
			}
		}

		private void GizmoTypeChangeCheck()
		{
			if (this.editType == Setting.gizmoType)
			{
				return;
			}
			this.editType = Setting.gizmoType;
			this.bgr.eAxis = false;
			this.bgr.eRotate = false;
			this.bgr.eScal = false;
			switch (this.editType)
			{
			case GizmoType.Position:
				this.bgr.eAxis = true;
				return;
			case GizmoType.Rotation:
				this.bgr.eRotate = true;
				return;
			case GizmoType.Scale:
				this.bgr.eScal = true;
				return;
			default:
				return;
			}
		}

		private void BoneDisplayChange()
		{
			if (this.boneDisplay != Setting.boneDisplay)
			{
				this.boneDisplay = Setting.boneDisplay;
				if (this.braList != null)
				{
					switch (this.boneDisplay)
					{
					case BoneDisplay.None:
						this.SetVisible(false);
						this.SetSelectable(false);
						return;
					case BoneDisplay.Visible:
						this.SetVisible(true);
						this.SetSelectable(true);
						return;
					case BoneDisplay.Choisable:
						this.SetVisible(true);
						this.SetSelectable(true);
						return;
					default:
						return;
					}
				}
			}
		}

		private void SetSelectable(bool fSelectable)
		{
			this.selectable = fSelectable;
			foreach (BoneRendererAssist boneRendererAssist in this.braList)
			{
				boneRendererAssist.SetSelectable(this.selectable);
			}
		}

		private void SetVisible(bool fVisible)
		{
			foreach (BoneRendererAssist boneRendererAssist in this.braList)
			{
				if (fVisible)
				{
					boneRendererAssist.SetVisible(this.JudgeVisibleBone(boneRendererAssist));
				}
				else
				{
					boneRendererAssist.SetVisible(false);
				}
			}
		}

		private void GizmoDragEnd()
		{
			int selectedType = this.bgr.GetSelectedType();
			if (selectedType > 0 && selectedType <= 21)
			{
				int targetSelectMode = Setting.targetSelectMode;
				BackUpBoneData backUpBoneData;
				if (targetSelectMode != 0)
				{
					if (targetSelectMode != 1)
					{
						return;
					}
					backUpBoneData = BackUpData.GetOrAddBoneData(CommonUIData.obj, CommonUIData.bone);
				}
				else
				{
					backUpBoneData = BackUpData.GetOrAddMaidBoneData(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, CommonUIData.bone);
				}
				if (selectedType <= 9)
				{
					if (!backUpBoneData.changedPos)
					{
						backUpBoneData.position = this.bgr.GetBackUpLocalPosition();
						backUpBoneData.changedPos = true;
						return;
					}
				}
				else if (selectedType > 9 && selectedType <= 12)
				{
					if (!backUpBoneData.changedRot)
					{
						backUpBoneData.rotation = this.bgr.GetBackUpLocalRotation();
						backUpBoneData.changedRot = true;
						return;
					}
				}
				else if (selectedType > 12 && !backUpBoneData.changedScl)
				{
					backUpBoneData.scale = this.bgr.GetBackUpLocalScale();
					backUpBoneData.changedScl = true;
				}
			}
		}

		private void OnChangeNormalBoneColor(Color color)
		{
			if (this.braList != null)
			{
				foreach (BoneRendererAssist boneRendererAssist in this.braList)
				{
					if (this.toFromBoneDic[boneRendererAssist.transform] != CommonUIData.bone)
					{
						boneRendererAssist.SetColor(color);
					}
				}
			}
		}

		private void OnChangeSelectBoneColor(Color color)
		{
			if (this.braList != null)
			{
				foreach (BoneRendererAssist boneRendererAssist in this.braList)
				{
					if (this.toFromBoneDic[boneRendererAssist.transform] == CommonUIData.bone)
					{
						boneRendererAssist.SetColor(color);
					}
				}
			}
		}

		private void OnChangeBodyBoneDisplay(BodyBoneDisplay bodyBoneDisplay)
		{
			if (this.braList != null)
			{
				foreach (BoneRendererAssist boneRendererAssist in this.braList)
				{
					boneRendererAssist.SetVisible(this.JudgeVisibleBone(boneRendererAssist));
				}
			}
		}

		private Dictionary<Transform, Transform> fromToBoneDic;

		private Dictionary<Transform, Transform> toFromBoneDic;

		private Transform rootTrs;

		private List<Transform> copyBoneList;

		private BoneRendererAssist rootBra;

		private List<BoneRendererAssist> braList;

		private Transform boneRendererRoot;

		private bool visible = true;

		private bool selectable = true;

		private GameObject targetObj;

		private Transform targetBone;

		private BoneDisplay boneDisplay;

		private bool yureEnable;

		private bool bEdit;

		private ExGizmoRenderer.COORDINATE coordinateType;

		private GizmoType editType;

		private BoneGizmoRenderer bgr;

		private List<string> exclusiveBoneList = new List<string>
		{
			"Foretwist_L",
			"Foretwist_R",
			"Foretwist1_L",
			"Foretwist1_R",
			"Hip_L",
			"Hip_L_nub",
			"Hip_R",
			"Hip_R_nub",
			"Kata_L",
			"Kata_L_nub",
			"Kata_R",
			"Kata_R_nub",
			"momoniku_L",
			"momoniku_R",
			"momotwist_L",
			"momotwist_R",
			"momotwist2_L",
			"momotwist2_R",
			"Mune_L",
			"Mune_L_sub",
			"Mune_R",
			"Mune_R_sub",
			"Uppertwist_L",
			"Uppertwist_R",
			"Uppertwist1_L",
			"Uppertwist1_R"
		};

		private Dictionary<string, string> parentChildDic = new Dictionary<string, string>
		{
			{
				"Bip01 Pelvis",
				"Bip01"
			},
			{
				"Bip01 L Thigh",
				"Bip01 Pelvis"
			},
			{
				"Bip01 L Calf",
				"Bip01 L Thigh"
			},
			{
				"Bip01 L Foot",
				"Bip01 L Calf"
			},
			{
				"Bip01 L Toe0",
				"Bip01 L Foot"
			},
			{
				"Bip01 L Toe01",
				"Bip01 L Toe0"
			},
			{
				"Bip01 L Toe0Nub",
				"Bip01 L Toe01"
			},
			{
				"Bip01 L Toe1",
				"Bip01 L Foot"
			},
			{
				"Bip01 L Toe11",
				"Bip01 L Toe1"
			},
			{
				"Bip01 L Toe1Nub",
				"Bip01 L Toe11"
			},
			{
				"Bip01 L Toe2",
				"Bip01 L Foot"
			},
			{
				"Bip01 L Toe21",
				"Bip01 L Toe2"
			},
			{
				"Bip01 L Toe2Nub",
				"Bip01 L Toe21"
			},
			{
				"Bip01 R Thigh",
				"Bip01 Pelvis"
			},
			{
				"Bip01 R Calf",
				"Bip01 R Thigh"
			},
			{
				"Bip01 R Foot",
				"Bip01 R Calf"
			},
			{
				"Bip01 R Toe0",
				"Bip01 R Foot"
			},
			{
				"Bip01 R Toe01",
				"Bip01 R Toe0"
			},
			{
				"Bip01 R Toe0Nub",
				"Bip01 R Toe01"
			},
			{
				"Bip01 R Toe1",
				"Bip01 R Foot"
			},
			{
				"Bip01 R Toe11",
				"Bip01 R Toe1"
			},
			{
				"Bip01 R Toe1Nub",
				"Bip01 R Toe11"
			},
			{
				"Bip01 R Toe2",
				"Bip01 R Foot"
			},
			{
				"Bip01 R Toe21",
				"Bip01 R Toe2"
			},
			{
				"Bip01 R Toe2Nub",
				"Bip01 R Toe21"
			},
			{
				"Bip01 Spine",
				"Bip01"
			},
			{
				"Bip01 Spine0a",
				"Bip01 Spine"
			},
			{
				"Bip01 Spine1",
				"Bip01 Spine0a"
			},
			{
				"Bip01 Spine1a",
				"Bip01 Spine1"
			},
			{
				"Bip01 L Clavicle",
				"Bip01 Spine1a"
			},
			{
				"Bip01 L UpperArm",
				"Bip01 L Clavicle"
			},
			{
				"Bip01 L Forearm",
				"Bip01 L UpperArm"
			},
			{
				"Bip01 L Hand",
				"Bip01 L Forearm"
			},
			{
				"Bip01 L Finger0",
				"Bip01 L Hand"
			},
			{
				"Bip01 L Finger01",
				"Bip01 L Finger0"
			},
			{
				"Bip01 L Finger02",
				"Bip01 L Finger01"
			},
			{
				"Bip01 L Finger0Nub",
				"Bip01 L Finger02"
			},
			{
				"Bip01 L Finger1",
				"Bip01 L Hand"
			},
			{
				"Bip01 L Finger11",
				"Bip01 L Finger1"
			},
			{
				"Bip01 L Finger12",
				"Bip01 L Finger11"
			},
			{
				"Bip01 L Finger1Nub",
				"Bip01 L Finger12"
			},
			{
				"Bip01 L Finger2",
				"Bip01 L Hand"
			},
			{
				"Bip01 L Finger21",
				"Bip01 L Finger2"
			},
			{
				"Bip01 L Finger22",
				"Bip01 L Finger21"
			},
			{
				"Bip01 L Finger2Nub",
				"Bip01 L Finger22"
			},
			{
				"Bip01 L Finger3",
				"Bip01 L Hand"
			},
			{
				"Bip01 L Finger31",
				"Bip01 L Finger3"
			},
			{
				"Bip01 L Finger32",
				"Bip01 L Finger31"
			},
			{
				"Bip01 L Finger3Nub",
				"Bip01 L Finger32"
			},
			{
				"Bip01 L Finger4",
				"Bip01 L Hand"
			},
			{
				"Bip01 L Finger41",
				"Bip01 L Finger4"
			},
			{
				"Bip01 L Finger42",
				"Bip01 L Finger41"
			},
			{
				"Bip01 L Finger4Nub",
				"Bip01 L Finger42"
			},
			{
				"Bip01 Neck",
				"Bip01 Spine1a"
			},
			{
				"Bip01 Head",
				"Bip01 Neck"
			},
			{
				"Bip01 HeadNub",
				"Bip01 Head"
			},
			{
				"Bip01 R Clavicle",
				"Bip01 Spine1a"
			},
			{
				"Bip01 R UpperArm",
				"Bip01 R Clavicle"
			},
			{
				"Bip01 R Forearm",
				"Bip01 R UpperArm"
			},
			{
				"Bip01 R Hand",
				"Bip01 R Forearm"
			},
			{
				"Bip01 R Finger0",
				"Bip01 R Hand"
			},
			{
				"Bip01 R Finger01",
				"Bip01 R Finger0"
			},
			{
				"Bip01 R Finger02",
				"Bip01 R Finger01"
			},
			{
				"Bip01 R Finger0Nub",
				"Bip01 R Finger02"
			},
			{
				"Bip01 R Finger1",
				"Bip01 R Hand"
			},
			{
				"Bip01 R Finger11",
				"Bip01 R Finger1"
			},
			{
				"Bip01 R Finger12",
				"Bip01 R Finger11"
			},
			{
				"Bip01 R Finger1Nub",
				"Bip01 R Finger12"
			},
			{
				"Bip01 R Finger2",
				"Bip01 R Hand"
			},
			{
				"Bip01 R Finger21",
				"Bip01 R Finger2"
			},
			{
				"Bip01 R Finger22",
				"Bip01 R Finger21"
			},
			{
				"Bip01 R Finger2Nub",
				"Bip01 R Finger22"
			},
			{
				"Bip01 R Finger3",
				"Bip01 R Hand"
			},
			{
				"Bip01 R Finger31",
				"Bip01 R Finger3"
			},
			{
				"Bip01 R Finger32",
				"Bip01 R Finger31"
			},
			{
				"Bip01 R Finger3Nub",
				"Bip01 R Finger32"
			},
			{
				"Bip01 R Finger4",
				"Bip01 R Hand"
			},
			{
				"Bip01 R Finger41",
				"Bip01 R Finger4"
			},
			{
				"Bip01 R Finger42",
				"Bip01 R Finger41"
			},
			{
				"Bip01 R Finger4Nub",
				"Bip01 R Finger42"
			}
		};

		private Dictionary<string, string> firstChildDic = new Dictionary<string, string>
		{
			{
				"Bip01 Spine0a",
				"Bip01 Spine"
			},
			{
				"Bip01 Spine1",
				"Bip01 Spine0a"
			},
			{
				"Bip01 Spine1a",
				"Bip01 Spine1"
			},
			{
				"Bip01 Neck",
				"Bip01 Spine1a"
			},
			{
				"Bip01 Head",
				"Bip01 Neck"
			},
			{
				"Bip01 L UpperArm",
				"Bip01 L Clavicle"
			},
			{
				"Bip01 L Forearm",
				"Bip01 L UpperArm"
			},
			{
				"Bip01 L Hand",
				"Bip01 L Forearm"
			},
			{
				"Bip01 L Finger4",
				"Bip01 L Hand"
			},
			{
				"Bip01 R UpperArm",
				"Bip01 R Clavicle"
			},
			{
				"Bip01 R Forearm",
				"Bip01 R UpperArm"
			},
			{
				"Bip01 R Hand",
				"Bip01 R Forearm"
			},
			{
				"Bip01 R Finger4",
				"Bip01 R Hand"
			},
			{
				"Bip01 L Calf",
				"Bip01 L Thigh"
			},
			{
				"Bip01 L Foot",
				"Bip01 L Calf"
			},
			{
				"Bip01 R Calf",
				"Bip01 R Thigh"
			},
			{
				"Bip01 R Foot",
				"Bip01 R Calf"
			}
		};
	}
}
