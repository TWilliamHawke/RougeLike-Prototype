using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class SummonAbility : AbstractAbility
    {
        protected override IIconData template => _template;

        SummonAbilityTemplate _template;
        [InjectField] SummonController _controller;

        public SummonAbility(SummonAbilityTemplate template)
        {
            _template = template;
        }

        public override void UseOn(IAbilityTarget target)
        {
            throw new System.NotImplementedException();
        }

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            throw new System.NotImplementedException();
        }
    }
}