using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	public interface IHaveLoot
	{
	    LootTable lootTable { get; }
        void AddLoot(ILootContainer container);
        void RemoveLoot(ILootContainer container);
	}
}

