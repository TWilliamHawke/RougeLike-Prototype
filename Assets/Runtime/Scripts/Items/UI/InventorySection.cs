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

        IInventorySectionData _section;

		List<ItemSlot> _itemSlots;

		private void OnEnable() {
			CreateEmptySlots();
			FillSection();
		}

        public void SetSectionData(IInventorySectionData section)
        {
            _section = section;
			_itemSlots = new List<ItemSlot>(_section.maxCount);
			_section.OnItemAdd += FillSection;
        }

        void FillSection()
        {
			if(_section is null) return;
			
            for (int i = 0; i < _section.count; i++)
			{
				if(i >= _itemSlots.Count) return;
				_itemSlots[i].UpdateData(_section[i]);
			}
        }

		void CreateEmptySlots()
		{
			ClearLayout();
			for (int i = 0; i < _section.maxCount; i++)
			{
				CreateSlot();
			}
		}

        void CreateSlot()
        {
            var slot = Instantiate(_slotPrefab);
            slot.transform.SetParent(_layout.transform);
            slot.gameObject.transform.localScale = transform.localScale;
			_itemSlots.Add(slot);
        }

        void ClearLayout()
        {
            _itemSlots.Clear();
            foreach (Transform children in _layout.transform)
            {
                Destroy(children.gameObject);
            }

        }
    }
}