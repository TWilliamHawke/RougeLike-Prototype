using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Items
{
	[System.Serializable]
	public class ItemContainerData
	{
		public string storageName;
		public int security;
		public LootTable loot;
	}
}


