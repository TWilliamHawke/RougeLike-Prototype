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

        List<IItemSlot> _itemSlots = new(5);

        public void FillSection(IInventorySectionData section)
        {
            for (int i = 0; i < _itemSlots.Count; i++)
            {
                if (i >= section.count)
                {
                    _itemSlots[i].Clear();
                }
                else
                {
                    _itemSlots[i].BindData(section[i]);
                }
            }
        }

        public void CreateSlot()
        {
            var slot = Instantiate(_slotPrefab);
            slot.transform.SetParent(_layout.transform);
            slot.gameObject.transform.localScale = transform.localScale;
            _itemSlots.Add(slot);
        }

        public void ClearLayout()
        {
            _itemSlots.Clear();
            _layout.DestroyChildren();
        }
    }
}