using UnityEngine;

namespace Abilities
{
    public class AoeAbility : AbstractAbility
    {
        protected override IIconData template => _template;

        AoeAbilityTemplate _template;

        [InjectField] AoeAbilityController _controller;

        public AoeAbility(AoeAbilityTemplate template)
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