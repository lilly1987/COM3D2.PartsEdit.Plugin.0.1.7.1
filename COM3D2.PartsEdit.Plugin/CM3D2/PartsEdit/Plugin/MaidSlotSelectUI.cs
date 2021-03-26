using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class MaidSlotSelectUI
	{
		public void Draw()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("スロット選択", new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			bool flag = this.combo == null;
			if (flag)
			{
				this.ResetSlotList();
			}
			else
			{
				bool flag2 = this.maid != CommonUIData.maid || (this.maidNotNull && !this.maid);
				if (flag2)
				{
					this.ResetSlotList();
				}
				else
				{
					bool flag3 = this.CheckActiveSlotChange();
					if (flag3)
					{
						this.ResetSlotList();
					}
				}
			}
			int num = this.combo.ShowScroll(GUILayout.ExpandWidth(false));
			bool flag4 = num == 0 || num == 1;
			if (flag4)
			{
				this.selectSlotId = num - 2;
			}
			else
			{
				bool flag5 = num > 1;
				if (flag5)
				{
					this.selectSlotId = this.activeSlotIdList[num - 2];
				}
			}
			CommonUIData.slotNo = this.selectSlotId;
			CommonUIData.obj = this.GetSlotObject();
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}

		public void DrawCombo()
		{
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			bool flag = this.combo == null;
			if (flag)
			{
				this.ResetSlotList();
			}
			else
			{
				bool flag2 = this.maid != CommonUIData.maid || (this.maidNotNull && !this.maid);
				if (flag2)
				{
					this.selectSlotId = -2;
					this.ResetSlotList();
				}
				else
				{
					bool flag3 = this.CheckActiveSlotChange();
					if (flag3)
					{
						this.ResetSlotList();
					}
				}
			}
			int num = this.combo.ShowScroll(GUILayout.ExpandWidth(false));
			bool flag4 = num == 0 || num == 1;
			if (flag4)
			{
				this.selectSlotId = num - 2;
			}
			else
			{
				bool flag5 = num > 1;
				if (flag5)
				{
					this.selectSlotId = this.activeSlotIdList[num - 2];
				}
			}
			bool flag6 = CommonUIData.slotNo != this.selectSlotId;
			if (flag6)
			{
				CommonUIData.slotNo = this.selectSlotId;
				CommonUIData.obj = this.GetSlotObject();
				CommonUIData.bone = null;
			}
			GUILayout.EndVertical();
		}

		public void SetMaidGuid(string guid)
		{
			bool flag = this.maidGuid == guid;
			if (!flag)
			{
				this.maidGuid = guid;
				this.maid = GameMain.Instance.CharacterMgr.GetMaid(guid);
				this.ResetSlotList();
			}
		}

		private void ResetSlotList()
		{
			this.maid = CommonUIData.maid;
			this.maidNotNull = (this.maid != null);
			this.activeSlotIdList = this.GetActiveSlotList();
			bool flag = CommonUIData.maid && this.selectSlotId == -1;
			string text;
			if (flag)
			{
				text = "ベース";
			}
			else
			{
				bool flag2 = CommonUIData.maid && this.activeSlotIdList.Contains(this.selectSlotId);
				if (flag2)
				{
					text = this.maid.body0.goSlot[this.selectSlotId].Category;
				}
				else
				{
					this.selectSlotId = -2;
					CommonUIData.slotNo = -2;
					CommonUIData.obj = null;
					text = "未選択";
				}
			}
			this.activeSlotHash = new HashSet<int>();
			this.activeSlotIdList.ForEach(delegate(int slot)
			{
				this.activeSlotHash.Add(slot);
			});
			bool flag3 = CommonUIData.maid == null;
			GUIContent[] array;
			if (flag3)
			{
				array = new GUIContent[]
				{
					new GUIContent("未選択")
				};
			}
			else
			{
				array = new GUIContent[this.activeSlotIdList.Count + 2];
				array[0] = new GUIContent("未選択");
				array[1] = new GUIContent("ベース");
				for (int i = 0; i < this.activeSlotIdList.Count; i++)
				{
					array[i + 2] = new GUIContent(CommonUIData.maid.body0.goSlot[this.activeSlotIdList[i]].Category);
				}
			}
			this.combo = new ComboBoxLO(new GUIContent(text), array, UIParams.Instance.bStyle, UIParams.Instance.winStyle, UIParams.Instance.listStyle, false);
		}

		private bool CheckActiveSlotChange()
		{
			List<int> activeSlotList = this.GetActiveSlotList();
			bool flag = this.activeSlotHash.Count != activeSlotList.Count;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				foreach (int item in activeSlotList)
				{
					bool flag2 = !this.activeSlotHash.Contains(item);
					if (flag2)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		private List<int> GetActiveSlotList()
		{
			bool flag = CommonUIData.maid == null;
			List<int> result;
			if (flag)
			{
				result = new List<int>();
			}
			else
			{
				result = (from slot in CommonUIData.maid.body0.goSlot.GetListParents()
				where this.GetSlotActive(slot)
				select (int)slot.SlotId).ToList<int>();
			}
			return result;
		}

		private bool GetSlotActive(TBodySkin tbs)
		{
			bool flag = tbs == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !tbs.obj;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = !tbs.boVisible;
					result = !flag3;
				}
			}
			return result;
		}

		public GameObject GetSlotObject()
		{
			bool flag = !CommonUIData.maid;
			GameObject result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = !CommonUIData.maid.body0;
				if (flag2)
				{
					result = null;
				}
				else
				{
					bool flag3 = this.selectSlotId == -2;
					if (flag3)
					{
						result = null;
					}
					else
					{
						bool flag4 = this.selectSlotId == -1;
						if (flag4)
						{
							result = CommonUIData.maid.body0.m_Bones.gameObject;
						}
						else
						{
							result = CommonUIData.maid.body0.goSlot[this.selectSlotId].obj;
						}
					}
				}
			}
			return result;
		}

		public int GetSlotNum()
		{
			return this.selectSlotId;
		}

		private ComboBoxLO combo = null;

		private string maidGuid = "";

		private Maid maid = null;

		private bool maidNotNull = false;

		private HashSet<int> activeSlotHash = new HashSet<int>();

		private List<int> activeSlotIdList = new List<int>();

		private List<Maid> activeMaidList = new List<Maid>();

		private int selectSlotId = -2;
	}
}
