using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Items.Equipment;
using Abilities;

namespace Entities.NPC
{
    public class NPCInventory : INPCInventory
    {
        protected ItemContainer _npcItems { get; init; }
        ItemSection _inventorySection;
        EquipmentStorage _equipment = new();

        public NPCInventory(NPCInventoryTemplate template)
        {
            _inventorySection = template.CreateInventorySection();
            _npcItems = new(_inventorySection);
            var equipmentItems = template.GetEquipmentItems();

            foreach (var itemSlot in equipmentItems)
            {
                _equipment.AddEquipment(itemSlot);
            }
        }

        public void AddItem(IItem item)
        {
            _inventorySection.AddItem(item);
        }

        public int FindItemCount(IItem item)
        {
            return _inventorySection.FindItemCount(item);
        }

        public virtual IEnumerator<ItemContainer> GetEnumerator()
        {
            yield return _npcItems;
        }

        public void RemoveOneItem(IItem item)
        {
            _inventorySection.RemoveItem(item);
        }

        public IEnumerable<IAbilityContainer> GetItemAbilities(IAbilitiesFactory factory)
        {
            foreach(var item in GetAllItems())
            {
                if (item is not IAbilitySource abilitySource) continue;
                yield return abilitySource.CreateAbilityContainer(factory);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerable<IItem> GetAllItems()
        {
            foreach (var itemSlot in _inventorySection)
            {
                yield return itemSlot.item;
            }

            foreach (var itemSlot in _equipment.GetItems())
            {
                yield return itemSlot.item;
            }
        }
    }
}


