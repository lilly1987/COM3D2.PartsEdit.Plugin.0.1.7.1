using System;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class ImportUI
	{
		public ImportUI()
		{
			this.InitCategorySelect();
		}

		private void InitCategorySelect()
		{
			int num = (TBody.m_strDefSlotName.Length - 1) / 3;
			this.cateNameList = new string[num];
			this.cateComboList = new GUIContent[num];
			for (int i = 0; i < num; i++)
			{
				this.cateNameList[i] = TBody.m_strDefSlotName[i * 3];
				this.cateComboList[i] = new GUIContent(TBody.m_strDefSlotName[i * 3]);
			}
			this.cateCombo = new ComboBoxLO(this.cateComboList[0], this.cateComboList, UIParams.Instance.bStyle, UIParams.Instance.winStyle, UIParams.Instance.listStyle, false);
			this.ResetCategorySelect();
		}

		private void ResetCategorySelect()
		{
			switch (this.cateSelectNum)
			{
			case 0:
				this.selectedCategory = null;
				break;
			case 1:
			{
				bool flag = Setting.targetSelectMode == 1;
				if (flag)
				{
					this.selectedCategory = null;
				}
				else
				{
					bool flag2 = CommonUIData.slotNo == -1;
					if (flag2)
					{
						this.selectedCategory = "base";
					}
					else
					{
						this.selectedCategory = this.cateNameList[CommonUIData.slotNo];
					}
				}
				break;
			}
			case 2:
				this.selectedCategory = this.cateNameList[this.cateComboNum];
				break;
			}
			this.ResetFileList();
		}

		private void ResetObjectNameSelect()
		{
			int num = this.objectNameSelectNum;
			if (num != 0)
			{
				if (num == 1)
				{
					this.objectName = CommonUIData.obj.name;
				}
			}
			else
			{
				this.objectName = null;
			}
			this.ResetFileList();
		}

		private void ResetObjectName()
		{
			bool flag = this.objectNameSelectNum == 0;
			if (flag)
			{
				this.objectName = null;
			}
			else
			{
				this.objectName = CommonUIData.obj.name;
			}
		}

		public void Draw()
		{
			this.DrawPresetFile();
			GUILayout.FlexibleSpace();
			this.DrawBackButton();
		}

		private void DrawPresetFile()
		{
			GUILayout.Label("プリセットファイル", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			UIUtil.BeginIndentArea();
			this.DrawNarrowing();
			this.DrawFileList();
			this.DrawImportButton();
			UIUtil.EndoIndentArea();
		}

		private void DrawNarrowing()
		{
			GUILayout.Label("絞り込み", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			UIUtil.BeginIndentArea();
			this.DrawCategoryNarrow();
			this.DrawObjectNameNarrow();
			UIUtil.EndoIndentArea();
		}

		private void DrawCategoryNarrow()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("カテゴリー", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			int num = GUILayout.Toolbar(this.cateSelectNum, this.cateSelectList, UIParams.Instance.tStyle, new GUILayoutOption[0]);
			bool flag = this.cateSelectNum != num;
			if (flag)
			{
				this.cateSelectNum = num;
				this.ResetCategorySelect();
			}
			bool flag2 = this.cateSelectNum != 2;
			if (flag2)
			{
				GUI.enabled = false;
			}
			int num2 = this.cateCombo.ShowScroll(GUILayout.ExpandHeight(false));
			bool flag3 = this.cateComboNum != num2;
			if (flag3)
			{
				this.ResetCategorySelect();
			}
			GUI.enabled = true;
			GUILayout.EndHorizontal();
		}

		private void DrawObjectNameNarrow()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("オブジェクト名", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			int num = GUILayout.Toolbar(this.objectNameSelectNum, this.objectNameSelectList, UIParams.Instance.tStyle, new GUILayoutOption[0]);
			bool flag = this.objectNameSelectNum != num;
			if (flag)
			{
				this.objectNameSelectNum = num;
				this.ResetObjectNameSelect();
			}
			GUILayout.EndHorizontal();
		}

		private void DrawFileList()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("ファイル選択", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			this.index = this.combo.ShowScroll(GUILayout.ExpandHeight(false));
			GUILayout.EndHorizontal();
		}

		private void DrawImportButton()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			bool flag = this.index <= 0;
			if (flag)
			{
				GUI.enabled = false;
			}
			bool flag2 = GUILayout.Button("Import", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag2)
			{
				PresetManager.LoadObjectData(this.fileNameList[this.index - 1]);
				Setting.mode = Mode.Edit;
			}
			GUI.enabled = true;
			GUILayout.EndHorizontal();
		}

		private void DrawBackButton()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			bool flag = GUILayout.Button("戻る", UIParams.Instance.bStyle, new GUILayoutOption[0]);
			if (flag)
			{
				Setting.mode = Mode.Edit;
			}
			GUILayout.EndHorizontal();
		}

		public void ResetFileList()
		{
			this.index = 0;
			this.fileNameList = PresetManager.GetFileList(this.selectedCategory, this.objectName);
			this.contentList = new GUIContent[this.fileNameList.Length + 1];
			this.contentList[0] = new GUIContent("未選択");
			for (int i = 0; i < this.fileNameList.Length; i++)
			{
				this.contentList[i + 1] = new GUIContent(this.fileNameList[i]);
			}
			this.combo = new ComboBoxLO(this.contentList[0], this.contentList, UIParams.Instance.bStyle, UIParams.Instance.winStyle, UIParams.Instance.listStyle, false);
		}

		public void Reset()
		{
			this.cateSelectNum = 0;
			this.objectNameSelectNum = 0;
			this.ResetCategorySelect();
			this.ResetObjectName();
			this.ResetObjectNameSelect();
		}

		private int index = 0;

		private string[] fileNameList = null;

		private GUIContent[] contentList = null;

		private ComboBoxLO combo = null;

		private string selectedCategory = null;

		private string objectName = null;

		private int cateSelectNum = 0;

		private GUIContent[] cateSelectList = new GUIContent[]
		{
			new GUIContent("無し"),
			new GUIContent("同カテ"),
			new GUIContent("選択")
		};

		private int cateComboNum = 0;

		private string[] cateNameList = null;

		private GUIContent[] cateComboList = null;

		private ComboBoxLO cateCombo = null;

		private int objectNameSelectNum = 0;

		private GUIContent[] objectNameSelectList = new GUIContent[]
		{
			new GUIContent("無し"),
			new GUIContent("同名")
		};
	}
}
