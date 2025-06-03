using Abilities;
using UnityEngine;

namespace Items
{
    public interface IItemWithAbility
    {
        IAbility CreateAbility();
    }
}