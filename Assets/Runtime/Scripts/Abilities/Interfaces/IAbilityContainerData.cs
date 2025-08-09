using UnityEngine;

namespace Abilities
{
    public interface IAbilityContainerData : IIconData
    {
        void UpdateAbilityButton(IAbilityCounterHandler handler);
    }
}