using System.Collections;
using System.Collections.Generic;
using Items.Equipment;
using UnityEngine;

namespace Items
{

    [CreateAssetMenu(fileName = "InventoryIterator", menuName = "Items/InventoryIterator")]
    public class InventoryIterator : ScriptableObject, IInventoryIterator
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] ItemSectionTemplate[] _visibleSections;
        [SerializeField] ItemSectionTemplate[] _mainSections;

        public IEnumerable<ItemSectionTemplate> GetVisibleSections()
        {
            return _visibleSections;
        }

        public IEnumerable<ItemSlotData> GetMainItems()
        {
            foreach (var template in _mainSections)
            {
                var section = _inventory.GetSection(template);
                if (section is null) continue;
                foreach (var item in section.GetItems())
                {
                    yield return item;
                }
            }
        }

        public bool HasEquipmentForSlot(ItemSlotData slot)
        {
			int slotIndex = slot.GetEquipmentSlot().index;
            foreach (var candidate in GetMainItems())
			{
				if (candidate.item is not IEquipment) continue;

				if (candidate.GetEquipmentSlot().index == slotIndex)
				{
					return true;
				}
			}

			return false;
        }
    }
}