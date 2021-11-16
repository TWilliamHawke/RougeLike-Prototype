using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

namespace Entities.Behavior
{
    public class MovementController
    {
        ICanMove _entity;
        Stack<TileNode> _path;
        TileNode _targetNode;

        Vector3 _currentNodePosition;
        Vector3 _targetNodePosition;
        float _progress = 0;

        public MovementController(ICanMove entity)
        {
            _entity = entity;
        }

        public void SetPath(Stack<TileNode> path)
        {
            _path = path;

            SetTarget(path.Pop());
        }

        public void SetTarget(TileNode node)
        {
            _targetNode = node;
            _targetNodePosition = GetNodePosition(_targetNode);
            _currentNodePosition = GetNodePosition(_entity.currentNode);
        }

        public void UpdatePosition()
        {
            if (_targetNode == null) return;

            _progress += Time.deltaTime * 2;
            var updatedPosition = Vector3.Lerp(_currentNodePosition, _targetNodePosition, _progress);
            _entity.TeleportTo(updatedPosition);

            if (_progress >= 1)
            {
                GotoNextNode();
            }
        }

        private void GotoNextNode()
        {
            _progress = 0;
            _entity.MoveTo(_targetNode);
            _currentNodePosition = GetNodePosition(_entity.currentNode);

            if (_path.Count > 0)
            {
                _targetNode = _path.Pop();
                _targetNodePosition = GetNodePosition(_targetNode);
            }
            else
            {
                _targetNode = null;
            }
        }

        Vector3 GetNodePosition(TileNode node)
        {
            float z = _entity.transform.position.z;
            return new Vector3(node.x, node.y, z);
        }
    }
}