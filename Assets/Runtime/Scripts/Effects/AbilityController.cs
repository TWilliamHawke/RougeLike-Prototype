using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Effects
{
    public class AbilityController : MonoBehaviour
    {
		public Body _body;
		public event UnityAction<IAbilityWithTarget> OnTargetSelectionStart;
        public event UnityAction OnAbilityUse;

        IAbilityWithTarget _performedAbility;

        IEffectTarget _self;

        public void Init()
        {
            var success = TryGetComponent<IEffectTarget>(out _self);
			if(success) return;
			Debug.LogError("Object hasn't any components with IEffectTarget interface");
        }

        public void ApplyToSelf(SourceEffectData effect, IEffectSource effectSource)
        {
            effect.ApplyEffect(_self, effectSource);
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

        public void SelectTarget(IEffectTarget target)
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