using System;
using UnityEngine;

internal class CopyTransform : MonoBehaviour
{
	private void Update()
	{
		this.FollowTarget();
	}

	public void SetTarget(Transform trs)
	{
		this.targetTrs = trs;
		this.FollowTarget();
	}

	public void FollowTarget()
	{
		bool flag = this.targetTrs;
		if (flag)
		{
			bool flag2 = this.global;
			if (flag2)
			{
				this.FollowTargetGlobal();
			}
			else
			{
				this.FollowTargetLocal();
			}
		}
	}

	private void FollowTargetGlobal()
	{
		bool flag = this.bPos;
		if (flag)
		{
			base.transform.position = this.targetTrs.position;
		}
		bool flag2 = this.bRot;
		if (flag2)
		{
			base.transform.rotation = this.targetTrs.rotation;
		}
		bool flag3 = this.bScl;
		if (flag3)
		{
			base.transform.localScale = Vector3.one;
			base.transform.localScale = base.transform.InverseTransformPoint(this.targetTrs.lossyScale);
		}
	}

	private void FollowTargetLocal()
	{
		bool flag = this.bPos;
		if (flag)
		{
			base.transform.localPosition = this.targetTrs.localPosition;
		}
		bool flag2 = this.bRot;
		if (flag2)
		{
			base.transform.localRotation = this.targetTrs.localRotation;
		}
		bool flag3 = this.bScl;
		if (flag3)
		{
			base.transform.localScale = this.targetTrs.localScale;
		}
	}

	public bool global = true;

	public bool bPos = true;

	public bool bRot = true;

	public bool bScl = true;

	public Transform targetTrs = null;
}
