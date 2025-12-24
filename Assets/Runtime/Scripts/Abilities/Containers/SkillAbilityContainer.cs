using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

namespace Abilities
{
    public class SkillAbilityContainer : AbilityContainer
    {
        public override bool canBeUsed => throw new System.NotImplementedException();
        protected override IAbility ability => _ability;
        
        IAbility _ability { get; init; }

        public override bool TileHasValidTarget(ITileClickData tile)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateAbilityCounter(IAbilityCounterHandler handler)
        {
            throw new System.NotImplementedException();
        }

        public override void UseAbility(IAbilityTarget target)
        {
            throw new System.NotImplementedException();
        }
    }

}