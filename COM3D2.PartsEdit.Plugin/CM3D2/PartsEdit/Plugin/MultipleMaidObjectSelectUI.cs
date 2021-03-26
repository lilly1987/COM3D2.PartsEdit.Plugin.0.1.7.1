using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CM3D2.PartsEdit.Plugin
{
	internal class MultipleMaidObjectSelectUI
	{
		public void Draw()
		{
			bool flag = this.objectNameList == null;
			if (flag)
			{
				this.ResetObjectList();
			}
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			bool flag2 = GUILayout.Button("オブジェクト一覧取得", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag2)
			{
				this.ResetObjectList();
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("オブジェクト選択", new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			int num = this.combo.ShowScroll(GUILayout.ExpandWidth(false));
			bool flag3 = num == 0;
			if (flag3)
			{
				this.selectedObject = null;
			}
			else
			{
				bool flag4 = num > 0;
				if (flag4)
				{
					this.selectedObject = this.objectList[num - 1];
				}
			}
			CommonUIData.obj = this.selectedObject;
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}

		public void DrawListReloadButton()
		{
			bool flag = this.objectNameList == null;
			if (flag)
			{
				this.ResetObjectList();
			}
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			bool flag2 = GUILayout.Button("オブジェクト一覧取得", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag2)
			{
				this.ResetObjectList();
			}
			GUILayout.EndHorizontal();
		}

		public void DrawCombo()
		{
			bool flag = this.CheckObjectChange();
			if (flag)
			{
				this.ResetObjectList();
			}
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			int num = this.combo.ShowScroll(GUILayout.ExpandWidth(false));
			bool flag2 = num == 0;
			if (flag2)
			{
				this.selectedObject = null;
			}
			else
			{
				bool flag3 = num > 0;
				if (flag3)
				{
					this.selectedObject = this.objectList[num - 1];
				}
			}
			CommonUIData.obj = this.selectedObject;
			GUILayout.EndVertical();
		}

		private bool CheckObjectChange()
		{
			bool flag = this.objectHash == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				List<GameObject> list = (from go in SceneManager.GetActiveScene().GetRootGameObjects()
				where go.activeSelf && go.name.EndsWith(".menu") && go.GetComponent<Animation>()
				select go into smr
				select smr.gameObject).ToList<GameObject>();
				bool flag2 = list.Count != this.objectHash.Count;
				if (flag2)
				{
					result = true;
				}
				else
				{
					foreach (GameObject item in list)
					{
						bool flag3 = !this.objectHash.Contains(item);
						if (flag3)
						{
							return true;
						}
					}
					result = false;
				}
			}
			return result;
		}

		private void ResetObjectList()
		{
			this.objectList = (from go in SceneManager.GetActiveScene().GetRootGameObjects()
			where go.activeSelf && go.name.EndsWith(".menu") && go.GetComponent<Animation>()
			select go into smr
			select smr.gameObject).ToList<GameObject>();
			this.objectNameList = new GUIContent[this.objectList.Count + 1];
			this.objectNameList[0] = new GUIContent("未選択");
			this.objectHash = new HashSet<GameObject>();
			for (int i = 0; i < this.objectList.Count; i++)
			{
				this.objectNameList[i + 1] = new GUIContent(this.objectList[i].name);
				this.objectHash.Add(this.objectList[i]);
			}
			int num = -1;
			bool flag = this.selectedObject;
			if (flag)
			{
				num = this.objectList.IndexOf(this.selectedObject);
				bool flag2 = num == -1;
				if (flag2)
				{
					this.selectedObject = null;
				}
			}
			bool flag3 = this.selectedObject != CommonUIData.obj;
			if (flag3)
			{
				CommonUIData.obj = this.selectedObject;
				CommonUIData.bone = null;
			}
			this.combo = new ComboBoxLO(this.objectNameList[num + 1], this.objectNameList, UIParams.Instance.bStyle, UIParams.Instance.winStyle, UIParams.Instance.listStyle, false);
		}

		private GUIContent[] objectNameList = null;

		private List<GameObject> objectList = null;

		private ComboBoxLO combo = null;

		private GameObject selectedObject = null;

		private HashSet<GameObject> objectHash = null;
	}
}
