using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	[System.Serializable]
	public class ItemContainerData
	{
		public LocalString storageName;
		public int security;
		public LootTable loot;
	}
}


