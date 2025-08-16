using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Effects;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Effect On Self")]
    public class SelfAbilityTemplate : AbilityTemplate, IEffectSource
    {
        [SerializeField] List<SourceEffectData> _effects;
        
        public override IAbility CreateAbility()
        {
            SelfAbility ability = new(this);
            abilityController.AddInjectionTarget(ability);
            return ability;
        }

        public IEnumerable<SourceEffectData> GetEffects()
        {
            return _effects;
        }
    }
}