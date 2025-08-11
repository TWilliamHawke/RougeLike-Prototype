using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

namespace Core.Input
{
    public class ClickUnwalkableTile : IClickAction
    {
        TilesGrid _tilemapController;
        InputController _inputController;

        public ClickUnwalkableTile(TilesGrid tilemapController, InputController inputController)
        {
            _tilemapController = tilemapController;
            _inputController = inputController;
        }

        void IClickAction.ProcessClick()
        {
			//do nothing
        }

        bool IClickAction.Condition()
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