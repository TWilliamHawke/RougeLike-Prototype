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

namespace Core
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] InputController _inputController;
        [SerializeField] HealthbarController _healthbarCanvas;
        [SerializeField] DragElementsCanvas _dragElementsCanvas;
        [Header("UI Screens")]
        [SerializeField] TileInfoPanel _tileInfoPanel;
        [SerializeField] InventoryScreen _inventoryScreen;
        [SerializeField] SpellbookScreen _spellbookScreen;

        List<IUIScreen> _screens = new List<IUIScreen>();

        public void StartUp()
        {
            _healthbarCanvas.Init();
            _dragElementsCanvas.Init();


            _screens.Add(_tileInfoPanel);
            _screens.Add(_inventoryScreen);
            _screens.Add(_spellbookScreen);

            foreach (var screen in _screens)
            {
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