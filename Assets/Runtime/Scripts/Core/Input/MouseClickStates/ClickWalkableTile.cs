using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using Entities.PlayerScripts;

namespace Core.Input
{
	public class ClickWalkableTile : IMouseClickState
	{
		InputController _inputController;
		TilemapController _tilemapController;
        Player _player;
        TileNode _targetNode;

        public ClickWalkableTile(IClickStateSource stateSource)
        {
            _tilemapController = stateSource.tilemapController;
            _inputController = stateSource.inputController;
            _player = stateSource.player;
        }


        void IMouseClickState.ProcessClick()
        {
            _player.GotoNode(_targetNode);
        }

        bool IMouseClickState.Condition()
        {
        	Vector3Int position = _inputController.hoveredTilePos;
            if (_tilemapController.TryGetNode(position, out var node))
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