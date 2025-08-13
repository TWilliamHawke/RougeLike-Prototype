using UnityEngine;
using UnityEngine.Events;

namespace Abilities
{
    public interface IAbilityUser
    {
        T GetComponent<T>();
        void SelectAbility(IAbilityContainer container);
        event UnityAction<IAbilityContainer> OnAbilitySelected;
        Vector3Int position { get; }
        void PlaySound(AudioClip sound);
	}
}