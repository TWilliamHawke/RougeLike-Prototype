using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Entities.PlayerScripts;

namespace Core.Input
{
	public class ClickPlayer : IMouseClickState
	{

        InputController _inputController;

        public ClickPlayer(InputController inputController)
        {
            _inputController = inputController;
        }

        void IMouseClickState.ProcessClick()
        {
            //skip turn
            
        }

        bool IMouseClickState.Condition()
        {
            return _inputController.hoveredTileHits.Any(hit => hit.collider.GetComponent<Player>());
        }

	}
}