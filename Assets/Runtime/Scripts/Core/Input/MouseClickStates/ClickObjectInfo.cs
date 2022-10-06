using System.Collections;
using System.Collections.Generic;
using Core.Input;
using UnityEngine;

namespace Core.Input
{
    public class ClickObjectInfo : IMouseClickState
    {
        IInfoModeState _infoModeState;
        InputController _inputController;


        public ClickObjectInfo(IInfoModeState infoModeState, InputController inputController)
        {
            _infoModeState = infoModeState;
            _inputController = inputController;
        }

        public bool Condition()
        {
            return _infoModeState.infoMode == true;
        }

        public void ProcessClick()
        {
            if (_inputController.hoveredTileHits.Length > 0)
            {
                var hit = _inputController.hoveredTileHits[0];
                Debug.Log(hit.collider.gameObject.name);
            }
            else
            {
                Debug.Log(_inputController.hoveredTilePos);
            }
        }

    }
}
