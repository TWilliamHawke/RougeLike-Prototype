using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Entities.NPCScripts
{
	[System.Serializable]
	public class TraderContainerTemplate
	{
		public string containerName;
		public int security;
		public LootTable loot;
	}
}


