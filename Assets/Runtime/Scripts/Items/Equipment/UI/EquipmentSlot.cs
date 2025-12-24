using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Items.Equipment.UI
{
	public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] EquipmentSlotTemplate _equipmentSlot;
		[SerializeField] CustomEvent _onSlotSelected;
		[Header("UI Elements")]
		[SerializeField] Image _itemIcon;
		[SerializeField] TextMeshProUGUI _slotName;
		[SerializeField] TextMeshProUGUI _itemName;

		const string EMPTY_SLOT_KEY = "empty_slot_text";
		readonly Color _defaultColor = new Color(1, 1, 1, 1);
		readonly Color _emptySlotColor = new Color(1, 1, 1, 0.25f);

		ItemSlotData _itemInSlot;

		public event UnityAction<ItemSlotData> OnSlotSelected;
		public event UnityAction<EquipmentSlotTemplate> OnEmptySlotClick;

		public void Init()
		{
			_slotName.SetLocalisedText(_equipmentSlot.displayName);
		}

		public void SelectEquipment(IEquipmentStorage storage)
		{
			if(!storage.HasEquipment(_equipmentSlot.equipmentType)) return;
			BindData(storage.GetEquipment(_equipmentSlot.equipmentType));
		}

		public void Clear()
		{
			_itemInSlot = null;
			_itemIcon.sprite = _equipmentSlot.icon;
			_itemName.SetLocalisedText(EMPTY_SLOT_KEY);
			_itemIcon.color = _emptySlotColor;
		}

		public void BindData(ItemSlotData slotData)
		{
			_itemInSlot = slotData;
			_itemIcon.sprite = slotData.item.icon;
			_itemName.text = slotData.item.displayName;
			_itemIcon.color = _defaultColor;
		}

        public void OnPointerClick(PointerEventData eventData)
        {
			if (_itemInSlot is null)
			{
				OnEmptySlotClick?.Invoke(_equipmentSlot);
			}
			else
			{
				_onSlotSelected.Invoke();
				OnSlotSelected?.Invoke(_itemInSlot);				
			}
        }
    }
}