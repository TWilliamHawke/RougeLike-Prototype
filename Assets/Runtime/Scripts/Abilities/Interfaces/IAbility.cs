using UnityEngine;

namespace Abilities
{
    public interface IAbility
    {
        Sprite icon { get; }
        string displayName { get; }
        void UseBy(AbilityController abilityController);
        void UseOn(IAbilityTarget target);
        void Select(IAbilityTrigger trigger);
    }
}