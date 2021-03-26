using System;

internal static class MaidUtil
{
	public static string GetMaidFullName(Maid maid)
	{
		return maid.status.lastName + " " + maid.status.firstName;
	}
}
