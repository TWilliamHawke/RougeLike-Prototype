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

namespace Items
{
    public class ItemSlot : UIDataElement<ItemSlotData>, IPointerEnterHandler, IPointerExitHandler,
     IDragDataSource<ItemSlotData>, IPointerClickHandler, IItemSlot, IHaveItemTooltip, IInjectionTarget
    {
        public static event UnityAction<IItemSlot> OnRightClick;

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

        //data
        ItemSlotData _slotData;
        ItemSlotContainers _slotContainer;
        ItemSlotContainers IItemSlot.itemSlotContainer => _slotContainer;
        ItemSlotData IItemSlot.itemSlotData => _slotData;

        //drag item
        DragHandler<ItemSlotData> _draghandler;
        ItemSlotData IDragDataSource<ItemSlotData>.dragData => _slotData;
        public DragableUIElement<ItemSlotData> dragableElementPrefab => _floatingItemPrefab;
        public Canvas dragCanvas => _dragCanvas;
        //tooltip
        bool IHaveItemTooltip.shouldShowTooltip => _slotData != null;

        public bool waitForAllDependencies => false;

        void Awake()
        {
            _dragCanvasInjector.AddInjectionTarget(this);
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

        public void SetSlotContainer(ItemSlotContainers slotContainer)
        {
            _slotContainer = slotContainer;
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
            if (eventData.button == MouseButton.Right)
            {
                OnRightClick?.Invoke(this);
            }
        }

        ItemTooltipData IHaveItemTooltip.GetTooltipData()
        {
            return _slotData.item.GetTooltipData();
        }

        void IInjectionTarget.FinalizeInjection()
        {
        }
    }
}