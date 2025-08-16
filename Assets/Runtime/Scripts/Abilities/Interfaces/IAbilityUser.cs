using UnityEngine;
using UnityEngine.Events;

namespace Abilities
{
    public interface IAbilityUser
    {
        T GetEntityComponent<T>();
        void SelectAbility(IAbilityContainer container);
        event UnityAction<IAbilityContainer> OnAbilitySelected;
	}
}