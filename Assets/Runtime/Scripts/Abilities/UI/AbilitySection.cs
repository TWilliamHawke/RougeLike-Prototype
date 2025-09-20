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

        List<IAbilityContainer> _abilityContainers = new();

        protected override bool _sectionDataIsEmpty => _abilityContainers.Count == 0;
        protected override UISectionHeader _header => _sectionHeader;
        protected override IUILayout _layout => _abilitySlotsLayout;
        protected override UILayoutWithObserver<AbilitySlot> _observerLayout => _abilitySlotsLayout;

        public event UnityAction<IAbilityContainer> OnAbilitySlotClick;


        public void AddToObserve(AbilitySlot target)
        {
            target.OnAbilitySelected += HandleSlotClick;
        }

        public void RemoveFromObserve(AbilitySlot target)
        {
            target.OnAbilitySelected -= HandleSlotClick;
        }

        protected override void FillLayout()
        {
            foreach (var ability in _abilityContainers)
            {
                var slot = _observerLayout.CreateLayoutElement(_abilitySlotPrefab);
                slot.BindData(ability);
            }
        }

        protected override void UpdateSectionLayout(IUILayout<AbilitySlot> parent)
        {
            throw new System.NotImplementedException();
        }

        private void HandleSlotClick(IAbilityContainer slotData)
        {
            OnAbilitySlotClick?.Invoke(slotData);
        }

    }
}