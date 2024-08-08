using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Items.UI
{
    // obsolete : used in desktop ui version
    public class InventorySection : UILayoutWithObserver<ItemSlotData, ItemSlot>
    {
        [SerializeField] ItemSectionHeader _sectionHeader;

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

        public void BindSection(IInventorySectionData sectionData)
        {
            _sectionData = sectionData;
            _sectionData.OnSectionDataChange += UpdateSectionView;
        }

        public void UpdateLayout(IInventorySectionData section)
        {
            base.UpdateLayout(section);
            _sectionHeader.ReplaceTitle(section);
        }

        public void Collapse()
        {
            SetLayoutActive(false);
            _sectionHeader.ShowCollapcePointer();
            _isCollapsed = true;
        }

        public void Expand()
        {
            SetLayoutActive(true);
            _sectionHeader.ShowExpandPointer();
            _isCollapsed = false;
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