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
            target.OnEmptySlotClick += ShowItemsInSection;
        }

        public void RemoveFromObserve(EquipmentSlot target)
        {
            target.OnEmptySlotClick -= ShowItemsInSection;
        }

        public void ShowItemsInSection(IEquipmentSlotTemplate slotTemplate)
        {
            ShowEquipmentInSection(slotTemplate, _iterator);
        }

        public void ShowEquipmentInSection(IEquipmentSlotTemplate slotTemplate, IInventoryIterator iterator)
        {
			_equipmentSlotName.text = slotTemplate.displayName;
			_screen.Open();
            _layout.ClearLayout();

            foreach (var slot in iterator.GetMainItems())
            {
                if (slot.GetEquipmentSlot().index != slotTemplate.index) continue;

                var button = _layout.CreateLayoutElement(_buttonPrefab);
                button.BindData(slot);
            }
        }
    }
}