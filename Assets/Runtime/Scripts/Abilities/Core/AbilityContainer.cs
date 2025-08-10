using UnityEngine;
using UnityEngine.Events;

namespace Abilities
{
    public abstract class AbilityContainer : IAbilityContainer
    {
        public abstract bool canBeUsed { get; }
        public virtual string displayName => ability.displayName;
        public virtual Sprite icon => ability.icon;

        protected abstract IAbility ability { get; }

        public abstract void UpdateAbilityButton(IAbilityCounterHandler handler);
        public abstract void UseAbility(IAbilityTarget target);

        public void SelectBy(IAbilityUser user)
        {
            ability.Select(user, this);
        }
    }
}