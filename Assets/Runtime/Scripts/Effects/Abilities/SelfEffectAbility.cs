using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName ="Ability", menuName ="Abilities/Effect On Self")]
    public class SelfEffectAbility : Ability
    {
		[SerializeField] List<SourceEffectData> _effects;


        public override void Use(AbilityController controller)
        {
            foreach (var effectData in _effects)
			{
				controller.ApplyToSelf(effectData);
			}
        }
    }
}