using System;
using System.Collections.Generic;
using UnityEngine;

internal class UIParams
{
	public static UIParams Instance
	{
		get
		{
			return UIParams.instance;
		}
	}

	public UIParams()
	{
		this.listStyle.onHover.background = (this.listStyle.hover.background = new Texture2D(2, 2));
		this.listStyle.padding.left = (this.listStyle.padding.right = 4);
		this.listStyle.padding.top = (this.listStyle.padding.bottom = 1);
		this.listStyle.normal.textColor = (this.listStyle.onNormal.textColor = (this.listStyle.hover.textColor = (this.listStyle.onHover.textColor = (this.listStyle.active.textColor = (this.listStyle.onActive.textColor = Color.white)))));
		this.listStyle.focused.textColor = (this.listStyle.onFocused.textColor = Color.blue);
		TextAnchor alignment = TextAnchor.MiddleLeft;
		this.lStyleB.fontStyle = FontStyle.Bold;
		this.lStyleB.alignment = alignment;
		this.lStyle.fontStyle = FontStyle.Normal;
		this.lStyle.normal.textColor = this.textColor;
		this.lStyle.alignment = alignment;
		this.lStyleS.fontStyle = FontStyle.Normal;
		this.lStyleS.normal.textColor = this.textColor;
		this.lStyleS.alignment = alignment;
		this.lStyleRS.fontStyle = FontStyle.Normal;
		this.lStyleRS.normal.textColor = this.textColor;
		this.lStyleRS.alignment = TextAnchor.MiddleRight;
		this.lStyleC.fontStyle = FontStyle.Normal;
		this.lStyleC.normal.textColor = new Color(0.82f, 0.88f, 1f, 0.98f);
		this.lStyleC.alignment = alignment;
		this.bStyle.normal.textColor = this.textColor;
		this.bStyleSC.normal.textColor = this.textColor;
		this.bStyleSC.alignment = TextAnchor.MiddleCenter;
		this.bStyleL.normal.textColor = this.textColor;
		this.bStyleL.alignment = TextAnchor.MiddleLeft;
		this.tStyle.normal.textColor = this.textColor;
		this.tStyleS.normal.textColor = this.textColor;
		this.tStyleS.alignment = TextAnchor.LowerLeft;
		this.tStyleSS.normal.textColor = this.textColor;
		this.tStyleSS.alignment = TextAnchor.MiddleLeft;
		this.tStyleL.normal.textColor = this.textColor;
		this.tStyleL.alignment = alignment;
		this.textStyle.normal.textColor = this.textColor;
		this.textStyleSC.normal.textColor = this.textColor;
		this.textStyleSC.alignment = TextAnchor.MiddleCenter;
		this.textAreaStyleS.normal.textColor = this.textColor;
		this.dialogStyle.alignment = TextAnchor.UpperCenter;
		this.dialogStyle.normal.textColor = this.textColor;
		this.tipsStyle.alignment = TextAnchor.MiddleCenter;
		this.tipsStyle.wordWrap = true;
	}

	public void Update()
	{
		bool flag = false;
		bool flag2 = Screen.height != this.height;
		if (flag2)
		{
			this.height = Screen.height;
			flag = true;
		}
		bool flag3 = Screen.width != this.width;
		if (flag3)
		{
			this.width = Screen.width;
			flag = true;
		}
		bool flag4 = !flag;
		if (!flag4)
		{
			this.ratio = 1f + ((float)this.width / 1280f - 1f) * 0.6f;
			this.fontSize = this.FixPx(14);
			this.fontSizeS = this.FixPx(12);
			this.fontSizeSS = this.FixPx(11);
			this.fontSizeL = this.FixPx(20);
			this.margin = this.FixPx(2);
			this.marginL = this.FixPx(10);
			this.itemHeight = this.FixPx(18);
			this.unitHeight = this.margin + this.itemHeight;
			this.lStyle.fontSize = this.fontSize;
			this.lStyleC.fontSize = this.fontSize;
			this.lStyleB.fontSize = this.fontSize;
			this.lStyleS.fontSize = this.fontSizeS;
			this.lStyleRS.fontSize = this.fontSizeS;
			this.bStyle.fontSize = this.fontSize;
			this.bStyleSC.fontSize = this.fontSizeS;
			this.bStyleL.fontSize = this.fontSize;
			this.tStyle.fontSize = this.fontSize;
			this.tStyleS.fontSize = this.fontSizeS;
			this.tStyleSS.fontSize = this.fontSizeSS;
			this.tStyleL.fontSize = this.fontSizeL;
			this.listStyle.fontSize = this.fontSizeS;
			this.textStyle.fontSize = this.fontSize;
			this.textStyleSC.fontSize = this.fontSizeS;
			this.textAreaStyleS.fontSize = this.fontSizeS;
			this.winStyle.fontSize = this.fontSize;
			this.tipsStyle.fontSize = this.fontSize;
			this.dialogStyle.fontSize = this.fontSize;
			this.InitWinRect();
			this.InitFBRect();
			this.InitModalRect();
			this.subConWidth = this.winRect.width - (float)(this.margin * 2);
			this.optBtnHeight = GUILayout.Height((float)this.itemHeight);
			this.optSubConWidth = GUILayout.Width(this.subConWidth);
			this.optSubConHeight = GUILayout.Height(this.winRect.height - (float)this.unitHeight * 3f);
			this.optSubCon6Height = GUILayout.Height(this.winRect.height - (float)this.unitHeight * 6.6f);
			this.optSubConHalfWidth = GUILayout.Width((this.winRect.width - (float)(this.marginL * 2)) * 0.5f);
			this.optSLabelWidth = GUILayout.Width((float)this.fontSizeS * 6f);
			this.mainRect.Set((float)this.margin, (float)(this.unitHeight * 5 + this.margin), this.winRect.width - (float)(this.margin * 2), this.winRect.height - (float)this.unitHeight * 6.5f);
			this.textureRect.Set((float)this.margin, (float)this.unitHeight, this.winRect.width - (float)(this.margin * 2), this.winRect.height - (float)this.unitHeight * 2.5f);
			float num = this.textureRect.width - 20f;
			this.optBtnWidth = GUILayout.Width(num * 0.09f);
			this.optDBtnWidth = GUILayout.Width((float)this.fontSizeS * 5f * 0.6f);
			this.optContentWidth = GUILayout.MaxWidth(num * 0.69f);
			this.optCategoryWidth = GUILayout.MaxWidth((float)this.fontSize * 12f * 0.47f);
			this.nodeSelectRect.Set((float)this.margin, (float)(this.unitHeight * 2), this.winRect.width - (float)(this.margin * 2), this.winRect.height - (float)this.unitHeight * 4.5f);
			this.colorRect.Set((float)this.margin, (float)(this.unitHeight * 2), this.winRect.width - (float)(this.margin * 3), this.winRect.height - (float)(this.unitHeight * 4));
			this.labelRect.Set(0f, 0f, this.winRect.width - (float)(this.margin * 2), (float)this.itemHeight * 1.2f);
			this.subRect.Set(0f, (float)this.itemHeight, this.winRect.width - (float)(this.margin * 2), (float)this.itemHeight);
			foreach (Action<UIParams> action in this.updaters)
			{
				action(this);
			}
		}
	}

	public void InitWinRect()
	{
		this.winRect.Set((float)(this.width - this.FixPx(290)), (float)this.FixPx(48), (float)this.FixPx(280), (float)(this.height - this.FixPx(150)));
		this.titleBarRect.Set(0f, 0f, this.winRect.width, 24f);
	}

	public void InitFBRect()
	{
		this.fileBrowserRect.Set((float)(this.width - this.FixPx(620)), (float)this.FixPx(100), (float)this.FixPx(600), (float)this.FixPx(600));
	}

	public void InitModalRect()
	{
		this.modalRect.Set((float)(this.width / 2 - this.FixPx(300)), (float)(this.height / 2 - this.FixPx(300)), (float)this.FixPx(600), (float)this.FixPx(600));
	}

	public int FixPx(int px)
	{
		return (int)(this.ratio * (float)px);
	}

	public void Add(Action<UIParams> action)
	{
		action(this);
		this.updaters.Add(action);
	}

	public bool Remove(Action<UIParams> action)
	{
		return this.updaters.Remove(action);
	}

	private static UIParams instance = new UIParams();

	private const int marginPx = 2;

	private const int marginLPx = 10;

	private const int itemHeightPx = 18;

	private const int fontPx = 14;

	private const int fontPxS = 12;

	private const int fontPxSS = 11;

	private const int fontPxL = 20;

	private int width;

	private int height;

	private float ratio;

	public int margin;

	public int marginL;

	public int fontSize;

	public int fontSizeS;

	public int fontSizeSS;

	public int fontSizeL;

	public int itemHeight;

	public int unitHeight;

	public readonly GUIStyle lStyle = new GUIStyle("label");

	public readonly GUIStyle lStyleB = new GUIStyle("label");

	public readonly GUIStyle lStyleC = new GUIStyle("label");

	public readonly GUIStyle lStyleS = new GUIStyle("label");

	public readonly GUIStyle lStyleRS = new GUIStyle("label");

	public readonly GUIStyle bStyle = new GUIStyle("button");

	public readonly GUIStyle bStyleSC = new GUIStyle("button");

	public readonly GUIStyle bStyleL = new GUIStyle("button");

	public readonly GUIStyle tStyle = new GUIStyle("toggle");

	public readonly GUIStyle tStyleS = new GUIStyle("toggle");

	public readonly GUIStyle tStyleSS = new GUIStyle("toggle");

	public readonly GUIStyle tStyleL = new GUIStyle("toggle");

	public readonly GUIStyle listStyle = new GUIStyle();

	public readonly GUIStyle textStyle = new GUIStyle("textField");

	public readonly GUIStyle textStyleSC = new GUIStyle("textField");

	public readonly GUIStyle textAreaStyleS = new GUIStyle("textArea");

	public readonly GUIStyle boxStyle = new GUIStyle("box");

	public readonly GUIStyle winStyle = new GUIStyle("box");

	public readonly GUIStyle dialogStyle = new GUIStyle("box");

	public readonly GUIStyle tipsStyle = new GUIStyle("window");

	public readonly Color textColor = new Color(1f, 1f, 1f, 0.98f);

	public Rect titleBarRect = default(Rect);

	public Rect winRect = default(Rect);

	public Rect fileBrowserRect = default(Rect);

	public Rect modalRect = default(Rect);

	public Rect mainRect = default(Rect);

	public Rect colorRect = default(Rect);

	public Rect nodeSelectRect = default(Rect);

	public Rect presetSelectRect = default(Rect);

	public Rect textureRect = default(Rect);

	public Rect labelRect = default(Rect);

	public Rect subRect = default(Rect);

	public GUILayoutOption optBtnHeight;

	public float subConWidth;

	public GUILayoutOption optSubConWidth;

	public GUILayoutOption optSubConHeight;

	public GUILayoutOption optSubCon6Height;

	public GUILayoutOption optSubConHalfWidth;

	public GUILayoutOption optBtnWidth;

	public GUILayoutOption optCategoryWidth;

	public GUILayoutOption optDBtnWidth;

	public GUILayoutOption optSLabelWidth;

	public GUILayoutOption optContentWidth;

	private readonly List<Action<UIParams>> updaters = new List<Action<UIParams>>();
}
