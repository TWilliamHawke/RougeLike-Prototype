using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Entities.PlayerScripts;

namespace Core.Input
{
	public class ClickPlayer : IMouseClickAction
	{

        InputController _inputController;
        Player _player;

        public ClickPlayer(InputController inputController, Player player)
        {
            _inputController = inputController;
            _player = player;
        }

        void IMouseClickAction.ProcessClick()
        {
            _player.EndTurn();
        }

        bool IMouseClickAction.Condition()
        {
            return _inputController.hoveredTileHits.Any(hit => hit.collider.GetComponent<Player>());
        }

	}
}