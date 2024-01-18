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
    public class SpellStringPreview : UIDataElement<ItemSlotData>, IDragDataSource<ItemSlotData>
    {
        [SerializeField] DragableUIElement<ItemSlotData> _dragableItemPrefab;
        [Header("UI Elements")]
        [SerializeField] Image _icon;
        [SerializeField] TextMeshProUGUI _title;
        [SerializeField] TextMeshProUGUI _count;
        [Header("Events")]
        [SerializeField] CustomEvent _onSpellLineDragStart;
        [SerializeField] CustomEvent _onSpellLineDragEnd;

        ItemSlotData _itemSlotData;
        DragController<ItemSlotData> _dragHandler;
        //drag item
        public ItemSlotData dragData => _itemSlotData;
        public IDragController dataHandler => _dragHandler;
        public bool allowToDrag => _itemSlotData is not null;

        private void Awake()
        {
            _dragHandler = new(this, _dragableItemPrefab);
        }

        public DragableUIElement<ItemSlotData> CreateDragableElement()
        {
            return Instantiate(_dragableItemPrefab);
        }

        public override void BindData(ItemSlotData data)
        {
            _itemSlotData = data;
            _icon.sprite = data.item.icon;
            _title.text = data.item.displayName;
            _count.text = data.count.ToString();
        }
    }
}