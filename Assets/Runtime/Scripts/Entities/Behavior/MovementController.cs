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
        PathFinder _pathFinder;

        Vector3 _currentNodePosition;
        Vector3 _targetNodePosition;
        float _progress = 0;
        float _directionMult = 1;
        bool _onPause = true;

        public int pathLength => _path?.Count ?? 0;

        public MovementController(ICanMove entity, TilemapController mapController)
        {
            _entity = entity;
            _pathFinder = new PathFinder(entity, mapController);
        }

        public void SetDestination(TileNode node)
        {
            if(!_onPause) return;
            _path = _pathFinder.FindPathTo(node);
        }

        public void StartMovement()
        {
            if (_path.Count <= 0) return;
            SetTarget(_path.Pop());
            _onPause = false;
        }

        public void UpdatePosition()
        {
            if (_targetNode == null) return;
            if (_onPause) return;

            _progress += Time.deltaTime * 2 * _directionMult;
            var updatedPosition = Vector3.Lerp(_currentNodePosition, _targetNodePosition, _progress);
            _entity.TeleportTo(updatedPosition);

            if (_progress >= 1)
            {
                GotoNextNode();
            }
        }

        public void SuspendMovement()
        {
            _onPause = true;
        }

        void SetTarget(TileNode node)
        {
            _targetNode = node;
            _targetNodePosition = GetNodePosition(_targetNode);
            _currentNodePosition = GetNodePosition(_entity.currentNode);
            float distance = Vector3.Distance(_currentNodePosition, _targetNodePosition);
            _directionMult = distance < Mathf.Epsilon ? 1 : 1 / distance;
        }

        void GotoNextNode()
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
                _onPause = true;
            }
        }

        Vector3 GetNodePosition(TileNode node)
        {
            float z = _entity.transform.position.z;
            return new Vector3(node.x, node.y, z);
        }
    }
}