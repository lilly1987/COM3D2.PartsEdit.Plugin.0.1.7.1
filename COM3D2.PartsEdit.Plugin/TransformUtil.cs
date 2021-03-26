using System;
using UnityEngine;

internal class TransformUtil
{
	public static string GetRelativePath(Transform root, Transform target)
	{
		string text = "";
		bool flag = root == target;
		string result;
		if (flag)
		{
			result = text;
		}
		else
		{
			Transform transform = target;
			while (root != transform)
			{
				bool flag2 = transform == null;
				if (flag2)
				{
					return null;
				}
				bool flag3 = text != "";
				if (flag3)
				{
					text = transform.name + "/" + text;
				}
				else
				{
					text = transform.name;
				}
				transform = transform.parent;
			}
			result = text;
		}
		return result;
	}

	public static string GetAbsolutePath(Transform target)
	{
		string text = "";
		Transform transform = target;
		while (transform != null)
		{
			bool flag = text != "";
			if (flag)
			{
				text = transform.name + "/" + text;
			}
			else
			{
				text = transform.name;
			}
			transform = transform.parent;
		}
		return text;
	}
}
