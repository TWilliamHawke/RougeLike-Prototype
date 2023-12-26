using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Items
{
    public class ItemSlotWithPrice : UIDataElement<ItemSlotData>, IItemSlot, IPointerClickHandler
    {
        [Header("UI Elements")]
        [SerializeField] Image _icon;
        [SerializeField] TextMeshProUGUI _count;

        [SerializeField] TextMeshProUGUI _price;

        public event UnityAction<ItemSlotData> OnClick;

        //data
        ItemSlotData _slotData;

        public override void BindData(ItemSlotData slotData)
        {
            _icon.gameObject.SetActive(true);

            _slotData = slotData;

            _icon.sprite = slotData.item.icon;

            _count.gameObject.SetActive(slotData.item.maxStackSize > 1);
            _count.text = slotData.count.ToString();
            _price.text = slotData.slotPrice.ToString();
        }

        public void Clear()
        {
            _slotData = null;
            _icon.gameObject.SetActive(false);
            _count.gameObject.SetActive(false);
            _price.text = "0";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_slotData is null) return;
            OnClick?.Invoke(_slotData);
        }
    }
}