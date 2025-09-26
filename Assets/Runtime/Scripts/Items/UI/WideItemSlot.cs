using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

namespace Items
{
    public class WideItemSlot : ItemSlot, IPointerClickHandler
    {
        [SerializeField] TextMeshProUGUI _itemName;
        [SerializeField] CustomEvent _onItemSlotSelect;

        public override void BindData(ItemSlotData slotData)
        {
            base.BindData(slotData);
            _itemName.text = slotData.item.displayName;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            TriggerClickEvent();
            _onItemSlotSelect.Invoke();
        }


    }
}