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
    public class ItemSlot : UIDataElement<ItemSlotData>, IItemSlot, IPointerEnterHandler, IPointerExitHandler,
     IDragDataSource<ItemSlotData>, IPointerClickHandler, IHaveItemTooltip
    {
        [SerializeField] Sprite _emptyFrame;
        [SerializeField] DragableUIElement<ItemSlotData> _floatingItemPrefab;
        [Header("UI Elements")]
        [SerializeField] Image _icon;
        [SerializeField] Image _outline;
        [SerializeField] TextMeshProUGUI _count;
        [Header("Injectors")]
        [SerializeField] Injector _dragCanvasInjector;
        [Header("Events")]
        [SerializeField] CustomEvent _onItemDragStart;
        [SerializeField] CustomEvent _onItemDragEnd;

        [InjectField] Canvas _dragCanvas;
        [InjectField] ItemActionsController _itemActionsController;

        //data
        ItemSlotData _slotData;

        //drag item
        DragHandler<ItemSlotData> _draghandler;
        ItemSlotData IDragDataSource<ItemSlotData>.dragData => _slotData;
        public DragableUIElement<ItemSlotData> dragableElementPrefab => _floatingItemPrefab;
        //tooltip
        bool IHaveItemTooltip.shouldShowTooltip => _slotData != null;

        public override void BindData(ItemSlotData slotData)
        {
            _icon.gameObject.SetActive(true);

            _slotData = slotData;

            _icon.sprite = slotData.item.icon;

            _count.gameObject.SetActive(slotData.item.maxStackSize > 1);
            _count.text = slotData.count.ToString();
        }

        public void Clear()
        {
            _slotData = null;
            _icon.gameObject.SetActive(false);
            _count.gameObject.SetActive(false);
        }

        //event in editor
        public void CreateDragHandler()
        {
            _draghandler = new DragHandler<ItemSlotData>(this, _dragCanvas);
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (_slotData is null) return;
            _outline.gameObject.SetActive(true);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            _outline.gameObject.SetActive(false);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            if (_slotData is null) return;
            _draghandler.OnBeginDrag();
            _onItemDragStart?.Invoke();
            _itemActionsController.FillContextMenu(_slotData);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (_slotData is null) return;
            _draghandler.OnDrag(eventData);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if (_slotData is null) return;
            _draghandler.OnEndDrag();
            _onItemDragEnd?.Invoke();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (_slotData is null) return;

            if (eventData.button == MouseButton.Right)
            {
                _itemActionsController.FillContextMenu(_slotData);
            }
        }

        ItemTooltipData IHaveItemTooltip.GetTooltipData()
        {
            return _slotData.item.GetTooltipData();
        }

    }
}