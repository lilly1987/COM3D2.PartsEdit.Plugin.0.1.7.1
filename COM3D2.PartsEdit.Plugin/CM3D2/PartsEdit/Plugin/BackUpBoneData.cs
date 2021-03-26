using System;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	internal class BackUpBoneData
	{
		public bool changedPos = false;

		public bool changedRot = false;

		public bool changedScl = false;

		public Vector3 position = Vector3.zero;

		public Quaternion rotation = Quaternion.identity;

		public Vector3 scale = Vector3.one;
	}
}
