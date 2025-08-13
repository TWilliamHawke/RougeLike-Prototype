using System.Collections;
using System.Collections.Generic;
using Map;
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

        public override void Use(IAbilityUser user, IAbilityTarget tile)
        {
            throw new System.NotImplementedException();
        }

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            throw new System.NotImplementedException();
        }

        public override bool TileHasValidTarget(IAbilityUser _, ITileClickData tile)
        {
            return tile.isWalkableAndEmpty;
        }

        public override IAbilityTarget SelectTarget(ITileClickData tile)
        {
            throw new System.NotImplementedException();
        }
    }
}