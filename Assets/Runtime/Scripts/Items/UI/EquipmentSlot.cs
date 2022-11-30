using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.DragAndDrop;
using UnityEngine.UI;
using Core;

namespace Items.UI
{
	public class EquipmentSlot : MonoBehaviour, IDropTarget<ItemSlotData>
	{
		[SerializeField] EquipmentTypes _inventorySlot;
		[SerializeField] EquipmentTypes _allowedType;
		[Header("UI Elements")]
		[SerializeField] Image _itemIcon;
		[SerializeField] Image _slotTypeIcon;

		Item _itemInSlot;

        public bool checkImageAlpha => false;

        public bool DataIsMeet(ItemSlotData data)
        {
            var equipment = data.item as IEquipment;
			return equipment != null && equipment.equipmentType == _allowedType;
        }

        public void DropData(ItemSlotData data)
        {
			data.RemoveFromStack();
            _itemInSlot = data.item;
			UpdateSlotUI();
        }

        void UpdateSlotUI()
		{
			_itemIcon.gameObject.SetActive(true);
			_itemIcon.sprite = _itemInSlot.icon;
			_slotTypeIcon.gameObject.SetActive(false);
		}

	}
}