using System.Collections.Generic;

namespace Items.UI
{
    public class InventoryScreenController
    {
        Inventory _inventory;
        InventoryScreen _inventoryScreen;

        List<InventorySectionController> _inventorySectionControllers = new();


        public InventoryScreenController(Inventory inventory, InventoryScreen inventoryScreen)
        {
            _inventory = inventory;
            _inventoryScreen = inventoryScreen;
            _inventoryScreen.OnScreenOpen += UpdateInventoryScreen;

            _inventory.Init();
            _inventoryScreen.Init();

            _inventorySectionControllers.Add(new InventorySectionController(
                sectionData: _inventory.potionsBag,
                inventorySection: _inventoryScreen.potionsBag
            ));
            _inventorySectionControllers.Add(new InventorySectionController(
                sectionData: _inventory.scrollsBag,
                inventorySection: _inventoryScreen.scrollsBag
            ));
            _inventorySectionControllers.Add(new InventorySectionController(
                sectionData: _inventory.main,
                inventorySection: _inventoryScreen.mainSection
            ));
        }

        void UpdateInventoryScreen()
        {
            foreach (var controller in _inventorySectionControllers)
            {
                controller.UpdateSectionView();
            }
        }
    }
}


