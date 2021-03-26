using System;
using System.Collections.Generic;
using System.Linq;

internal class MaidObserver
{
	public bool CheckActiveChange()
	{
		bool flag = this.maidHash == null;
		bool result;
		if (flag)
		{
			result = true;
		}
		else
		{
			List<Maid> activeMaidList = this.GetActiveMaidList();
			bool flag2 = this.maidHash.Count != activeMaidList.Count;
			if (flag2)
			{
				result = true;
			}
			else
			{
				foreach (Maid item in activeMaidList)
				{
					bool flag3 = !this.maidHash.Contains(item);
					if (flag3)
					{
						return true;
					}
				}
				result = false;
			}
		}
		return result;
	}

	public void UpdateMaidActiveState()
	{
		this.maidHash = new HashSet<Maid>(this.GetActiveMaidList());
	}

	public List<Maid> GetActiveMaidList()
	{
		List<Maid> list = new List<Maid>();
		for (int i = 0; i < GameMain.Instance.CharacterMgr.GetMaidCount(); i++)
		{
			Maid maid = GameMain.Instance.CharacterMgr.GetMaid(i);
			bool flag = maid == null || !maid.gameObject.activeInHierarchy;
			if (!flag)
			{
				list.Add(maid);
			}
		}
		List<Maid> stockMaidList = GameMain.Instance.CharacterMgr.GetStockMaidList();
		list.AddRange(from m in stockMaidList
		where m && m.gameObject.activeInHierarchy
		select m);
		return list.Distinct<Maid>().ToList<Maid>();
	}

	private HashSet<Maid> maidHash = null;
}
