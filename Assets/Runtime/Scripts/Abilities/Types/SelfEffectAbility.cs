using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Effects;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Effect On Self")]
    public class SelfEffectAbility : Ability, IEffectSource
    {
        [SerializeField] List<SourceEffectData> _effects;

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            var sb = new StringBuilder();
            string pattern1 = @"%m";

            foreach(var effectData in _effects)
            {
                var magnitude = effectData.power * abilityModifiers.magnitudeMult;
                var realDescription = Regex.Replace(effectData.effect.description, pattern1, magnitude.ToString());
                sb.AppendLine(realDescription);
            }

            return sb.ToString();
        }

        public override void SelectControllerUsage(AbilityController controller)
        {
            foreach (var effectData in _effects)
            {
                controller.ApplyToSelf(effectData, this);
            }
        }
    }
}