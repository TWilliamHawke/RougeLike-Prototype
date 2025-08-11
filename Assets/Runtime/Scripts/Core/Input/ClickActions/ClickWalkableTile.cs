using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using Entities.PlayerScripts;

namespace Core.Input
{
    public class ClickWalkableTile : IMouseClickAction
    {
        InputController _inputController;
        TilesGrid _tileGrid;
        Player _player;
        TileNode _targetNode;

        public ClickWalkableTile(InputController inputController, TilesGrid tileGrid, Player player)
        {
            _inputController = inputController;
            _tileGrid = tileGrid;
            _player = player;
        }

        void IMouseClickAction.ProcessClick()
        {
            _player.GotoNode(_targetNode);
        }

        bool IMouseClickAction.Condition()
        {
            Vector3Int position = _inputController.hoveredTilePos;
            if (_tileGrid.TryGetNode(position, out var node))
            {
                if (node.isWalkable)
                {
                    _targetNode = node;
                    return true;
                }
            }

            return false;

        }

    }
}