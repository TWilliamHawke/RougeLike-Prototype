using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class TargetEffectAbility : Ability, IAbilityWithTarget, IEffectSource
    {
        [SerializeField] List<SourceEffectData> _effects;

        public bool TargetIsValid(IEffectTarget target)
        {
            return true;
        }

        public override void SelectControllerUsage(AbilityController controller)
        {
            controller.StartTargetSelection(this);
        }

        public void UseOnTarget(AbilityController _, IEffectTarget target)
        {
			foreach (var effect in _effects)
			{
				effect.ApplyEffect(target, this);
			}
        }

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            throw new System.NotImplementedException();
        }
    }
}