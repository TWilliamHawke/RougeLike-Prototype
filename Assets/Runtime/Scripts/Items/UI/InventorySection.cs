using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Items.UI
{
    public class InventorySection : UISection<ItemSlot>, IObserver<ItemSlot>
    {
        [SerializeField] UISectionHeader _sectionHeader;
        [SerializeField] ItemSlotsLayout _itemSlotList;
        [SerializeField] ItemSlot _itemSlotPrefab;
        ItemSectionTemplate _template;

        protected override UISectionHeader _header => _sectionHeader;
        protected override UILayoutWithObserver<ItemSlot> _layout => _itemSlotList;
        protected override bool _sectionDataIsEmpty => _sectionData.filledSlotsCount == 0;

        public event UnityAction<ItemSlotData, ItemSectionTemplate> OnItemSlotClick;

        IUISectionData<ItemSlotData> _sectionData;

        void OnDestroy()
        {
            if (_sectionData == null) return;
            _sectionData.OnSectionDataChange -= UpdateSectionLayout;
        }

        public void BindData(IUISectionData<ItemSlotData> sectionData, ItemSectionTemplate template)
        {
            _template = template;
            _sectionData = sectionData;
            _sectionData.OnSectionDataChange += UpdateSectionLayout;
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

        protected override void UpdateSectionLayout(IUILayout<ItemSlot> parent)
        {
            UpdateSectionTitle(_sectionData);
            foreach (var itemSlot in _sectionData)
            {
                var slot = parent.CreateLayoutElement(_itemSlotPrefab);
                slot.BindData(itemSlot);
            }
        }
    }
}