using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Items.Equipment.UI
{
    public class EquipmentSelectionButton : UIDataElement<ItemSlotData>, IPointerClickHandler
	{
        [SerializeField] Image _itemIcon;
        [SerializeField] TextMeshProUGUI _itemName;

        ItemSlotData _itemSlotData;

        public event UnityAction<ItemSlotData> OnClick;

        public override void BindData(ItemSlotData data)
        {
            _itemSlotData = data;
            _itemIcon.sprite = data.item.icon;
            _itemName.text = data.item.displayName;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_itemSlotData is null) return;
            OnClick?.Invoke(_itemSlotData);
        }
    }
}