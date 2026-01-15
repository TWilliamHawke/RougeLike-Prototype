using Entities.PlayerScripts;
using UnityEngine;

namespace Abilities
{
    public class QuickBarSetupController : MonoBehaviour
    {
        [SerializeField] QuickBarSetupSlot _mainSlot;
        [SerializeField] QuickBarSetupSlot[] _quickSlots;

        [SerializeField] UIScreen _quickBarSetupScreen;
        [SerializeField] QuickBarDataStorage _quickBarDataStorage;

        [InjectField] Player _player;

        IAbilityContainer _selectedAbility;

        private void Awake()
        {
            _mainSlot.OnSlotClick += HandleMainSlotClick;

            for (int i = 0; i < _quickSlots.Length; i++)
            {
                _quickSlots[i].slotIndex = i;
                _quickSlots[i].OnSlotClick += HandleQuickSlotClick;
            }
        }

        public void OpenSetupScreen(IAbilityContainer selectedAbility)
        {
            _quickBarSetupScreen.Open();
            _selectedAbility = selectedAbility;

            _mainSlot.UpdateSlotGraphic(_quickBarDataStorage.mainAbility);

            for (int i = 0; i < _quickSlots.Length; i++)
            {
                _quickBarDataStorage.TryGetQuickAbility(i, out var ability);
                _quickSlots[i].UpdateSlotGraphic(ability);
            }
        }

        private void HandleQuickSlotClick(int index)
        {
            _quickBarDataStorage.SetQuickAbility(index, _selectedAbility);
            if (index < 0 || index >= _quickSlots.Length) return;
            _quickSlots[index].UpdateSlotGraphic(_selectedAbility);
            _quickBarSetupScreen.Close();
        }

        private void HandleMainSlotClick(int _)
        {
            var abilityController = _player.GetComponent<AbilityController>();
            //TODO: Add condition if selected ability can be used in main slot
            _quickBarDataStorage.SetMainAbility(_selectedAbility, abilityController);
            _mainSlot.UpdateSlotGraphic(_selectedAbility);
            _quickBarSetupScreen.Close();
        }
    }
}