using System;
using System.Collections.Generic;
using UnityEngine;

internal class BoneRendererAssist : MonoBehaviour
{
	public bool Selectable
	{
		get
		{
			return this.visible && this.selectable;
		}
	}

	public bool IsRoot
	{
		get
		{
			return this.parent == null;
		}
	}

	private LineRenderer BoneRenderer
	{
		get
		{
			bool flag = this.boneRenderer == null;
			if (flag)
			{
				this.boneRenderer = base.gameObject.AddComponent<LineRenderer>();
				this.lineMaterial = this.CreateMaterial();
				this.boneRenderer.materials = new Material[]
				{
					this.lineMaterial
				};
				this.boneRenderer.SetWidth(this.lineWidth, this.lineWidth * 0.2f);
				this.boneRenderer.SetVertexCount(2);
			}
			return this.boneRenderer;
		}
	}

	private CapsuleCollider BoneCollider
	{
		get
		{
			bool flag = this.boneCollider == null;
			if (flag)
			{
				this.boneCollider = base.gameObject.AddComponent<CapsuleCollider>();
				this.boneCollider.direction = 0;
				this.boneCollider.radius = this.lineWidth;
				this.boneCollider.isTrigger = true;
			}
			return this.boneCollider;
		}
	}

	public void BRAUpdate()
	{
		bool flag = !this.IsRoot;
		if (!flag)
		{
			this.UpdateTransform();
			this.UpdatePosition();
		}
	}

	public void AutoSetUp()
	{
		bool flag = base.transform.parent;
		if (flag)
		{
			this.parent = base.transform.parent.GetComponent<BoneRendererAssist>();
		}
		else
		{
			this.parent = null;
		}
		this.children = new List<BoneRendererAssist>();
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			BoneRendererAssist component = transform.GetComponent<BoneRendererAssist>();
			bool flag2 = component != null;
			if (flag2)
			{
				this.children.Add(component);
			}
		}
	}

	public void SetParent(BoneRendererAssist bra)
	{
		this.parent = bra;
	}

	public void SetChild(BoneRendererAssist bra)
	{
		this.children.Add(bra);
	}

	public void SetFirstChild(BoneRendererAssist bra)
	{
		this.children.Insert(0, bra);
	}

	public void UpdatePosition()
	{
		bool flag = this.visible;
		if (flag)
		{
			this.UpdateBoneRendererPosition();
		}
		bool flag2 = this.Selectable;
		if (flag2)
		{
			this.UpdateBoneColliderPosition();
		}
		foreach (BoneRendererAssist boneRendererAssist in this.children)
		{
			boneRendererAssist.UpdatePosition();
		}
	}

	public void UpdateTransform()
	{
		base.gameObject.GetComponent<CopyTransform>().FollowTarget();
		foreach (BoneRendererAssist boneRendererAssist in this.children)
		{
			boneRendererAssist.UpdateTransform();
		}
	}

	public Vector3 GetBoneTailPos()
	{
		return this.boneTailPos;
	}

	public float GetBoneLength()
	{
		return (this.boneTailPos - base.transform.position).magnitude;
	}

	public void SetVisible(bool fVisible)
	{
		this.visible = fVisible;
		bool flag = this.boneRenderer != null;
		if (flag)
		{
			this.boneRenderer.enabled = fVisible;
		}
		bool flag2 = this.boneCollider != null;
		if (flag2)
		{
			this.boneCollider.enabled = (this.selectable && this.visible);
		}
	}

	public void SetSelectable(bool fSelectable)
	{
		this.selectable = fSelectable;
		bool flag = this.boneCollider != null;
		if (flag)
		{
			this.boneCollider.enabled = (this.selectable && this.visible);
		}
	}

	public void SetColor(Color fColor)
	{
		this.lineColor = fColor;
		bool flag = this.boneRenderer != null;
		if (flag)
		{
			this.boneRenderer.material.color = this.lineColor;
		}
	}

	public Vector3 GetColliderCenter()
	{
		bool flag = !this.boneCollider;
		Vector3 result;
		if (flag)
		{
			result = Vector3.zero;
		}
		else
		{
			result = this.boneCollider.center;
		}
		return result;
	}

	public float GetColliderLength()
	{
		bool flag = !this.boneCollider;
		float result;
		if (flag)
		{
			result = 0f;
		}
		else
		{
			result = this.boneCollider.height;
		}
		return result;
	}

	private void UpdateBoneRendererPosition()
	{
		this.BoneRenderer.SetPosition(0, base.transform.position);
		this.boneTailPos = base.transform.position;
		int count = this.children.Count;
		if (count != 0)
		{
			if (count != 1)
			{
				this.boneTailPos = base.transform.position + Vector3.Project(this.children[0].transform.position - base.transform.position, base.transform.right);
			}
			else
			{
				this.boneTailPos = base.transform.position + Vector3.Project(this.children[0].transform.position - base.transform.position, base.transform.right);
			}
		}
		bool flag = this.boneTailPos == base.transform.position;
		if (flag)
		{
			bool flag2 = this.parent;
			if (flag2)
			{
				this.boneTailPos = base.transform.TransformPoint(-this.parent.GetBoneLength(), 0f, 0f);
			}
			else
			{
				this.boneTailPos = base.transform.TransformPoint(-BoneRendererAssist.maxLength, 0f, 0f);
			}
		}
		else
		{
			bool flag3 = (base.transform.position - this.boneTailPos).magnitude < BoneRendererAssist.minLength;
			if (flag3)
			{
				this.boneTailPos = base.transform.TransformPoint(-BoneRendererAssist.minLength, 0f, 0f);
			}
		}
		this.BoneRenderer.SetPosition(1, this.boneTailPos);
	}

	private void UpdateBoneColliderPosition()
	{
		this.BoneCollider.center = base.transform.InverseTransformPoint(this.boneTailPos) / 2f;
		this.BoneCollider.height = Mathf.Abs(base.transform.InverseTransformPoint(this.boneTailPos).x);
	}

	private Material CreateMaterial()
	{
		Shader shader = Shader.Find("Hidden/Internal-Colored");
		Material material = new Material(shader)
		{
			hideFlags = HideFlags.HideAndDontSave
		};
		material.SetInt("_ZTest", 0);
		material.SetInt("_SrcBlend", 5);
		material.SetInt("_DstBlend", 10);
		material.SetInt("_Cull", 0);
		material.SetInt("_ZWrite", 0);
		material.renderQueue = 5000;
		material.color = this.lineColor;
		return material;
	}

	public bool visible = true;

	public bool selectable = false;

	private Material lineMaterial;

	private Color lineColor = Color.white;

	private readonly float lineWidth = 0.006f;

	private LineRenderer boneRenderer = null;

	private CapsuleCollider boneCollider = null;

	public BoneRendererAssist parent = null;

	public List<BoneRendererAssist> children = new List<BoneRendererAssist>();

	private Vector3 boneTailPos = Vector3.zero;

	private static float minLength = 0.01f;

	private static float maxLength = 0.1f;
}
