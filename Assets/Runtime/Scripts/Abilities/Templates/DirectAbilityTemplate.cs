using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Direct")]
    public class DirectAbilityTemplate : AbilityTemplate, IEffectSource
    {
        [SerializeField] List<SourceEffectData> _effects;

        public override IAbility CreateAbility()
        {
            DirectAbility ability = new(this);
            abilityController.AddInjectionTarget(ability);
            return ability;
        }

        public IEnumerable<SourceEffectData> GetEffects()
        {
            return _effects;
        }
    }
}