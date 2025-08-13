using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Entities.PlayerScripts;
using Map;

namespace Core.Input
{
	public class ClickPlayer : IClickAction
	{

        Player _player;

        public ClickPlayer(Player player)
        {
            _player = player;
        }

        public void ProcessClick(ITileClickData tile)
        {
            _player.EndTurn();
        }

        public bool CanBeUsedOnTile(ITileClickData tile)
        {
            return tile.entitiesOnTile.Any(entity => entity is Player);
        }

	}
}