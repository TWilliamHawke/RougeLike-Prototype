using UnityEngine;

namespace Abilities
{
    public interface IAbilityTemplate : IIconData
    {
        AudioClip useSound { get; }
    }
}