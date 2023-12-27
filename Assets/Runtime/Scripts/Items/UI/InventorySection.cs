using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Items.UI
{
    public class InventorySection : UIPanelWithGrid<ItemSlotData>
    {
        protected override IEnumerable<ItemSlotData> _layoutElementsData => throw new System.NotImplementedException();

        public void UpdateLayout(IInventorySectionData section)
        {
            base.UpdateLayout(section);
        }
    }
}