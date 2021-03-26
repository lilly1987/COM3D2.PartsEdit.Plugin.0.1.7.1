using System;
using System.Collections.Generic;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class BackUpObjectData
	{
		public bool changedYure = false;

		public bool bYure = true;

		public Dictionary<Transform, BackUpBoneData> boneDataDic = new Dictionary<Transform, BackUpBoneData>();
	}
}
