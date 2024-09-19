using System.Collections.Generic;
using UnityEngine;

namespace Items.UI
{
    public class ShopSection : UILayoutWithObserver<ItemSlotData, ItemSlotWithPrice>
    {
        public override void UpdateLayout(IEnumerable<ItemSlotData> section)
        {
            base.UpdateLayout(GetSlotIterator(section));
        }

        public void ShowSelectedItems(IContainersList containers)
        {
            var selectedSlots = GetSelectedSlots(containers);
            var iterator = GetSlotIterator(selectedSlots);
            base.UpdateLayout(iterator);
        }

        IEnumerable<ItemSlotData> GetSelectedSlots(IContainersList containers)
        {
            foreach(var section in containers.GetAllContainers())
            {
                foreach(var slot in section.GetSelectedItems())
                {
                    yield return slot;
                }
            }
        }

        IEnumerable<ItemSlotData> GetSlotIterator(IEnumerable<ItemSlotData> section)
        {
            var size = GetLayoutSize();
            var emptySlots = size.x;

            foreach (var slot in section)
            {
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