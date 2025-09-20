using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Abilities
{
    public class AbilitySection : UISection<IAbilityContainer, AbilitySlot>, IObserver<AbilitySlot>
    {
        [SerializeField] UISectionHeader _sectionHeader;
        [SerializeField] AbilitySlotsLayout _abilitySlotsLayout;
        [SerializeField] AbilitySlot _abilitySlotPrefab;

        protected override UISectionHeader _header => _sectionHeader;
        protected override IUILayout _layout => _abilitySlotsLayout;
        protected override UILayoutWithObserver<AbilitySlot> _observerLayout => _abilitySlotsLayout;

        protected override AbilitySlot _slotPrefab => _abilitySlotPrefab;

        public event UnityAction<IAbilityContainer> OnAbilitySlotClick;

        public override void AddToObserve(AbilitySlot target)
        {
            target.OnAbilitySelected += HandleSlotClick;
        }

        public override void RemoveFromObserve(AbilitySlot target)
        {
            target.OnAbilitySelected -= HandleSlotClick;
        }

        private void HandleSlotClick(IAbilityContainer slotData)
        {
            OnAbilitySlotClick?.Invoke(slotData);
        }

    }
}