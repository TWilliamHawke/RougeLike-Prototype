using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
	public class AbilityController : MonoBehaviour
	{
		IEffectTarget _self;

		public void Init()
		{
			TryGetComponent<IEffectTarget>(out _self);
		}

		public void ApplyToSelf(SourceEffectData effect)
		{
			effect.ApplyEffect(_self);
		}

		public void StartTargetSelection(IAbilityWithTarget ability)
		{
			
		}
	}
}