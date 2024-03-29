using System.Collections;
using System.Collections.Generic;
using Entities;
using Entities.PlayerScripts;
using UnityEngine;

namespace Core.Input
{
	public class ClickNextTileObject : IMouseClickState
	{
        InputController _inputController;
        Player _player;
        
		IInteractive _target;

        public ClickNextTileObject(InputController inputController, Player player)
        {
            _inputController = inputController;
            _player = player;
        }

        void IMouseClickState.ProcessClick()
        {
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

                    return Vector3.SqrMagnitude(targetPos - playerPos) < 2.1;
                }
            }

            return false;
        }
	}
}