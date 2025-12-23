using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Items.UI
{
    public class InventorySection : UISection<ItemSlotData, ItemSlot>, IObserver<ItemSlot>
    {
        [SerializeField] UISectionHeader _sectionHeader;
        [SerializeField] ItemSlotsLayout _itemSlotList;
        [SerializeField] ItemSlot _itemSlotPrefab;
        ItemSectionTemplate _template;

        protected override UISectionHeader _header => _sectionHeader;
        protected override IUILayout _layout => _itemSlotList;
        protected override UILayoutWithObserver<ItemSlot> _observerLayout => _itemSlotList;
        protected override ItemSlot _slotPrefab => _itemSlotPrefab;

        public event UnityAction<ItemSlotData, ItemSectionTemplate> OnItemSlotClick;

        public void BindData(IUISectionData<ItemSlotData> sectionData, ItemSectionTemplate template)
        {
            _template = template;
            BindData(sectionData);
        }

        protected void HandleSlotClick(ItemSlotData slotData)
        {
            OnItemSlotClick?.Invoke(slotData, _template);
        }

        public override void AddToObserve(ItemSlot target)
        {
            target.OnClick += HandleSlotClick;
        }

        public override void RemoveFromObserve(ItemSlot target)
        {
            target.OnClick -= HandleSlotClick;
        }

    }
}