using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UI.DragAndDrop;

namespace Items
{
    public class ItemSlot : UIDataElement<ItemSlotData>, IPointerEnterHandler, IPointerExitHandler, IDragDataSource<ItemSlotData>
    {
        [SerializeField] Sprite _emptyFrame;
        [SerializeField] DragController _dragController;
		[SerializeField] DragableUIElement<ItemSlotData> _floatingItemPrefab;
        [Header("UI Elements")]
        [SerializeField] Image _icon;
        [SerializeField] Image _outline;
        [SerializeField] TextMeshProUGUI _count;

        ItemSlotData _slotData;
        DragHandler<ItemSlotData> _draghandler;

        ItemSlotData IDragDataSource<ItemSlotData>.dragData => _slotData;

        public DragableUIElement<ItemSlotData> dragableElementPrefab => _floatingItemPrefab;

        DragController IDragDataSource<ItemSlotData>.dragController => _dragController;

        void Awake()
        {
            _draghandler = new DragHandler<ItemSlotData>(this);
        }

        public override void UpdateData(ItemSlotData slotData)
        {
            _icon.gameObject.SetActive(true);

            _slotData = slotData;

            _icon.sprite = slotData.item.icon;

            if (slotData.item.maxStackSize > 1)
            {
                _count.gameObject.SetActive(true);
                _count.text = slotData.count.ToString();
            }
        }

        public void Clear()
        {
            _slotData = null;
            _icon.gameObject.SetActive(false);
            _count.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_slotData is null) return;
            _outline.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _outline.gameObject.SetActive(false);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            if(_slotData is null) return;
			_draghandler.OnBeginDrag();
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if(_slotData is null) return;
			_draghandler.OnDrag(eventData);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if(_slotData is null) return;
			_draghandler.OnEndDrag();
        }
    }
}