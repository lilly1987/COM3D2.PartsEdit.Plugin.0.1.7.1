using System;
using System.Collections.Generic;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class MaidSelectUI
	{
		public void Draw()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("メイド選択", new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			bool flag = this.combo == null;
			if (flag)
			{
				this.ResetMaidList();
			}
			else
			{
				bool flag2 = this.maidObserver.CheckActiveChange();
				if (flag2)
				{
					this.ResetMaidList();
				}
			}
			int num = this.combo.ShowScroll(GUILayout.ExpandWidth(false));
			bool flag3 = num == 0;
			if (flag3)
			{
				this.selectedMaid = null;
				CommonUIData.maid = null;
			}
			else
			{
				bool flag4 = num > 0;
				if (flag4)
				{
					this.selectedMaid = this.activeMaidList[num - 1];
					CommonUIData.maid = this.selectedMaid;
				}
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}

		public void DrawCombo()
		{
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			bool flag = this.combo == null;
			if (flag)
			{
				this.ResetMaidList();
			}
			else
			{
				bool flag2 = this.maidObserver.CheckActiveChange();
				if (flag2)
				{
					this.ResetMaidList();
				}
			}
			int num = this.combo.ShowScroll(GUILayout.ExpandWidth(false));
			bool flag3 = num == 0;
			if (flag3)
			{
				this.selectedMaid = null;
				CommonUIData.SetMaid(null);
			}
			else
			{
				bool flag4 = num > 0;
				if (flag4)
				{
					this.selectedMaid = this.activeMaidList[num - 1];
					CommonUIData.SetMaid(this.selectedMaid);
				}
			}
			GUILayout.EndVertical();
		}

		private void ResetMaidList()
		{
			this.maidObserver.UpdateMaidActiveState();
			this.activeMaidList = this.maidObserver.GetActiveMaidList();
			bool flag = this.selectedMaid && this.activeMaidList.Contains(this.selectedMaid);
			string text;
			if (flag)
			{
				text = this.GetMaidFullName(this.selectedMaid);
			}
			else
			{
				this.selectedMaid = null;
				text = "未選択";
			}
			this.maidNameList = new GUIContent[this.activeMaidList.Count + 1];
			this.maidNameList[0] = new GUIContent("未選択");
			for (int i = 0; i < this.activeMaidList.Count; i++)
			{
				this.maidNameList[i + 1] = new GUIContent(this.GetMaidFullName(this.activeMaidList[i]));
			}
			this.combo = new ComboBoxLO(new GUIContent(text), this.maidNameList, UIParams.Instance.bStyle, UIParams.Instance.winStyle, UIParams.Instance.listStyle, false);
		}

		private string GetMaidFullName(Maid maid)
		{
			return maid.status.lastName + " " + maid.status.firstName;
		}

		private GUIContent[] maidNameList = null;

		private ComboBoxLO combo = null;

		private List<Maid> activeMaidList = null;

		private MaidObserver maidObserver = new MaidObserver();

		private Maid selectedMaid = null;
	}
}
