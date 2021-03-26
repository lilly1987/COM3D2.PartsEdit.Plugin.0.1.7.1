using System;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class ObjectEditUI
	{
		public void Draw()
		{
			GUILayout.Label("操作", UIParams.Instance.lStyle, new GUILayoutOption[0]);
			UIUtil.BeginIndentArea();
			int targetSelectMode = Setting.targetSelectMode;
			if (targetSelectMode != 0)
			{
				if (targetSelectMode == 1)
				{
					this.smoEditUI.Draw();
				}
			}
			else
			{
				this.maidObjectUI.Draw();
			}
			UIUtil.EndoIndentArea();
		}

		private MaidObjectUI maidObjectUI = new MaidObjectUI();

		private SkinnedMeshObjectEditUI smoEditUI = new SkinnedMeshObjectEditUI();
	}
}
