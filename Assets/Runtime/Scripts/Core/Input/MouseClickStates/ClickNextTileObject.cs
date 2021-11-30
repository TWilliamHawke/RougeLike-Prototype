using System.Collections;
using System.Collections.Generic;
using Entities;
using Entities.Player;
using UnityEngine;

namespace Core.Input
{
	public class ClickNextTileObject : IMouseClickState
	{
        InputController _inputController;
        PlayerCore _player;
        
		IInteractive _target;

		public ClickNextTileObject(GameObjects gameObjects)
		{
			_inputController = gameObjects.inputController;
			_player = gameObjects.player;
		}

        void IMouseClickState.ProcessClick()
        {
            Debug.Log("interact");
            _target?.Interact(_player);
        }

        bool IMouseClickState.Condition()
        {
            foreach (var hit in _inputController.hoveredTileHits)
            {
                if(hit.collider.TryGetComponent<IInteractive>(out var target))
                {
                    var targetPos = target.transform.position;
                    var playerPos = _player.transform.position;
                    _target = target;

                    return Vector3.Distance(targetPos, playerPos) < 1.5;
                }
            }

            return false;
        }
	}
}