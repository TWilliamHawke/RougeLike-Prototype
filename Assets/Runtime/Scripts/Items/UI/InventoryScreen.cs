using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.DragAndDrop;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Items.UI
{
    public class InventoryScreen : MonoBehaviour, IObserver<InventorySection>
    {
        [SerializeField] UIScreen _inventoryScreen;
        [SerializeField] Inventory _inventory;
        [SerializeField] ItemSectionTemplate[] _visibleSections;
        [Header("UI Elements")]
        [SerializeField] SectionsLayout _sectionsLayout;

        HashSet<InventorySection> _sections = new();

        void Awake()
        {
            _sectionsLayout.AddObserver(this);

            _sections.ForEach(s => s.OnSectionSelect += ToggleSection);
        }

        void Start()
        {
            _inventoryScreen.OnScreenOpen += SetDefaultScreenView;
            _inventoryScreen.OnScreenClose += _inventory.ClearTempStorage;

            _sectionsLayout.UpdateLayout(GetVisibleSections());
        }

        public void AddSlotObservers(IObserver<ItemSlot> observer)
        {
            _sections.ForEach(s => s.AddObserver(observer));
        }

        private void SetDefaultScreenView()
        {
            _sections.ForEach(s => s.UpdateSectionView());
            _sections.ForEach(s => s.Collapse());
        }

        private void ToggleSection(InventorySection selectedSection)
        {
            foreach (var section in _sections)
            {
                if (section == selectedSection)
                {
                    section.Toggle();
                    return;
                }
                section.Collapse();
            }
        }

        private IEnumerable<IInventorySectionData> GetVisibleSections()
        {
            foreach (var template in _visibleSections)
            {
                var section = _inventory.GetSection(template);
                if (section == null) continue;
                if (template.hideifEmpty && section.isEmpty) continue;
                yield return section;
            }
        }

        void IObserver<InventorySection>.AddToObserve(InventorySection target)
        {
            target.OnSectionSelect += ToggleSection;
            _sections.Add(target);
        }

        void IObserver<InventorySection>.RemoveFromObserve(InventorySection target)
        {
            target.OnSectionSelect -= ToggleSection;
            _sections.Remove(target);
        }
    }
}