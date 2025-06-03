using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class SummonAbility : AbstractAbility
    {
        protected override IIconData template => _template;

        SummonAbilityTemplate _template { get; init; }

        public SummonAbility(SummonAbilityTemplate template)
        {
            _template = template;
        }

        public override void Use(AbilityController abilityController)
        {
            throw new System.NotImplementedException();
        }
    }
}