using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.DragAndDrop;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Items.UI
{
    public class InventoryScreen : MonoBehaviour
    {
        [SerializeField] UIScreen _inventoryScreen;
        [SerializeField] Inventory _inventory;
        [Header("UI Elements")]
        [SerializeField] InventorySection _tempSection;
        [SerializeField] InventorySection _potionsBag;
        [SerializeField] InventorySection _scrollsBag;
        [SerializeField] InventorySection _mainSection;

        List<InventorySection> _sections = new();

        void Awake()
        {
            _sections.Add(_tempSection);
            _sections.Add(_potionsBag);
            _sections.Add(_mainSection);
            _sections.Add(_scrollsBag);

            _sections.ForEach(s => s.OnSectionSelect += ToggleSection);
        }

        void Start()
        {
            _inventoryScreen.OnScreenOpen += SetDefaultScreenView;
            _inventoryScreen.OnScreenClose += _inventory.ClearTempStorage;

            _potionsBag.BindSection(_inventory.potionsBag);
            _mainSection.BindSection(_inventory.main);
            _scrollsBag.BindSection(_inventory.scrollsBag);
            _tempSection.BindSection(_inventory.tempStorage);
        }

        public void AddSlotObservers(IObserver<ItemSlot> observer)
        {
            _sections.ForEach(s => s.AddObserver(observer));
        }

        private void SetDefaultScreenView()
        {
            _sections.ForEach(s => s.UpdateSectionView());
            _sections.ForEach(s => s.Collapse());

            if (!_inventory.tempStorage.isEmpty)
            {
                _tempSection.Expand();
            }
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

    }
}