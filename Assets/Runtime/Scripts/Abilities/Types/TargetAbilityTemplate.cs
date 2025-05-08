using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Abilities
{
    public class TargetAbilityTemplate : AbilityTemplate, IAbilityWithTarget, IEffectSource
    {
        [SerializeField] List<SourceEffectData> _effects;

        public bool TargetIsValid(IAbilityTarget target)
        {
            return true;
        }

        public override void SelectAbilityController(AbilityController controller)
        {
            controller.StartTargetSelection(this);
        }

        public void UseOnTarget(AbilityController _, IAbilityTarget target)
        {
            var effectsStorage = target.GetComponent<EffectsStorage>();
			foreach (var effect in _effects)
			{
				effect.ApplyEffect(effectsStorage, this);
			}
        }

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            throw new System.NotImplementedException();
        }
    }
}