using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	public class LootTableException : System.Exception
	{
		LootTable _buggedLootTable;

		public LootTable lootTable => _buggedLootTable;

	    public LootTableException(LootTable lootTable)
		{
			_buggedLootTable = lootTable;
		}
	}
}