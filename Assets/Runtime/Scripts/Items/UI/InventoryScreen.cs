using System.Collections;
using UnityEngine;

namespace Items.UI
{
    public class InventoryScreen : ScreenWithSections<InventorySection>
    {
        [SerializeField] UIScreen _inventoryScreen;
        [SerializeField] Inventory _inventory;
        [SerializeField] ItemSectionTemplate[] _visibleSections;
        [SerializeField] InventorySection _sectionPrefab;
        [Header("UI Elements")]
        [SerializeField] ItemSectionsLayout _sectionsLayout;

        protected override IObserversController<InventorySection> _layout => _sectionsLayout;

        protected override void CreateSections()
        {
            _sectionsLayout.ClearLayout();

            foreach (var template in _visibleSections)
            {
                var sectionData = _inventory.GetSection(template);
                if (sectionData == null) continue;
                if (template.hideifEmpty && sectionData.isEmpty) continue;

                var section = _sectionsLayout.CreateLayoutElement(_sectionPrefab);
                section.BindData(sectionData, template);
            }
        }
    }
}