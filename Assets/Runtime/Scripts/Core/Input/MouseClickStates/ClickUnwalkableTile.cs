using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

namespace Core.Input
{
    public class ClickUnwalkableTile : IMouseClickState
    {
        TilesGrid _tilemapController;
        InputController _inputController;

        public ClickUnwalkableTile(TilesGrid tilemapController, InputController inputController)
        {
            _tilemapController = tilemapController;
            _inputController = inputController;
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