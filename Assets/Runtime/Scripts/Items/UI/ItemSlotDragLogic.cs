using UnityEngine;
using UI.DragAndDrop;
using UnityEngine.Events;

namespace Items
{
    [RequireComponent(typeof(DragHandler))]
    public class ItemSlotDragLogic : MonoBehaviour, IDragDataSource<ItemSlotData>
    {
        [SerializeField] DragableUIElement<ItemSlotData> _floatingItemPrefab;

        public event UnityAction OnDragStart;
        //drag item
        public ItemSlotData dragData => _slotData;
        public IDragController dataHandler => _dragDataHandler;
        public bool allowToDrag => _slotData is not null;
        DragController<ItemSlotData> _dragDataHandler;

        //data
        ItemSlotData _slotData;

        void Awake()
        {
            var dragHandler = GetComponent<DragHandler>();
            dragHandler.OnDragStart += TriggerDragEvent;
            _dragDataHandler = new(this, _floatingItemPrefab);
        }

        public void SetData(ItemSlotData slotData)
        {
            _slotData = slotData;
        }

        private void TriggerDragEvent()
        {
            if (_slotData is null) return;
            OnDragStart?.Invoke();
        }

    }
}