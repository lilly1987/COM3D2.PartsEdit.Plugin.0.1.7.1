using System;
using System.Collections.Generic;

internal static class SlotUtil
{
	private static List<string> SlotStrList
	{
		get
		{
			bool flag = SlotUtil.slotStrList == null;
			if (flag)
			{
				int num = (TBody.m_strDefSlotName.Length - 1) / 3;
				SlotUtil.slotStrList = new List<string>();
				for (int i = 0; i < num; i++)
				{
					SlotUtil.slotStrList.Add(TBody.m_strDefSlotName[i * 3]);
				}
			}
			return SlotUtil.slotStrList;
		}
	}

	private static Dictionary<string, List<int>> MpnSlotListDic
	{
		get
		{
			bool flag = SlotUtil.mpnSlotListDic == null;
			if (flag)
			{
				SlotUtil.mpnSlotListDic = new Dictionary<string, List<int>>();
				foreach (string key in SlotUtil.mpnSlotStrListDic.Keys)
				{
					List<int> list = new List<int>();
					foreach (string slotName in SlotUtil.mpnSlotStrListDic[key])
					{
						int slotID = SlotUtil.GetSlotID(slotName);
						bool flag2 = slotID == -1;
						if (!flag2)
						{
							list.Add(slotID);
						}
					}
					bool flag3 = list.Count == 0;
					if (!flag3)
					{
						SlotUtil.mpnSlotListDic.Add(key, list);
					}
				}
			}
			return SlotUtil.mpnSlotListDic;
		}
	}

	public static string GetSlotName(int slotNo)
	{
		return SlotUtil.SlotStrList[slotNo];
	}

	public static int GetSlotID(string slotName)
	{
		return SlotUtil.SlotStrList.IndexOf(slotName);
	}

	public static List<int> GetSlotListFromMpnStr(string mpnStr)
	{
		return new List<int>();
	}

	private static List<string> slotStrList = null;

	private static Dictionary<string, List<string>> mpnSlotStrListDic = new Dictionary<string, List<string>>
	{
		{
			"body",
			new List<string>
			{
				"body"
			}
		},
		{
			"moza",
			new List<string>
			{
				"moza"
			}
		},
		{
			"head",
			new List<string>
			{
				"head"
			}
		},
		{
			"hairf",
			new List<string>
			{
				"hairF"
			}
		},
		{
			"hairr",
			new List<string>
			{
				"hairR"
			}
		},
		{
			"hairt",
			new List<string>
			{
				"hairT"
			}
		},
		{
			"hairs",
			new List<string>
			{
				"hairS"
			}
		},
		{
			"underhair",
			new List<string>
			{
				"underhair"
			}
		},
		{
			"eye",
			new List<string>
			{
				"eye"
			}
		},
		{
			"chikubi",
			new List<string>
			{
				"chikubi"
			}
		},
		{
			"wear",
			new List<string>
			{
				"wear"
			}
		},
		{
			"skirt",
			new List<string>
			{
				"skirt"
			}
		},
		{
			"mizugi",
			new List<string>
			{
				"mizugi"
			}
		},
		{
			"bra",
			new List<string>
			{
				"bra"
			}
		},
		{
			"panz",
			new List<string>
			{
				"panz"
			}
		},
		{
			"stkg",
			new List<string>
			{
				"stkg"
			}
		},
		{
			"shoes",
			new List<string>
			{
				"shoes"
			}
		},
		{
			"headset",
			new List<string>
			{
				"headset"
			}
		},
		{
			"glove",
			new List<string>
			{
				"glove"
			}
		},
		{
			"acchead",
			new List<string>
			{
				"accHead"
			}
		},
		{
			"hairaho",
			new List<string>
			{
				"hairAho"
			}
		},
		{
			"accha",
			new List<string>
			{
				"accHa"
			}
		},
		{
			"acchana",
			new List<string>
			{
				"accHana"
			}
		},
		{
			"acckamisub",
			new List<string>
			{
				"accKamiSubR",
				"accKamiSubL"
			}
		},
		{
			"acckami",
			new List<string>
			{
				"accKami_1_",
				"accKami_2_",
				"accKami_3_"
			}
		},
		{
			"accmimi",
			new List<string>
			{
				"accMiMiR",
				"accMiMiL"
			}
		},
		{
			"accnip",
			new List<string>
			{
				"accNipR",
				"accNipL"
			}
		},
		{
			"acckubi",
			new List<string>
			{
				"accKubi"
			}
		},
		{
			"acckubiwa",
			new List<string>
			{
				"accKubiwa"
			}
		},
		{
			"accheso",
			new List<string>
			{
				"accHeso"
			}
		},
		{
			"accude",
			new List<string>
			{
				"accUde"
			}
		},
		{
			"accashi",
			new List<string>
			{
				"accAshi"
			}
		},
		{
			"accsenaka",
			new List<string>
			{
				"accSenaka"
			}
		},
		{
			"accshippo",
			new List<string>
			{
				"accShippo"
			}
		},
		{
			"accanl",
			new List<string>
			{
				"accAnl"
			}
		},
		{
			"accvag",
			new List<string>
			{
				"accVag"
			}
		},
		{
			"megane",
			new List<string>
			{
				"megane"
			}
		},
		{
			"accxxx",
			new List<string>
			{
				"accXXX"
			}
		},
		{
			"handitem",
			new List<string>
			{
				"HandItemR",
				"HandItemL"
			}
		},
		{
			"acchat",
			new List<string>
			{
				"accHat"
			}
		},
		{
			"onepiece",
			new List<string>
			{
				"onepiece"
			}
		},
		{
			"kousoku_upper",
			new List<string>
			{
				"kousoku_upper"
			}
		},
		{
			"kousoku_lower",
			new List<string>
			{
				"kousoku_lower"
			}
		},
		{
			"seieki_naka",
			new List<string>
			{
				"seieki_naka"
			}
		},
		{
			"seieki_hara",
			new List<string>
			{
				"seieki_hara"
			}
		},
		{
			"seieki_face",
			new List<string>
			{
				"seieki_face"
			}
		},
		{
			"seieki_mune",
			new List<string>
			{
				"seieki_mune"
			}
		},
		{
			"seieki_hip",
			new List<string>
			{
				"seieki_hip"
			}
		},
		{
			"seieki_ude",
			new List<string>
			{
				"seieki_ude"
			}
		},
		{
			"seieki_ashi",
			new List<string>
			{
				"seieki_ashi"
			}
		}
	};

	private static Dictionary<string, string> slotMpnStrDic = new Dictionary<string, string>
	{
		{
			"body",
			"body"
		},
		{
			"moza",
			"moza"
		},
		{
			"head",
			"head"
		},
		{
			"hairF",
			"hairf"
		},
		{
			"hairR",
			"hairr"
		},
		{
			"hairT",
			"hairt"
		},
		{
			"hairS",
			"hairs"
		},
		{
			"underhair",
			"underhair"
		},
		{
			"eye",
			"eye"
		},
		{
			"chikubi",
			"chikubi"
		},
		{
			"wear",
			"wear"
		},
		{
			"skirt",
			"skirt"
		},
		{
			"mizugi",
			"mizugi"
		},
		{
			"bra",
			"bra"
		},
		{
			"panz",
			"panz"
		},
		{
			"stkg",
			"stkg"
		},
		{
			"shoes",
			"shoes"
		},
		{
			"headset",
			"headset"
		},
		{
			"glove",
			"glove"
		},
		{
			"accHead",
			"acchead"
		},
		{
			"hairAho",
			"hairaho"
		},
		{
			"accHa",
			"accha"
		},
		{
			"accHana",
			"acchana"
		},
		{
			"accKamiSubR",
			"acckamisub"
		},
		{
			"accKamiSubL",
			"acckamisub"
		},
		{
			"accKami_1_",
			"acckami"
		},
		{
			"accKami_2_",
			"acckami"
		},
		{
			"accKami_3_",
			"acckami"
		},
		{
			"accMiMiR",
			"accmimi"
		},
		{
			"accMiMiL",
			"accmimi"
		},
		{
			"accNipR",
			"accnip"
		},
		{
			"accNipL",
			"accnip"
		},
		{
			"accKubi",
			"acckubi"
		},
		{
			"accKubiwa",
			"acckubiwa"
		},
		{
			"accHeso",
			"accheso"
		},
		{
			"accUde",
			"accude"
		},
		{
			"accAshi",
			"accashi"
		},
		{
			"accSenaka",
			"accsenaka"
		},
		{
			"accShippo",
			"accshippo"
		},
		{
			"accAnl",
			"accanl"
		},
		{
			"accVag",
			"accvag"
		},
		{
			"megane",
			"megane"
		},
		{
			"accXXX",
			"accxxx"
		},
		{
			"HandItemR",
			"handitem"
		},
		{
			"HandItemL",
			"handitem"
		},
		{
			"accHat",
			"acchat"
		},
		{
			"onepiece",
			"onepiece"
		},
		{
			"kousoku_upper",
			"kousoku_upper"
		},
		{
			"kousoku_lower",
			"kousoku_lower"
		},
		{
			"seieki_naka",
			"seieki_naka"
		},
		{
			"seieki_hara",
			"seieki_hara"
		},
		{
			"seieki_face",
			"seieki_face"
		},
		{
			"seieki_mune",
			"seieki_mune"
		},
		{
			"seieki_hip",
			"seieki_hip"
		},
		{
			"seieki_ude",
			"seieki_ude"
		},
		{
			"seieki_ashi",
			"seieki_ashi"
		}
	};

	private static Dictionary<string, List<int>> mpnSlotListDic = null;
}
