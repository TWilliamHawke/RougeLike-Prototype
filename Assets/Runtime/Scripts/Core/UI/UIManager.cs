using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using Entities.UI;
using Items.UI;
using Magic.UI;
using Core.Input;
using UI.DragAndDrop;
using X = UnityEngine.InputSystem.InputAction.CallbackContext;
using UI.Tooltips;
using Map.Objects.UI;
using Items;

namespace Core
{
    public class UIManager : MonoBehaviour, IInjectionTarget
    {
        [InjectField] InputController _inputController;

        [SerializeField] TooltipCanvas _tooltipCanvas;
        [Header("UI Screens")]
        [SerializeField] MainCanvas _mainCanvas;
        [SerializeField] InventoryScreen _inventoryScreen;
        [SerializeField] SpellbookScreen _spellbookScreen;
        [SerializeField] LootPanel _lootPanel;
        [SerializeField] ActionsScreen _actionsScreen;
        [Header("Injectors")]
        [SerializeField] Injector _inputControllerInjector;
        [Header("Data Objects")]
        [SerializeField] Inventory _inventory;

        List<IUIScreen> _screens = new List<IUIScreen>();

        InventoryScreenController _inventoryScreenController;

        bool IInjectionTarget.waitForAllDependencies => false;

        void Awake()
        {
            CreateControllers();
            _mainCanvas.Init();
            _tooltipCanvas.Init();
            _actionsScreen.Init();
            _lootPanel.Init();

            _screens.Add(_inventoryScreen);
            _screens.Add(_spellbookScreen);
            //_screens.Add(_lootPanel);

            _spellbookScreen.Init();

            _inputControllerInjector.AddInjectionTarget(this);
        }

        private void CreateControllers()
        {
            _inventoryScreenController = new InventoryScreenController(
                inventory: _inventory,
                inventoryScreen: _inventoryScreen
            );
        }

        private void OnDestroy()
        {
            if (_inputController is null) return;
            _inputController.main.Spellbook.performed -= ToggleSpellbook;
            _inputController.main.Inventory.performed -= ToggleInventory;
        }

        public void FinalizeInjection()
        {
            _inputController.main.Spellbook.performed += ToggleSpellbook;
            _inputController.main.Inventory.performed += ToggleInventory;
        }


        void ToggleScreen(IUIScreen targetScreen)
        {
            foreach (var screen in _screens)
            {
                if (screen != targetScreen)
                {
                    screen.gameObject.SetActive(false);
                }
                else
                {
                    screen.Toggle();
                }
            }

        }

        void ToggleSpellbook(X _) => ToggleScreen(_spellbookScreen);
        void ToggleInventory(X _) => ToggleScreen(_inventoryScreen);

    }
}