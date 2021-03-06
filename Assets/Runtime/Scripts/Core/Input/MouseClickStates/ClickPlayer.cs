using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Entities.Player;

namespace Core.Input
{
	public class ClickPlayer : IMouseClickState
	{

        InputController _inputController;

        public ClickPlayer(GameObjects gameObjects)
        {
            _inputController = gameObjects.inputController;
        }

        void IMouseClickState.ProcessClick()
        {
            //skip turn
            
        }

        bool IMouseClickState.Condition()
        {
            return _inputController.hoveredTileHits.Any(hit => hit.collider.GetComponent<PlayerCore>());
        }

	}
}