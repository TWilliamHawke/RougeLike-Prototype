using UnityEngine;

namespace Abilities
{
    public class MainAbilitySelector : MonoBehaviour, IObserver<AbilitySection>
    {
        [SerializeField] MainAbilityScreen _abilityScreen;
        [SerializeField] QuickBarDataStorage _quickBar;

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
            _quickBar.SetMainAbility(container);
            _abilityScreen.CloseScreen();
        }

    }
}