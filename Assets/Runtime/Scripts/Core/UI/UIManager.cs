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

namespace Core
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] InputController _inputController;
        [SerializeField] HealthbarController _healthbarCanvas;
        [SerializeField] DragElementsCanvas _dragElementsCanvas;
        [SerializeField] TooltipCanvas _tooltipCanvas;
        [Header("UI Screens")]
        [SerializeField] MainCanvas _mainCanvas;
        [SerializeField] InventoryScreen _inventoryScreen;
        [SerializeField] SpellbookScreen _spellbookScreen;
        [SerializeField] LootPanel _lootPanel;

        List<IUIScreen> _screens = new List<IUIScreen>();

        public void StartUp()
        {
            _healthbarCanvas.Init();
            _dragElementsCanvas.Init();
            _mainCanvas.Init();
            _tooltipCanvas.Init();


            _screens.Add(_inventoryScreen);
            _screens.Add(_spellbookScreen);
            _screens.Add(_lootPanel);

            foreach (var screen in _screens)
            {
                screen.gameObject.SetActive(false);
                screen.Init();
            }

            _inputController.main.Spellbook.performed += ToggleSpellbook;
            _inputController.main.Inventory.performed += ToggleInventory;
        }

        private void OnDestroy()
        {
            _inputController.main.Spellbook.performed -= ToggleSpellbook;
            _inputController.main.Inventory.performed -= ToggleInventory;

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
                    bool state = screen.gameObject.activeSelf;
                    screen.gameObject.SetActive(!state);
                }
            }

        }

        void ToggleSpellbook(X _) => ToggleScreen(_spellbookScreen);
        void ToggleInventory(X _) => ToggleScreen(_inventoryScreen);
    }
}