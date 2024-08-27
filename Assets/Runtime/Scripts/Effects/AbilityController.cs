using System.Collections;
using System.Collections.Generic;
using Effects;
using Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Abilities
{
    [RequireComponent(typeof(EffectsStorage))]
    public class AbilityController : MonoBehaviour, IEntityComponent
    {
		public Body _body;
		public event UnityAction<IAbilityWithTarget> OnTargetSelectionStart;
        public event UnityAction OnAbilityUse;

        IAbilityWithTarget _performedAbility;

        EffectsStorage _effectsStorage;

        public void Init()
        {
            _effectsStorage = GetComponent<EffectsStorage>();
        }

        public void ApplyToSelf(SourceEffectData effect, IEffectSource effectSource)
        {
            effect.ApplyEffect(_effectsStorage, effectSource);
            OnAbilityUse?.Invoke();
        }

        public void AddToEffectStorage(IEffectSource effectSource)
        {

        }

        public void StartTargetSelection(IAbilityWithTarget ability)
        {
            _performedAbility = ability;
			OnTargetSelectionStart?.Invoke(ability);
        }

        public void SelectTarget(IAbilityTarget target)
        {
            _performedAbility.UseOnTarget(this, target);
            OnAbilityUse?.Invoke();
        }

		public void PlaySound(AudioClip sound)
		{
			_body.PlaySound(sound);
		}
    }
}