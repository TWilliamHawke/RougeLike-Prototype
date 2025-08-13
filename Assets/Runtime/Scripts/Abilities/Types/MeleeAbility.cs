using Map;
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

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            throw new System.NotImplementedException();
        }

        public override bool TileHasValidTarget(IAbilityUser user, ITileClickData tile)
        {
            float deltaX = user.position.x - tile.position.x;
            float deltaY = user.position.y - tile.position.y;
            return Mathf.Abs(deltaX) <= 1 && Mathf.Abs(deltaY) <= 1;
        }

        public override void Use(IAbilityUser user, IAbilityTarget target)
        {
            throw new System.NotImplementedException();
        }

        public override IAbilityTarget SelectTarget(ITileClickData tile)
        {
            throw new System.NotImplementedException();
        }
    }
}