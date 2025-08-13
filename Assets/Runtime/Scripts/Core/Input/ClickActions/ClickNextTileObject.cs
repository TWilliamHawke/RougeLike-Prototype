using System.Collections;
using System.Collections.Generic;
using Entities;
using Entities.PlayerScripts;
using Map;
using UnityEngine;

namespace Core.Input
{
    public class ClickNextTileObject : IClickAction
    {
        Player _player;


        public ClickNextTileObject(Player player)
        {
            _player = player;
        }

        public void ProcessClick(ITileClickData tile)
        {
            if (TryFindTarget(tile, out var target))
            {
                target.Interact(_player);
            }
        }

        public bool CanBeUsedOnTile(ITileClickData tile)
        {
            return TryFindTarget(tile, out _);
        }
        
        private bool TryFindTarget(ITileClickData tile, out IInteractive target)
        {
            target = null;

            foreach (var entity in tile.entitiesOnTile)
            {
                target = entity as IInteractive;
                if (target is not null)
                {
                    var targetPos = target.transform.position;
                    var playerPos = _player.transform.position;

                    return Vector3.SqrMagnitude(targetPos - playerPos) < 2.1;
                }
            }

            return false;

        }
	}
}