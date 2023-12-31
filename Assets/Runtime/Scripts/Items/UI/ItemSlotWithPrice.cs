using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Items
{
    public class ItemSlotWithPrice : ItemSlot, IPointerClickHandler
    {
        [SerializeField] EdgeLabel _price;
        public override event UnityAction<ItemSlotData> OnClick;

        public override void BindData(ItemSlotData slotData)
        {
            if (slotData.item is null) return;
            base.BindData(slotData);
            _price.SetEdge(slotData.slotPrice);
            _price.gameObject.SetActive(true);
        }

        public void SetValueStorage(IValueStorage valueStorage)
        {
            _price.AddToObserve(valueStorage);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (_slotData is null) return;
            if (!_price.isActive) return;
            OnClick?.Invoke(_slotData);
        }

    }
}