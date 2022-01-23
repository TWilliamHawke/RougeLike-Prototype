using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	[System.Serializable]
	public class LootTableData
	{
	    public LootTable lootTable;
		[Range(0,1)]
		public float chanceOfNone;
	}
}