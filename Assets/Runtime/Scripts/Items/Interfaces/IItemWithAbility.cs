using Abilities;
using UnityEngine;

namespace Items
{
    public interface IItemWithAbility
    {
        IAbilityContainer CreateAbilityContainer(IAbilitiesFactory factory);
    }
}