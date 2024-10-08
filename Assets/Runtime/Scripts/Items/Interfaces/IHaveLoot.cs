using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	public interface IHaveLoot
	{
	    LootTable lootTable { get; }
        void AddLoot(IItemStorage container);
        void RemoveLoot(IItemStorage container);
	}
}

