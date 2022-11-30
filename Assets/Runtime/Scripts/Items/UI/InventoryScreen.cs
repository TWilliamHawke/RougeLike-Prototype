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
        [SerializeField] Inventory _inventory;
        [Header("UI Elements")]
        [SerializeField] ContextMenu _contextMenu;
        [SerializeField] InventorySection _potionsBag;
        [SerializeField] InventorySection _scrollsBag;
        [SerializeField] InventorySection _mainSection;

        List<InventorySectionController> _inventorySectionControllers = new();

        private void Awake()
        {
            _inventory.Init();
            CreateControllers();
        }

        void OnEnable()
        {
            _contextMenu.TryInit();
            foreach (var controller in _inventorySectionControllers)
            {
                controller.UpdateSectionView();
            }
        }

        private void CreateControllers()
        {
            _inventorySectionControllers.Add(
                new InventorySectionController(
                    sectionData: _inventory.potionsBag,
                    inventorySection: _potionsBag
            ));
            _inventorySectionControllers.Add(
                new InventorySectionController(
                    sectionData: _inventory.scrollsBag,
                    inventorySection: _scrollsBag
            ));
            _inventorySectionControllers.Add(
                new InventorySectionController(
                    sectionData: _inventory.main,
                    inventorySection: _mainSection
            ));
        }


    }
}