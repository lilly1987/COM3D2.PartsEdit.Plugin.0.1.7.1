using System;
using UnityEngine;
using UnityEngine.Events;

namespace CM3D2.PartsEdit.Plugin
{
	internal class PartsEdit : MonoBehaviour
	{
		private void Start()
		{
			this.window = base.gameObject.AddComponent<GearWindowSystem>().GetUIWindow();
			this.window.SetWindowInfo("PartsEdit", PluginInfo.Version);
			this.window.AddDrawEvent(new UnityAction(this.Draw));
			this.finishUI = new FinishUI(this.window);
			this.boneEdit = base.gameObject.AddComponent<BoneEdit>();
		}

		private void Draw()
		{
			bool flag = this.mode != Setting.mode;
			if (flag)
			{
				this.mode = Setting.mode;
				bool flag2 = this.mode == Mode.Import;
				if (flag2)
				{
					this.importUI.Reset();
				}
			}
			switch (Setting.mode)
			{
			case Mode.Edit:
				this.settingUI.Draw();
				GUILayout.Label("", new GUILayoutOption[0]);
				this.targetSelectModeUI.Draw();
				GUILayout.Label("", new GUILayoutOption[0]);
				this.objectEditUI.Draw();
				GUILayout.FlexibleSpace();
				GUILayout.Label("", new GUILayoutOption[0]);
				this.finishUI.Draw();
				break;
			case Mode.Import:
				this.importUI.Draw();
				break;
			case Mode.Export:
				this.exportUI.Draw();
				break;
			case Mode.BoneDisplaySetting:
				this.boneDisplaySettingUI.Draw();
				break;
			case Mode.GizmoSetting:
				this.gizmoSettingUI.Draw();
				break;
			}
		}

		private UIWindow window;

		private SettingUI settingUI = new SettingUI();

		private TargetSelectModeUI targetSelectModeUI = new TargetSelectModeUI();

		private ObjectEditUI objectEditUI = new ObjectEditUI();

		private ImportUI importUI = new ImportUI();

		private ExportUI exportUI = new ExportUI();

		private BoneDisplaySettingUI boneDisplaySettingUI = new BoneDisplaySettingUI();

		private GizmoSettingUI gizmoSettingUI = new GizmoSettingUI();

		private FinishUI finishUI = null;

		private BoneEdit boneEdit;

		private Mode mode = Mode.Edit;
	}
}
