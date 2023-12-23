using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Entities.Combat;

namespace Entities.NPC
{
    public partial class NPCInventoryTemplate : ScriptableObject
    {

        protected class NPCInventory : INPCInventory
        {
            public Dictionary<DamageType, int> resists { get; init; } = new();

            public Weapon weapon { get; init; }
            public LootTable loot { get; init; }

            public NPCInventory(NPCInventoryTemplate template)
            {
                weapon = template.weapon;
                loot = template.inventory;
            }
        }
    }
}


