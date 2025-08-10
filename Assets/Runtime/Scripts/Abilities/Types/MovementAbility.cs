using UnityEngine;

namespace Abilities
{
    public class MovementAbility : AbstractAbility
    {
        protected override IIconData template => _template;

        MovementAbilityTemplate _template;

        [InjectField] MovementController _controller;

        public MovementAbility(MovementAbilityTemplate template)
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