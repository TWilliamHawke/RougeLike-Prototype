using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Items
{
    public class ItemSlotWithPrice : ItemSlot
    {
        [SerializeField] TextMeshProUGUI _price;

        public event UnityAction<ItemSlotData> OnClick;

        public override void BindData(ItemSlotData slotData)
        {
            if (slotData.item is null) return;
            base.BindData(slotData);
            _price.text = slotData.slotPrice.ToString();
            _price.gameObject.SetActive(slotData.slotPrice >= 0);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_slotData is null) return;
            OnClick?.Invoke(_slotData);
        }
    }
}