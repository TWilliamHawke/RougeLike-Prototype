using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Items.UI
{
    public class InventorySection : MonoBehaviour
    {
        [SerializeField] LayoutGroup _layout;
        [SerializeField] ItemSlot _slotPrefab;

        List<ItemSlot> _itemSlots = new(5);

        public void FillSection(IInventorySectionData section)
        {
            for (int i = 0; i < section.count; i++)
            {
                if (i >= _itemSlots.Count) return;
                _itemSlots[i].UpdateData(section[i]);
            }
        }

        public void CreateSlot()
        {
            var slot = Instantiate(_slotPrefab);
            slot.transform.SetParent(_layout.transform);
            slot.gameObject.transform.localScale = transform.localScale;
            slot.SetSlotContainer(ItemSlotContainers.inventory);
            _itemSlots.Add(slot);
        }

        public void ClearLayout()
        {
            _itemSlots.Clear();
            _layout.DestroyChildren();
        }
    }
}