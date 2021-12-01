using System.Collections.Generic;
using Core.Settings;
using Map;
using UnityEngine;
using Core.Input;
using Core;

namespace Entities.Behavior
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] GlobalSettings _settings;
        [SerializeField] GameObjects _gameObjects;
        [SerializeField] AudioSource _body;
        InputController _inputController;
        TilemapController _mapController;

        Stack<TileNode> _path;
        TileNode _targetNode;

        Vector3 _currentNodePosition;
        Vector3 _targetNodePosition;
        float _progress = 0;
        float _directionMult = 1;
        bool _onPause = true;

        TileNode _currentNode;
        public int pathLength => _path?.Count ?? 0;

        public void Init(TileNode spawnNode)
        {
            _currentNode = spawnNode;
            _inputController = _gameObjects.inputController;
            _mapController = _gameObjects.tilemapController;
        }

        public void ClearPath()
        {
            _path.Clear();
        }

        public void SetDestination(TileNode node)
        {
            if (!_onPause) return;
            _path = PathFinder.FindPath(_currentNode, node);
        }

        public void SetDestination(IInteractive target)
        {
            if (_mapController.TryGetNode(target.transform.position.ToInt(), out var node))
            {
                SetDestination(node);
            }
        }

        public void TakeAStep()
        {
            if (_path.Count <= 0) return;

            PlayStepSound();
            _inputController.DisableLeftClick();

            _targetNode = _path.Pop();
            _targetNodePosition = GetNodePosition(_targetNode);
            _currentNodePosition = GetNodePosition(_currentNode);

            float distance = Vector3.Distance(_currentNodePosition, _targetNodePosition);
            _directionMult = distance < Mathf.Epsilon ? 1 : 1 / distance;
            _onPause = false;
        }

        void Update()
        {
            if (_targetNode == null) return;
            if (_onPause) return;

            MoveEntity();
        }

        void MoveEntity()
        {
            _progress += Time.deltaTime * _settings.animationSpeed;// * _directionMult;
            var updatedPosition = Vector3.Lerp(_currentNodePosition, _targetNodePosition, _progress);
            transform.position = updatedPosition;

            if (_progress >= 1)
            {
                _progress = 0;
                _onPause = true;
                transform.position = _targetNodePosition;
                _currentNode = _targetNode;
                _targetNode = null;
                _gameObjects.StartNextEntityTurn();
            }
        }

        void PlayStepSound()
        {
            var clip = _mapController.stepSounds.GetRandom();
            _body.PlayOneShot(clip);
        }

        Vector3 GetNodePosition(TileNode node)
        {
            float z = transform.position.z;
            return new Vector3(node.x, node.y, z);
        }
    }
}