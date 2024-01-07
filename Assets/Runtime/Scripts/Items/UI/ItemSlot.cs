using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UI.DragAndDrop;
using UI.Tooltips;
using UnityEngine.Events;
using MouseButton = UnityEngine.EventSystems.PointerEventData.InputButton;
using Items.Actions;

namespace Items
{
    [RequireComponent(typeof(DragHandler))]
    public class ItemSlot : UIDataElement<ItemSlotData>, IItemSlot, IPointerEnterHandler, IPointerExitHandler,
     IDragDataSource<ItemSlotData>, IPointerClickHandler, IHaveItemTooltip
    {
        [SerializeField] DragableUIElement<ItemSlotData> _floatingItemPrefab;
        [Header("UI Elements")]
        [SerializeField] Image _icon;
        [SerializeField] Image _outline;
        [SerializeField] TextMeshProUGUI _count;

        //data
        protected ItemSlotData _slotData;

        //drag item
        public ItemSlotData dragData => _slotData;
        public IDragController dataHandler => _dragDataHandler;
        public bool allowToDrag => _slotData is not null;
        DragController<ItemSlotData> _dragDataHandler;
        //tooltip
        bool IHaveItemTooltip.shouldShowTooltip => _slotData != null;

        public virtual event UnityAction<ItemSlotData> OnClick;
        public event UnityAction<ItemSlotData> OnDragStart;

        void Awake()
        {
            var dragHandler = GetComponent<DragHandler>();
            dragHandler.OnDragStart += TriggerDragEvent;
            _dragDataHandler = new(this, _floatingItemPrefab);
        }

        public override void BindData(ItemSlotData slotData)
        {
            if (slotData.item is null) return;
            _icon.gameObject.SetActive(true);

            _slotData = slotData;

            _icon.sprite = slotData.item.icon;
            _icon.gameObject.SetActive(slotData.count > 0);

            _count.gameObject.SetActive(slotData.item.maxStackSize > 1);
            _count.text = slotData.count.ToString();
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