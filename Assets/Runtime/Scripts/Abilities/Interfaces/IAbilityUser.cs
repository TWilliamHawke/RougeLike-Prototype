using UnityEngine.Events;

namespace Abilities
{
    public interface IAbilityUser
    {
        T GetComponent<T>();
        void SelectAbility(IAbilityContainer container);
        event UnityAction<IAbilityContainer> OnAbilitySelected;
	}
}