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
        [SerializeField] Sprite _emptyFrame;
        [SerializeField] DragableUIElement<ItemSlotData> _floatingItemPrefab;
        [Header("UI Elements")]
        [SerializeField] Image _icon;
        [SerializeField] Image _outline;
        [SerializeField] TextMeshProUGUI _count;

        [InjectField] ItemActionsController _itemActionsController;

        //data
        ItemSlotData _slotData;

        //drag item
        ItemSlotData IDragDataSource<ItemSlotData>.dragData => _slotData;
        public DragableUIElement<ItemSlotData> dragableElementPrefab => _floatingItemPrefab;
        public IDragController dataHandler => _dragDataHandler;
        //tooltip
        bool IHaveItemTooltip.shouldShowTooltip => _slotData != null;

        DragController<ItemSlotData> _dragDataHandler;

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
            _dragDataHandler = new(this, _floatingItemPrefab);
        }

        //mouse only
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (_slotData is null) return;
            _outline.gameObject.SetActive(true);
            _itemActionsController.FillContextMenu(_slotData);
        }

        //mouse only
        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            _outline.gameObject.SetActive(false);
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