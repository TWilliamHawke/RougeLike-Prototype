using UnityEngine;

namespace Abilities
{
    public interface IAbility
    {
        Sprite abilityIcon { get; }
        string abilityName { get; }
        void UseBy(AbilityController abilityController);
        void UseOn(IAbilityTarget target);
        void Select();
    }
}