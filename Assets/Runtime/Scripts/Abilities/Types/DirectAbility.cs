using System.Linq;
using Map;
using UnityEngine;

namespace Abilities
{
    public class DirectAbility : AbstractAbility
    {
        protected override IIconData template => _template;

        DirectAbilityTemplate _template;

        [InjectField] DirectAbilityController _controller;

        public DirectAbility(DirectAbilityTemplate template)
        {
            _template = template;
        }

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            throw new System.NotImplementedException();
        }

        public override bool TileHasValidTarget(IAbilityUser _, ITileClickData tile)
        {
            return tile.entitiesOnTile.Any(entity => entity is IAbilityTarget);
        }

        public override void Use(IAbilityUser user, IAbilityTarget target)
        {
            _controller.ApplyEffects(_template.GetEffects(), target, _template);
        }

        public override IAbilityTarget SelectTarget(ITileClickData tile)
        {
            return tile.entitiesOnTile
                .First(entity => entity is IAbilityTarget) as IAbilityTarget;
        }
    }
}