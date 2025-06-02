using UnityEngine;

namespace Abilities
{
    public interface IAbility
    {
        Sprite abilityIcon { get; }
        string abilityName { get; }
    }
}