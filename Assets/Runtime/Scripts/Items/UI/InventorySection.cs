using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Items.UI
{
    public class InventorySection : UIDataElement<IInventorySectionData>
    {
        [SerializeField] ItemSectionHeader _sectionHeader;
        [SerializeField] ItemSlotsLayout _itemSlotList;

        IInventorySectionData _sectionData;
        bool _isCollapsed = true;

        public event UnityAction<InventorySection> OnSectionSelect;

        void Start()
        {
            _sectionHeader.OnClick += SelectSection;
        }

        void OnDestroy()
        {
            _sectionData.OnSectionDataChange -= UpdateSectionView;
        }

        public override void BindData(IInventorySectionData sectionData)
        {
            _sectionData = sectionData;
            _sectionData.OnSectionDataChange += UpdateSectionView;
        }

        public void UpdateLayout(IInventorySectionData section)
        {
            _itemSlotList.UpdateLayout(section);
            _sectionHeader.ReplaceTitle(section);
        }

        public void UpdateLayout(IEnumerable<ItemSlotData> items)
        {
            _itemSlotList.UpdateLayout(items);
        }

        public void Collapse()
        {
            _itemSlotList.HideLayout();
            _sectionHeader.ShowCollapcePointer();
            _isCollapsed = true;
        }

        public void Expand()
        {
            _itemSlotList.ShowLayout();
            _sectionHeader.ShowExpandPointer();
            _isCollapsed = false;
        }

        public void AddObserver(IObserver<ItemSlot> observer)
        {
            _itemSlotList.AddObserver(observer);
        }

        public void Toggle()
        {
            if (_isCollapsed)
            {
                Expand();
                return;
            }
            Collapse();
        }

        public void UpdateSectionView()
        {
            UpdateLayout(_sectionData);
        }

        private void SelectSection()
        {
            if (_sectionData?.count == 0) return;
            OnSectionSelect?.Invoke(this);
        }
    }
}