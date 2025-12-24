using UnityEngine;

namespace Abilities
{
    public interface IAbilityContainerData : IIconData
    {
        void UpdateAbilityCounter(IAbilityCounterHandler handler);
    }
}