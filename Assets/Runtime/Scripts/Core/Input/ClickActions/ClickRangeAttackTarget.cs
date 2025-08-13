using System.Collections;
using System.Collections.Generic;
using Entities;
using Entities.Behavior;
using Entities.Combat;
using UnityEngine;
using Abilities;
using Map;

namespace Core.Input
{
    public class ClickRangeAttackTarget : IClickAction
    {
        ProjectileController _player;

        public ClickRangeAttackTarget(ProjectileController player)
        {
            _player = player;
        }

        public bool CanBeUsedOnTile(ITileClickData tile)
        {
            return TryFindTarget(tile, out _);
        }

        public void ProcessClick(ITileClickData tile)
        {
            if (TryFindTarget(tile, out var target))
            {
                _player.ThrowProjectile(target);
            }
        }
        
        private bool TryFindTarget(ITileClickData tile, out IRangeAttackTarget target)
        {
            target = null;

            foreach (var entity in tile.entitiesOnTile)
            {
                target = entity as IRangeAttackTarget;
                if (target is null) continue;
                var aggr = entity.GetEntityComponent<IFactionMember>()?.behavior;
                if (aggr == BehaviorType.agressive) return true;
            }

            return false;
        }
    }
}