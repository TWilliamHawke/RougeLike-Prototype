using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Items.UI
{
    public class InventorySection : UILayoutWithObserver<ItemSlotData, ItemSlot>
    {
        public void UpdateLayout(IInventorySectionData section)
        {
            base.UpdateLayout(section);
        }
    }
}