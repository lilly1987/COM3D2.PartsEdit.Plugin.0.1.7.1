using System;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class BoneGizmoRenderer : ExGizmoRenderer
	{
		protected override Transform posTargetTrs
		{
			get
			{
				bool flag = this.targetShaftTrs;
				Transform targetTrs;
				if (flag)
				{
					targetTrs = this.targetShaftTrs;
				}
				else
				{
					targetTrs = this.targetTrs;
				}
				return targetTrs;
			}
		}

		protected override Transform rotTargetTrs
		{
			get
			{
				bool flag = this.targetShaftTrs;
				Transform targetTrs;
				if (flag)
				{
					targetTrs = this.targetShaftTrs;
				}
				else
				{
					targetTrs = this.targetTrs;
				}
				return targetTrs;
			}
		}

		protected override Transform sclTargetTrs
		{
			get
			{
				return this.targetTrs;
			}
		}

		public override void SetTarget(Transform t)
		{
			this.targetTrs = t;
			bool flag = t && t.name.EndsWith("_SCL_") && t.parent.name == t.name.Substring(0, t.name.Length - 5);
			if (flag)
			{
				this.targetShaftTrs = t.parent;
			}
			else
			{
				this.targetShaftTrs = null;
			}
		}

		public new static BoneGizmoRenderer AddGizmo(Transform parent_tr, string gizmo_name)
		{
			GameObject gameObject = new GameObject();
			ExGizmoRenderer._gameObjects_.Add(gameObject);
			gameObject.transform.SetParent(parent_tr, false);
			gameObject.name = gizmo_name;
			BoneGizmoRenderer boneGizmoRenderer = gameObject.AddComponent<BoneGizmoRenderer>();
			boneGizmoRenderer.name = gizmo_name + "_GR";
			return boneGizmoRenderer;
		}

		private Transform targetShaftTrs = null;
	}
}
