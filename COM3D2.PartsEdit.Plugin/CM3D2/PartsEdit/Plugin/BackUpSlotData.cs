using System;
using System.Collections.Generic;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class BackUpSlotData
	{
		public Dictionary<GameObject, bool> yureDic = new Dictionary<GameObject, bool>();

		public Dictionary<GameObject, BackUpObjectData> objectDataDic = new Dictionary<GameObject, BackUpObjectData>();
	}
}
