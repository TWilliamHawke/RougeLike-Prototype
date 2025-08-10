using UnityEngine;

namespace Abilities
{
    public class MeleeAbility : AbstractAbility
    {
        protected override IIconData template => _template;

        MeleeAbilityTemplate _template;

        [InjectField] MeleeAttackController _controller;

        public MeleeAbility(MeleeAbilityTemplate template)
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