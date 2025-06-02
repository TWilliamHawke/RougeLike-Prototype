using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	public interface IHaveLoot
	{
        void AddLootTo(IItemStorage container);
        void RemoveLootFrom(IItemStorage container);
	}
}

