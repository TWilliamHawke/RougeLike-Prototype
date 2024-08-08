using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UI.Tooltips;
using UnityEngine.Events;
using MouseButton = UnityEngine.EventSystems.PointerEventData.InputButton;
using Items.Actions;

namespace Items
{
    public class ItemSlot : UIDataElement<ItemSlotData>, IItemSlot, IPointerEnterHandler, IPointerExitHandler,
     IPointerClickHandler, IHaveItemTooltip
    {
        [Header("UI Elements")]
        [SerializeField] Image _icon;
        [SerializeField] Image _outline;
        [SerializeField] RectTransform _count;
        [SerializeField] TextMeshProUGUI _countText;
        [SerializeField] ItemSlotDragLogic _dragLogic;

        //data
        protected ItemSlotData _slotData;

        //tooltip
        bool IHaveItemTooltip.shouldShowTooltip => _slotData != null;

        public virtual event UnityAction<ItemSlotData> OnClick;
        public event UnityAction<ItemSlotData> OnDragStart;

        void Awake()
        {
            _dragLogic.OnDragStart += TriggerDragEvent;
        }

        public override void BindData(ItemSlotData slotData)
        {
            if (slotData.item is null) return;
            _dragLogic.SetData(slotData);
            _icon.gameObject.SetActive(true);

            _slotData = slotData;

            _icon.sprite = slotData.item.icon;
            _icon.gameObject.SetActive(slotData.count > 0);

            _count.gameObject.SetActive(slotData.item.maxStackSize > 1);
            _countText.text = slotData.count.ToString();
        }

        public void Clear()
        {
            _slotData = null;
            _icon.gameObject.SetActive(false);
            _count.gameObject.SetActive(false);
        }

        private void TriggerDragEvent()
        {
            if (_slotData is null) return;
            OnDragStart?.Invoke(_slotData);
        }

        //mouse only
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (_slotData is null) return;
            _outline.gameObject.SetActive(true);
        }

        //mouse only
        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            _outline.gameObject.SetActive(false);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (_slotData is null) return;
            OnClick?.Invoke(_slotData);
        }

        ItemTooltipData IHaveItemTooltip.GetTooltipData()
        {
            return _slotData.item.GetTooltipData();
        }

    }
}