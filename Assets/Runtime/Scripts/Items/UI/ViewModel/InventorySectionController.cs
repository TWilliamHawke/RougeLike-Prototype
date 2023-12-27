using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items.UI
{
    public class InventorySectionController
    {
        IInventorySectionData _sectionData;
        InventorySection _inventorySection;

        public InventorySectionController(
            IInventorySectionData sectionData, InventorySection inventorySection)
        {
            _sectionData = sectionData;
            _inventorySection = inventorySection;
            _sectionData.OnSectionDataChange += UpdateSectionView;
        }

        public void UpdateSectionView()
        {
            _inventorySection.UpdateLayout(_sectionData);
        }
    }
}


