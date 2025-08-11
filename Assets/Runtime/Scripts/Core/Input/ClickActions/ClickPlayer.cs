using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Entities.PlayerScripts;

namespace Core.Input
{
	public class ClickPlayer : IClickAction
	{

        InputController _inputController;
        Player _player;

        public ClickPlayer(InputController inputController, Player player)
        {
            _inputController = inputController;
            _player = player;
        }

        void IClickAction.ProcessClick()
        {
            _player.EndTurn();
        }

        bool IClickAction.Condition()
        {
            return _inputController.hoveredTileHits.Any(hit => hit.collider.GetComponent<Player>());
        }

	}
}