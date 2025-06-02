using UnityEngine;

namespace Abilities
{
    public abstract class AbstractAbility : IAbility
    {
        public Sprite abilityIcon => template.icon;
        public string abilityName => template.displayName;

        protected abstract AbilityTemplate template { get; }
    }
}