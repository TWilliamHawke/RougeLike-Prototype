using System.Collections.Generic;
using UnityEngine;

namespace Items.UI
{
    public class ShopSection : UILayoutWithObserver<ItemSlotData, ItemSlotWithPrice>
    {
        HashSet<ItemSlotData> _selectedItems = new();

        public override void UpdateLayout(IEnumerable<ItemSlotData> section)
        {
            base.UpdateLayout(GetSlotIterator(section, _selectedItems));
        }

        public void ShowUnselectedItems(IEnumerable<ItemSlotData> section, HashSet<ItemSlotData> selectedItems)
        {
            base.UpdateLayout(GetSlotIterator(section, selectedItems));
        }

        IEnumerable<ItemSlotData> GetSlotIterator(IEnumerable<ItemSlotData> section, HashSet<ItemSlotData> selectedItems)
        {
            var size = GetLayoutSize();
            var emptySlots = size.x;

            foreach (var slot in section)
            {
                if (selectedItems.Contains(slot)) continue;
                emptySlots = emptySlots < 0 ? size.x : emptySlots - 1;
                yield return slot;
            }

            for(int i = 1; i <= emptySlots; i++)
            {
                yield return new ItemSlotData();
            }
        }
    }
}