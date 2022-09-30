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

        public ClickPlayer(IClickStateSource stateSource)
        {
            _inputController = stateSource.inputController;
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