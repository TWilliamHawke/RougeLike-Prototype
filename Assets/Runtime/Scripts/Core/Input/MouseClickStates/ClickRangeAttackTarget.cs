using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using UnityEngine;

namespace Core.Input
{
	public class ClickRangeAttackTarget : IMouseClickState
	{
		ProjectileController _player;
        InputController _inputController;
        IRangeAttackTarget _target;

        public ClickRangeAttackTarget(GameObjects gameObjects)
		{
			_player = gameObjects.player.GetComponent<ProjectileController>();
            _inputController = gameObjects.inputController;
		}

        bool IMouseClickState.Condition()
        {
            foreach (var hit in _inputController.hoveredTileHits)
            {
                if(hit.collider.TryGetComponent<IRangeAttackTarget>(out var target))
                {
                    _target = target;

                    return true;
                }
            }

            return false;
        }

        void IMouseClickState.ProcessClick()
        {
			_player.ThrowProjectile(_target);
        }
    }
}