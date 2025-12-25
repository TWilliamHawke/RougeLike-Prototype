using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Effect On Self")]
    public class SelfAbilityTemplate : AbilityTemplate, IEffectSource
    {
        [SerializeField] List<SourceEffectData> _effects;
        
        public override AbstractAbility CreateAbility()
        {
            SelfAbility ability = new(this);
            ability.BindEffectSource(this);
            abilityController.AddInjectionTarget(ability);
            return ability;
        }

        public IEnumerable<ISourceEffectData> GetEffects()
        {
            return _effects;
        }
    }
}