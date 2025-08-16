using Entities;
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
            var positionData = user.GetEntityComponent<PositionController>();
            if (positionData == null) return false;
            float deltaX = positionData.position.x - tile.intPosition.x;
            float deltaY = positionData.position.y - tile.intPosition.y;
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