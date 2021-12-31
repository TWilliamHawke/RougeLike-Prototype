using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class TargetEffectAbility : Ability, IAbilityWithTarget
    {
        [SerializeField] List<SourceEffectData> _effects;

        public bool TargetIsValid(IEffectTarget target)
        {
            return true;
        }

        public override void Use(AbilityController controller)
        {
            controller.StartTargetSelection(this);
        }

        public void UseOnTarget(AbilityController _, IEffectTarget target)
        {
			foreach (var effect in _effects)
			{
				effect.ApplyEffect(target);
			}
        }
    }
}