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
            public ItemStorage equipment { get; init; }

            public NPCInventory(NPCInventoryTemplate template)
            {
                weapon = template.weapon;
                loot = template.inventory;

                equipment = new("Equipment", loot, ItemStorageType.inventory);
            }

            public virtual IEnumerator<ItemStorage> GetEnumerator()
            {
                yield return equipment;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}


