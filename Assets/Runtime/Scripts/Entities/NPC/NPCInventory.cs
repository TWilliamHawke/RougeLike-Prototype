using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Entities.Combat;
using System.Linq;

namespace Entities.NPC
{

    public class NPCInventory : INPCInventory
    {
        public Dictionary<DamageType, int> resists { get; init; } = new();

        public WeaponTemplate weapon { get; init; }
        protected NPCEquipment _equipmentContainer { get; init; }
        ItemSection _equipment;

        public NPCInventory(NPCInventoryTemplate template)
        {
            weapon = template.weapon;
            _equipment = new(template.equipmentSection);
            _equipment.AddItemsFrom(template.inventoryTable);

            _equipmentContainer = new(_equipment);
        }

        public void AddItem(IItem item)
        {
            _equipment.AddItem(item);
        }

        public int FindItemCount(IItem item)
        {
            return _equipment.FindItemCount(item);
        }

        public virtual IEnumerator<ItemContainer> GetEnumerator()
        {
            yield return _equipmentContainer;
        }

        public void RemoveOneItem(IItem item)
        {
            _equipment.RemoveItem(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}


