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
		public event UnityAction<AbilityController> OnTargetSelectionStart;

        IEffectTarget _self;

        public void Init()
        {
            var success = TryGetComponent<IEffectTarget>(out _self);
			if(success) return;
			Debug.LogError("Object hasn't any components with IEffectTarget interface");
        }

        public void ApplyToSelf(SourceEffectData effect)
        {
            effect.ApplyEffect(_self);
        }

        public void StartTargetSelection(IAbilityWithTarget ability)
        {
			OnTargetSelectionStart?.Invoke(this);
        }

        public void SelectTarget(IEffectTarget target)
        {

        }

		public bool TrySpendMana(int count)
		{
			return true;
		}

		public void PlaySound(AudioClip sound)
		{
			_body.PlaySound(sound);
		}
    }
}