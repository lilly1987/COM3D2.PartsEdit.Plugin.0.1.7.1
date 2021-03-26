using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

internal class ExGizmoRenderer : GizmoRender
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event ExGizmoRenderer.DragEndDelegate dragEndDelegate = delegate()
	{
	};

	protected virtual Transform posTargetTrs
	{
		get
		{
			return this.targetTrs;
		}
	}

	protected virtual Transform rotTargetTrs
	{
		get
		{
			return this.targetTrs;
		}
	}

	protected virtual Transform sclTargetTrs
	{
		get
		{
			return this.targetTrs;
		}
	}

	protected Vector3 position
	{
		get
		{
			return base.transform.position;
		}
		set
		{
			base.transform.position = value;
		}
	}

	protected Vector3 localPosition
	{
		get
		{
			return base.transform.localPosition;
		}
		set
		{
			base.transform.localPosition = value;
		}
	}

	protected Quaternion rotation
	{
		get
		{
			return base.transform.rotation;
		}
		set
		{
			base.transform.rotation = value;
		}
	}

	protected Quaternion localRotation
	{
		get
		{
			return base.transform.localRotation;
		}
		set
		{
			base.transform.localRotation = value;
		}
	}

	protected Vector3 localScale
	{
		get
		{
			return base.transform.localScale;
		}
		set
		{
			base.transform.localScale = value;
		}
	}

	private void BkupPos()
	{
		this._backup_pos = this.position;
	}

	private void BkupRot()
	{
		this._backup_rot = this.rotation;
	}

	private void BkupScl()
	{
		this._backup_local_scl = this.localScale;
	}

	private void BkupPosAndRot()
	{
		this.BkupPos();
		this.BkupRot();
	}

	private void BkupAll()
	{
		this.BkupPos();
		this.BkupRot();
		this.BkupScl();
	}

	public override void Update()
	{
		bool flag = !this.targetTrs;
		if (!flag)
		{
			this.FrameStateInit();
			bool flag2 = this.dragnow && !this._predrag_state;
			if (flag2)
			{
				this.DragStart();
			}
			else
			{
				bool flag3 = !this.dragnow && this._predrag_state;
				if (flag3)
				{
					bool flag4 = this.dragMove;
					if (flag4)
					{
						this.DragEnd();
					}
				}
			}
			bool isDragUndo = this.isDragUndo;
			if (isDragUndo)
			{
				this.DragCancel();
			}
			base.Update();
			bool flag5 = this.dragnow;
			if (flag5)
			{
				this.TargetToGizmo();
			}
			else
			{
				this.GizmoToTarget();
			}
			this.FrameStateFinish();
		}
	}

	public override void OnRenderObject()
	{
		bool flag = !this.targetTrs;
		if (flag)
		{
			bool flag2 = (int)this._fi_beSelectedType.GetValue(this) != 0 && (bool)this._fi_local_control_lock_.GetValue(this);
			if (flag2)
			{
				this._fi_local_control_lock_.SetValue(this, false);
			}
			this.ClearSelectedType();
		}
		else
		{
			base.OnRenderObject();
		}
	}

	protected virtual void FrameStateInit()
	{
		this.dragnow = this.isDrag;
		this.deltaPos = this.position - this._backup_pos;
		this.deltaRot = this.rotation * Quaternion.Inverse(this._backup_rot);
		this.deltaLocalPos = base.transform.InverseTransformVector(this.deltaPos);
		this.deltaLocalRot = Quaternion.Inverse(this._backup_rot) * this.rotation;
		this.deltaLocalScl = this.localScale - this._backup_local_scl;
	}

	protected virtual void FrameStateFinish()
	{
		this._predrag_state = this.dragnow;
		this.BkupAll();
	}

	protected virtual void DragStart()
	{
		this._predrag_pos = this.position;
		this._predrag_rot = this.rotation;
		bool flag = this.targetTrs;
		if (flag)
		{
			this._predrag_local_pos = this.posTargetTrs.localPosition;
			this._predrag_local_rot = this.rotTargetTrs.localRotation;
			this._predrag_local_scl = this.sclTargetTrs.localScale;
		}
		this.dragMove = false;
		this.selectedType = (int)this._fi_beSelectedType.GetValue(this);
	}

	private void DragEnd()
	{
		this.dragEndDelegate();
		this.selectedType = 0;
	}

	protected virtual void DragCancel()
	{
		bool flag = this.targetTrs;
		if (flag)
		{
			this.posTargetTrs.localPosition = this._predrag_local_pos;
			this.rotTargetTrs.localRotation = this._predrag_local_rot;
			this.sclTargetTrs.localScale = this._predrag_local_scl;
			this.GizmoToTarget();
		}
		else
		{
			this.position = this._predrag_pos;
			this.rotation = this._predrag_rot;
			base.transform.localScale = Vector3.one;
		}
		this._fi.SetValue(null, false);
		this._fi_local_control_lock_.SetValue(null, false);
		this.ClearSelectedType();
		this.dragnow = false;
		this.dragMove = false;
	}

	protected virtual void TargetToGizmo()
	{
		float d = 1f;
		bool flag = this.smallMoveKey != KeyCode.None && Input.GetKey(this.smallMoveKey);
		if (flag)
		{
			d = 0.1f;
		}
		else
		{
			bool flag2 = this.bigMoveKey != KeyCode.None && Input.GetKey(this.bigMoveKey);
			if (flag2)
			{
				d = 10f;
			}
		}
		switch (this.coordinate)
		{
		case ExGizmoRenderer.COORDINATE.Local:
		{
			this.posTargetTrs.position += this.posTargetTrs.TransformVector(this.deltaLocalPos).normalized * this.deltaLocalPos.magnitude * d;
			this.rotTargetTrs.rotation = this.rotTargetTrs.rotation * this.deltaLocalRot;
			this.sclTargetTrs.localScale += this.deltaLocalScl * d;
			bool flag3 = this.deltaLocalPos != Vector3.zero || this.deltaLocalRot != Quaternion.identity || this.deltaLocalScl != Vector3.zero;
			if (flag3)
			{
				this.dragMove = true;
			}
			break;
		}
		case ExGizmoRenderer.COORDINATE.World:
		{
			this.posTargetTrs.position += this.deltaPos * d;
			this.rotTargetTrs.rotation = this.deltaRot * this.rotTargetTrs.rotation;
			bool flag4 = this.deltaPos != Vector3.zero || this.deltaRot != Quaternion.identity;
			if (flag4)
			{
				this.dragMove = true;
			}
			break;
		}
		case ExGizmoRenderer.COORDINATE.View:
		{
			this.posTargetTrs.position += this.deltaPos * d;
			this.rotTargetTrs.rotation = this.deltaRot * this.rotTargetTrs.rotation;
			bool flag5 = this.deltaPos != Vector3.zero || this.deltaRot != Quaternion.identity;
			if (flag5)
			{
				this.dragMove = true;
			}
			break;
		}
		}
	}

	private void GizmoToTarget()
	{
		switch (this.coordinate)
		{
		case ExGizmoRenderer.COORDINATE.Local:
			this.position = this.posTargetTrs.position;
			this.rotation = this.rotTargetTrs.rotation;
			base.transform.localScale = Vector3.one;
			break;
		case ExGizmoRenderer.COORDINATE.World:
			this.position = this.posTargetTrs.position;
			this.rotation = Quaternion.identity;
			base.transform.localScale = Vector3.one;
			break;
		case ExGizmoRenderer.COORDINATE.View:
		{
			this.position = this.posTargetTrs.position;
			base.transform.localScale = Vector3.one;
			Transform transform = Camera.main.transform;
			this.rotation = Quaternion.LookRotation(this.position - transform.position, transform.up);
			break;
		}
		}
	}

	private bool isDragUndo
	{
		get
		{
			bool flag = !this._predrag_state || !this.eDragUndo;
			return !flag && (Input.GetMouseButton(1) || Input.GetKey(KeyCode.Escape));
		}
	}

	private bool isDrag
	{
		get
		{
			bool flag = !this.Visible;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this._fi != null && this._fi_beSelectedType != null;
				if (flag2)
				{
					object value = this._fi.GetValue(this);
					bool flag3 = value is bool && (bool)value;
					if (flag3)
					{
						object value2 = this._fi_beSelectedType.GetValue(this);
						bool flag4 = value2 is Enum && (int)value2 != 0;
						if (flag4)
						{
							return true;
						}
					}
				}
				result = false;
			}
			return result;
		}
	}

	private void ClearSelectedType()
	{
		this._fi_beSelectedType.SetValue(this, 0);
	}

	private bool isDragEnd
	{
		get
		{
			bool isDrag = this.isDrag;
			bool flag = isDrag != this._isdrag_bk;
			if (flag)
			{
				this._isdrag_bk = isDrag;
				bool flag2 = !isDrag;
				if (flag2)
				{
					return true;
				}
			}
			return false;
		}
	}

	private void DragBkup()
	{
		this._isdrag_bk = this.isDrag;
	}

	public ExGizmoRenderer()
	{
		bool flag = this._fi_beSelectedType == null;
		if (flag)
		{
			this._fi_beSelectedType = typeof(GizmoRender).GetField("beSelectedType", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic);
		}
		bool flag2 = this._fi == null;
		if (flag2)
		{
			this._fi = typeof(GizmoRender).GetField("is_drag_", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic);
		}
		bool flag3 = this._fi_local_control_lock_ == null;
		if (flag3)
		{
			this._fi_local_control_lock_ = typeof(GizmoRender).GetField("local_control_lock_", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic);
		}
	}

	public virtual void SetTarget(Transform t)
	{
		this.targetTrs = t;
	}

	public void SetVisible(bool fVisible)
	{
		this.Visible = fVisible;
	}

	public void SetCoordinate(ExGizmoRenderer.COORDINATE fCoordinate)
	{
		this.coordinate = fCoordinate;
	}

	public Vector3 GetBackUpLocalPosition()
	{
		return this._predrag_local_pos;
	}

	public Quaternion GetBackUpLocalRotation()
	{
		return this._predrag_local_rot;
	}

	public Vector3 GetBackUpLocalScale()
	{
		return this._predrag_local_scl;
	}

	public int GetSelectedType()
	{
		return this.selectedType;
	}

	public static ExGizmoRenderer AddGizmo(Transform parent_tr, string gizmo_name)
	{
		GameObject gameObject = new GameObject();
		ExGizmoRenderer._gameObjects_.Add(gameObject);
		gameObject.transform.SetParent(parent_tr, false);
		gameObject.name = gizmo_name;
		ExGizmoRenderer exGizmoRenderer = gameObject.AddComponent<ExGizmoRenderer>();
		exGizmoRenderer.name = gizmo_name + "_GR";
		return exGizmoRenderer;
	}

	public ExGizmoRenderer.COORDINATE coordinate = ExGizmoRenderer.COORDINATE.Local;

	public bool eDragUndo = false;

	protected bool dragnow = false;

	protected bool dragMove = false;

	protected FieldInfo _fi = null;

	protected FieldInfo _fi_beSelectedType = null;

	protected FieldInfo _fi_local_control_lock_ = null;

	private bool _isdrag_bk = false;

	private int selectedType = 0;

	public KeyCode smallMoveKey = KeyCode.None;

	public KeyCode bigMoveKey = KeyCode.None;

	protected Transform targetTrs = null;

	private Vector3 _backup_pos = Vector3.zero;

	private Quaternion _backup_rot = Quaternion.identity;

	private Vector3 _backup_local_scl = Vector3.one;

	protected Vector3 deltaPos = Vector3.zero;

	protected Quaternion deltaRot = Quaternion.identity;

	protected Vector3 deltaLocalPos = Vector3.zero;

	protected Quaternion deltaLocalRot = Quaternion.identity;

	protected Vector3 deltaLocalScl = Vector3.zero;

	protected Vector3 _predrag_pos = Vector3.zero;

	protected Quaternion _predrag_rot = Quaternion.identity;

	protected Vector3 _predrag_local_pos = Vector3.zero;

	protected Quaternion _predrag_local_rot = Quaternion.identity;

	protected Vector3 _predrag_local_scl = Vector3.one;

	protected bool _predrag_state = false;

	public static List<GameObject> _gameObjects_ = new List<GameObject>();

	public delegate void DragEndDelegate();

	public enum COORDINATE
	{
		Local,
		World,
		View
	}
}
