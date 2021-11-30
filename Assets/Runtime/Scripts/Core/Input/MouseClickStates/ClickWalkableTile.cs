using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using Entities.Player;

namespace Core.Input
{
	public class ClickWalkableTile : IMouseClickState
	{
		InputController _inputController;
		TilemapController _tilemapController;
        PlayerCore _player;
        TileNode _targetNode;

        public ClickWalkableTile(GameObjects gameobjects)
        {
            _tilemapController = gameobjects.tilemapController;
            _inputController = gameobjects.inputController;
            _player = gameobjects.player;
        }


        void IMouseClickState.ProcessClick()
        {
            Debug.Log("move");
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