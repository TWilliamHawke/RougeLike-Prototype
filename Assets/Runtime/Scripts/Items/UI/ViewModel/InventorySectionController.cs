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
            _sectionData.OnItemAdd += UpdateSectionView;

            _inventorySection.ClearLayout();

            for (int i = 0; i < _sectionData.maxCount; i++)
            {
                _inventorySection.CreateSlot();
            }
        }

        public void UpdateSectionView()
        {
            _inventorySection.FillSection(_sectionData);
        }
    }
}


