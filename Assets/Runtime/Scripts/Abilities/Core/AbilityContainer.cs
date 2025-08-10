using UnityEngine;
using UnityEngine.Events;

namespace Abilities
{
    public abstract class AbilityContainer : IAbilityContainer, IAbilityTrigger
    {
        public static event UnityAction<IAbilityContainer> OnAbilitySelection;

        public abstract bool canBeUsed { get; }
        public virtual string displayName => _ability.abilityName;
        public virtual Sprite icon => _ability.abilityIcon;

        protected IAbility _ability { get; init; }

        public abstract void UpdateAbilityButton(IAbilityCounterHandler handler);
        public abstract void UseAbility(AbilityController controller);

        public void Select()
        {
            _ability.Select(this);
        }

        public void TriggerSelectionEvent()
        {
            OnAbilitySelection?.Invoke(this);
        }
    }
}