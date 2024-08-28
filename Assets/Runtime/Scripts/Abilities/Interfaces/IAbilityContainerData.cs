using UnityEngine;

namespace Abilities
{
    public interface IAbilityContainerData
    {
        Sprite abilityIcon { get; }
        string displayName { get; }
        int numOfUses { get; }
    }
}