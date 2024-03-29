using System.Collections;
using System.Collections.Generic;
using Entities;
using Entities.Behavior;
using Entities.Combat;
using UnityEngine;

namespace Core.Input
{
	public class ClickRangeAttackTarget : IMouseClickState
	{
		ProjectileController _player;
        InputController _inputController;
        IRangeAttackTarget _target;

        public ClickRangeAttackTarget(InputController inputController, ProjectileController player)
        {
            _inputController = inputController;
            _player = player;
        }

        bool IMouseClickState.Condition()
        {
            foreach (var hit in _inputController.hoveredTileHits)
            {
                if(!hit.collider.TryGetComponent<IRangeAttackTarget>(out _target)) continue;
                var aggr = hit.collider.GetComponent<IFactionMember>()?.behavior;
                if(aggr == BehaviorType.agressive) return true;
            }

            return false;
        }

        void IMouseClickState.ProcessClick()
        {
			_player.ThrowProjectile(_target);
        }
    }
}