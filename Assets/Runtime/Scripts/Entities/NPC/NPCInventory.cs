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
            public ItemStorage equipment { get; init; }

            public virtual int storageCount => 1;
            public virtual ItemStorage this[int idx] => equipment;

            public NPCInventory(NPCInventoryTemplate template)
            {
                weapon = template.weapon;
                loot = template.inventory;

                equipment = new("Equipment", loot);
            }

            public void DeselectItem(ItemSlotData item)
            {
                this.ForEach(storage => storage.DeselectItem(item));
            }

            public virtual IEnumerator<ItemStorage> GetEnumerator()
            {
                yield return equipment;
            }

            public IEnumerable<ItemSlotData> GetSelectedItems()
            {
                return this.SelectMany(storage => storage.selectedItems);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}


