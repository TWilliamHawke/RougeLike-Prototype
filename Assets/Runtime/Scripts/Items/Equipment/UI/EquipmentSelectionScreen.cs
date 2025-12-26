using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Items.Equipment.UI
{
    public class EquipmentSelectionScreen : MonoBehaviour, IEquipmentSelectior, IObserver<EquipmentSlot>
    {
        [SerializeField] InventoryIterator _iterator;
        [SerializeField] EquipmentSelectionButton _buttonPrefab;
		[SerializeField] TextMeshProUGUI _equipmentSlotName;

        [SerializeField] EquipmentSelectionLayout _layout;
		[SerializeField] UIScreen _screen;
		[SerializeField] EquipmentScreen _equipmentScreen;

		void Awake()
		{
			_equipmentScreen.AddObserver(this);
		}

        public void AddToObserve(EquipmentSlot target)
        {
            target.OnEmptySlotClick += ShowMainItems;
        }

        public void RemoveFromObserve(EquipmentSlot target)
        {
            target.OnEmptySlotClick -= ShowMainItems;
        }

        public void ShowMainItems(IEquipmentSlotTemplate slotTemplate)
        {
			_equipmentSlotName.text = slotTemplate.displayName;
			_screen.Open();
            _layout.ClearLayout();
            ShowItemsInSection(slotTemplate, _iterator.GetMainItems());
        }

        public void ShowStorageItems(IEquipmentSlotTemplate slotTemplate)
        {
            ShowMainItems(slotTemplate);
            ShowItemsInSection(slotTemplate, _iterator.GetStorageItems());
        }

        private void ShowItemsInSection(IEquipmentSlotTemplate slotTemplate, IEnumerable<ItemSlotData> section)
        {
            foreach (var slot in section)
            {
                if (slot.GetEquipmentSlot().index != slotTemplate.index) continue;

                var button = _layout.CreateLayoutElement(_buttonPrefab);
                button.BindData(slot);
            }
        }
    }
}