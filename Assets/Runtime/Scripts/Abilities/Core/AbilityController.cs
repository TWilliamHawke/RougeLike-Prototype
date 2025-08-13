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
        public Body _body;

        public Vector3Int position => transform.position.ToInt();

        public event UnityAction<IAbilityContainer> OnAbilitySelected;

        public void PlaySound(AudioClip sound)
        {
            _body.PlaySound(sound);
        }

        public void SelectAbility(IAbilityContainer container)
        {
            OnAbilitySelected?.Invoke(container);
        }
    }
}