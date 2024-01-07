using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items.UI
{

    [RequireComponent(typeof(InventorySection))]
    public class InventorySectionController : MonoBehaviour
    {
        [SerializeField] InventorySection _inventorySection;

        IInventorySectionData _sectionData;

        public void BindSection(IInventorySectionData sectionData)
        {
            _sectionData = sectionData;
            sectionData.OnSectionDataChange += UpdateSectionView;
        }

        public void AddSlotObservers(IObserver<ItemSlot> observer)
        {
            _inventorySection.AddObserver(observer);
        }

        public void UpdateSectionView()
        {
            _inventorySection.UpdateLayout(_sectionData);
        }
    }
}


