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
        [SerializeField] InventorySectionController _potionsBag;
        [SerializeField] InventorySectionController _scrollsBag;
        [SerializeField] InventorySectionController _mainSection;
        [SerializeField] ResourceCounter _goldCounter;
        [SerializeField] ResourceCounter _dustCounter;

        List<InventorySectionController> _controllers = new();

        void Awake()
        {
            _controllers.Add(_potionsBag);
            _controllers.Add(_mainSection);
            _controllers.Add(_scrollsBag);
        }

        void Start()
        {
            _inventoryScreen.OnScreenOpen += UpdateInventoryScreen;
            _inventory.resources.OnResourceChange += UpdateResources;
            _potionsBag.BindSection(_inventory.potionsBag);
            _mainSection.BindSection(_inventory.main);
            _scrollsBag.BindSection(_inventory.scrollsBag);
        }

        public void AddSlotObservers(IObserver<ItemSlot> observer)
        {
            _controllers.ForEach(controller => controller.AddSlotObservers(observer));
        }

        private void UpdateInventoryScreen()
        {
            foreach (var controller in _controllers)
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

    }
}