using System.Collections;
using System.Collections.Generic;
using Effects;
using Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Abilities
{
    [RequireComponent(typeof(EffectsStorage))]
    public class AbilityController : MonoBehaviour, IEntityComponent, IAbilityUser
    {
        public event UnityAction<IAbilityContainer> OnAbilitySelected;

        public T GetEntityComponent<T>()
        {
            return GetComponent<T>();
        }

        public void SelectAbility(IAbilityContainer container)
        {
            OnAbilitySelected?.Invoke(container);
        }
    }
}