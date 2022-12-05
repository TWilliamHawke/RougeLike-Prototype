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
        [InjectField] Inventory _inventory;
        [Header("UI Elements")]
        [SerializeField] InventorySection _potionsBag;
        [SerializeField] InventorySection _scrollsBag;
        [SerializeField] InventorySection _mainSection;
        [SerializeField] ResourceCounter _goldCounter;
        [SerializeField] ResourceCounter _dustCounter;

        List<InventorySectionController> _inventorySectionControllers = new();

        //event handler in editor
        public void Init()
        {
            _inventoryScreen.OnScreenOpen += UpdateInventoryScreen;
            _inventory.resources.OnResourceChange += UpdateResources;
            CreateControllers();
        }

        private void UpdateInventoryScreen()
        {
            foreach (var controller in _inventorySectionControllers)
            {
                controller.UpdateSectionView();
            }
        }

        private void OnDestroy()
        {
            _inventory.resources.OnResourceChange -= UpdateResources;
        }

        private void UpdateResources(ResourceType resourceType)
        {
            _goldCounter.UpdateResourceValue(resourceType);
            _dustCounter.UpdateResourceValue(resourceType);
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