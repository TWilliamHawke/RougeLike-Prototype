using UnityEngine;

namespace Abilities
{
    public interface IAbility
    {
        Sprite icon { get; }
        string displayName { get; }
        void UseOn(IAbilityTarget target);
        void Select(IAbilityUser user, IAbilityContainer container);
        string GetDescription(AbilityModifiers abilityModifiers);
    }
}