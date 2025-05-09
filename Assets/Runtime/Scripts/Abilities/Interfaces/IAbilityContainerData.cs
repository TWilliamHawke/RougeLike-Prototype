using UnityEngine;

namespace Abilities
{
    public interface IAbilityContainerData : IIconData
    {
        int numOfUses { get; }
    }
}