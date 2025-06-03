using UnityEngine;

namespace Abilities
{
    public abstract class AbstractAbility : IAbility
    {
        public Sprite abilityIcon => template.icon;
        public string abilityName => template.displayName;

        protected abstract IIconData template { get; }

        public abstract void Use(AbilityController abilityController);
    }
}