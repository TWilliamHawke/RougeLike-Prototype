using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Entities.Combat;
using System.Linq;

namespace Entities.NPC
{
    public partial class NPCInventoryTemplate : ScriptableObject
    {

        protected class NPCInventory : INPCInventory
        {
            public Dictionary<DamageType, int> resists { get; init; } = new();

            public Weapon weapon { get; init; }
            public LootTable loot { get; init; }
            public ItemContainer equipment { get; init; }

            public virtual int sectionsCount => 1;
            public virtual ItemContainer this[int idx] => equipment;

            public NPCInventory(NPCInventoryTemplate template)
            {
                weapon = template.weapon;
                loot = template.inventory;

                equipment = new("Equipment", loot);
            }

            public virtual IEnumerator<ItemContainer> GetEnumerator()
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


