using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class SkinnedMeshObjectSelectUI
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
			bool flag2 = GUILayout.Button("オブジェクト一覧再取得", UIParams.Instance.bStyle, new GUILayoutOption[0]);
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
			bool flag5 = CommonUIData.obj != this.selectedObject;
			if (flag5)
			{
				CommonUIData.obj = this.selectedObject;
				CommonUIData.bone = null;
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}

		private void ResetObjectList()
		{
			this.objectList = (from smr in UnityEngine.Object.FindObjectsOfType<SkinnedMeshRenderer>()
			where smr.gameObject.GetComponentInParent<Maid>() == null
			select smr.gameObject).ToList<GameObject>();
			this.objectNameList = new GUIContent[this.objectList.Count + 1];
			this.objectNameList[0] = new GUIContent("未選択");
			for (int i = 0; i < this.objectList.Count; i++)
			{
				this.objectNameList[i + 1] = new GUIContent(this.objectList[i].name);
			}
			int num = -1;
			bool flag = this.selectedObject;
			if (flag)
			{
				num = this.objectList.IndexOf(this.selectedObject);
			}
			this.combo = new ComboBoxLO(this.objectNameList[num + 1], this.objectNameList, UIParams.Instance.bStyle, UIParams.Instance.winStyle, UIParams.Instance.listStyle, false);
		}

		private GUIContent[] objectNameList = null;

		private List<GameObject> objectList = null;

		private ComboBoxLO combo = null;

		private GameObject selectedObject = null;
	}
}
