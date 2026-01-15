using Entities.PlayerScripts;
using UnityEngine;

namespace Abilities
{
    public class MainAbilitySelector : MonoBehaviour, IObserver<AbilitySection>
    {
        [SerializeField] MainAbilityScreen _abilityScreen;
        [SerializeField] QuickBarDataStorage _quickBar;

        [InjectField] Player _player;

        void Awake()
        {
            _abilityScreen.AddSectionObserver(this);
        }

        public void AddToObserve(AbilitySection target)
        {
            target.OnAbilitySlotClick += SelectAbility;
        }

        public void RemoveFromObserve(AbilitySection target)
        {
            target.OnAbilitySlotClick -= SelectAbility;
        }

        private void SelectAbility(IAbilityContainer container)
        {
            var abilityController = _player.GetComponent<AbilityController>();
            _quickBar.SetMainAbility(container, abilityController);
            _abilityScreen.CloseScreen();
        }

    }
}