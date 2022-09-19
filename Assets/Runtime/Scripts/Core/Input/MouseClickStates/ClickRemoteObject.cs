using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using Entities.Player;

namespace Core.Input
{
    public class ClickRemoteObject : IMouseClickState
    {
        InputController _inputController;
        PlayerCore _player;

        IInteractive _target;

        public ClickRemoteObject(IClickStateSource stateSource)
        {
            _inputController = stateSource.inputController;
            _player = stateSource.player;
        }

        void IMouseClickState.ProcessClick()
        {
            _player.GotoRemoteTarget(_target);
        }

        bool IMouseClickState.Condition()
        {
            foreach (var hit in _inputController.hoveredTileHits)
            {
                if(hit.collider.TryGetComponent<IInteractive>(out var target))
                {
                    var objectPos = target.transform.position;
                    var playerPos = _player.transform.position;
                    _target = target;

                    return Vector3.Distance(objectPos, playerPos) > 1.5;
                }
            }

            return false;
        }
    }
}