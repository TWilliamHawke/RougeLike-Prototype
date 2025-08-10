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

        public override void UseBy(AbilityController abilityController)
        {
            throw new System.NotImplementedException();
        }

        public override void Select(IAbilityTrigger trigger)
        {
            trigger.TriggerSelectionEvent();
        }

        public override void UseOn(IAbilityTarget target)
        {
            throw new System.NotImplementedException();
        }
    }
}