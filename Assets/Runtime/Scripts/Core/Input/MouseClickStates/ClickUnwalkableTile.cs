using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

namespace Core.Input
{
    public class ClickUnwalkableTile : IMouseClickState
    {
        TilemapController _tilemapController;
        InputController _inputController;

        public ClickUnwalkableTile(IClickStateSource stateSource)
        {
            _tilemapController = stateSource.tilemapController;
            _inputController = stateSource.inputController;
        }

        void IMouseClickState.ProcessClick()
        {
			//do nothing
        }

        bool IMouseClickState.Condition()
        {
        	Vector3Int position = _inputController.hoveredTilePos;
            if (_tilemapController.TryGetNode(position, out var node))
            {
                if (node.isWalkable)
                {
                    return false;
                }
            }

			return true;

        }

    }
}