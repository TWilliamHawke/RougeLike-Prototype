using UnityEngine;

namespace Abilities
{
    public abstract class AbstractAbility : IAbility, IInjectionTarget
    {
        public Sprite icon => template.icon;
        public string displayName => template.displayName;

        public bool waitForAllDependencies => false;

        protected abstract IIconData template { get; }

        public abstract string GetDescription(AbilityModifiers abilityModifiers);
        public abstract void UseOn(IAbilityTarget target);

        public virtual void Select(IAbilityUser user, IAbilityContainer container)
        {
            user.SelectAbility(container);
        }
    }
}