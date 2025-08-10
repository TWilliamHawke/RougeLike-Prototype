using UnityEngine;

namespace Abilities
{
    public abstract class AbstractAbility : IAbility, IInjectionTarget
    {
        public Sprite abilityIcon => template.icon;
        public string abilityName => template.displayName;

        public bool waitForAllDependencies => false;

        protected abstract IIconData template { get; }

        public abstract void Select(IAbilityTrigger trigger);
        public abstract void UseBy(AbilityController abilityController);
        public abstract void UseOn(IAbilityTarget target);
    }
}