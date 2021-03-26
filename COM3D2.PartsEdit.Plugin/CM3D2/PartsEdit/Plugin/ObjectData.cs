using System;
using System.Collections.Generic;
using UnityEngine;

namespace CM3D2.PartsEdit.Plugin
{
	public class ObjectData
	{
		public string version = "0.1";

		public string slotName = "";

		public bool bMaidParts = false;

		public ObjectData.TransformData rootData = null;

		public bool bYure = true;

		public List<ObjectData.TransformData> transformDataList = new List<ObjectData.TransformData>();

		public class TransformData
		{
			public string name;

			public Vector3 position = Vector3.zero;

			public Quaternion rotation = Quaternion.identity;

			public Vector3 scale = Vector3.one;
		}
	}
}
