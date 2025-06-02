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
            public ItemContainer equipmentContainer { get; init; }
            ItemSection _equipment;

            public virtual int sectionsCount => 1;
            public virtual ItemContainer this[int idx] => equipmentContainer;

            public NPCInventory(NPCInventoryTemplate template)
            {
                weapon = template.weapon;
                _equipment = new();
                _equipment.AddItemsFrom(template.inventory);

                equipmentContainer = new("Equipment", _equipment);
            }

            public void AddItem(Item item)
            {
                _equipment.AddItem(item);
            }

            public int FindItemCount(Item item)
            {
                return _equipment.FindItemCount(item);
            }

            public virtual IEnumerator<ItemContainer> GetEnumerator()
            {
                yield return equipmentContainer;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}


