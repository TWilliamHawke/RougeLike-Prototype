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
        ItemSectionTemplate _template;

        protected override UISectionHeader _header => _sectionHeader;
        protected override UILayoutWithObserver<ItemSlotData, ItemSlot> _layout => _itemSlotList;

        public event UnityAction<ItemSlotData, ItemSectionTemplate> OnItemSlotClick;

        public void SetTemplate(ItemSectionTemplate template)
        {
            _template = template;
        }

        public void StartObserving()
        {
            AddObserver(this);
        }

        private void HandleSlotClick(ItemSlotData slotData)
        {
            OnItemSlotClick?.Invoke(slotData, _template);
        }

        public void AddToObserve(ItemSlot target)
        {
            target.OnClick += HandleSlotClick;
        }

        public void RemoveFromObserve(ItemSlot target)
        {
            target.OnClick -= HandleSlotClick;
        }
    }
}