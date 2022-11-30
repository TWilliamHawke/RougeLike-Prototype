using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Items;
using UnityEngine.UI;
using UI.DragAndDrop;
using UnityEngine.EventSystems;

namespace Magic.UI
{
    public class SpellStringPreview : UIDataElement<ItemSlotData>, IDragDataSource<ItemSlotData>, IInjectionTarget
    {
        [SerializeField] DragableUIElement<ItemSlotData> _dragableItemPrefab;
        [Header("UI Elements")]
        [SerializeField] Image _icon;
        [SerializeField] TextMeshProUGUI _title;
        [SerializeField] TextMeshProUGUI _count;
        [Header("Injectors")]
        [SerializeField] Injector _dragCanvasInjector;
        [Header("Events")]
        [SerializeField] CustomEvent _onSpellLineDragStart;
        [SerializeField] CustomEvent _onSpellLineDragEnd;

        [InjectField] Canvas _dragCanvas;

        ItemSlotData _itemSlotData;
        DragHandler<ItemSlotData> _dragHandler;

        public ItemSlotData dragData => _itemSlotData;

        public DragableUIElement<ItemSlotData> dragableElementPrefab => _dragableItemPrefab;
        public bool waitForAllDependencies => false;

        private void Awake()
        {
            _dragCanvasInjector.AddInjectionTarget(this);
        }

        public DragableUIElement<ItemSlotData> CreateDragableElement()
        {
            return Instantiate(_dragableItemPrefab);
        }

        public override void UpdateData(ItemSlotData data)
        {
            _itemSlotData = data;
            _icon.sprite = data.item.icon;
            _title.text = data.item.displayName;
            _count.text = data.count.ToString();
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _dragHandler.OnBeginDrag();
            _onSpellLineDragStart?.Invoke();
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            _dragHandler.OnDrag(eventData);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            _dragHandler.OnEndDrag();
            _onSpellLineDragEnd?.Invoke();
        }

        public void FinalizeInjection()
        {
            _dragHandler = new DragHandler<ItemSlotData>(this, _dragCanvas);
        }
    }
}