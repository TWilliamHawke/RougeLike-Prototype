using System.Collections.Generic;
using Core.Settings;
using Map;
using UnityEngine;
using Core.Input;

namespace Entities.Behavior
{
    public class MovementController: MonoBehaviour
    {
        [SerializeField] TilemapController _mapController;
        [SerializeField] GlobalSettings _settings;
        [SerializeField] InputController _inputController;
        [SerializeField] AudioSource _audioSource;

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

        public void Init(ICanMove entity)
        {
            _entity = entity;
            _pathFinder = new PathFinder(entity, _mapController);
        }

        public void SetDestination(TileNode node)
        {
            if(!_onPause) return;
            _path = _pathFinder.FindPathTo(node);
        }

        public void TakeAStep()
        {
            if (_path.Count <= 0) return;

            PlayStepSound();
            _inputController.DisableLeftClick();

            _targetNode = _path.Pop();
            _targetNodePosition = GetNodePosition(_targetNode);
            _currentNodePosition = GetNodePosition(_entity.currentNode);

            float distance = Vector3.Distance(_currentNodePosition, _targetNodePosition);
            _directionMult = distance < Mathf.Epsilon ? 1 : 1 / distance;
            _onPause = false;
        }

        void Update()
        {
            if (_targetNode == null) return;
            if (_onPause) return;

            _progress += Time.deltaTime * _settings.animationSpeed;// * _directionMult;
            var updatedPosition = Vector3.Lerp(_currentNodePosition, _targetNodePosition, _progress);
            _entity.TeleportTo(updatedPosition);

            if (_progress >= 1)
            {
                _progress = 0;
                var targetNode = _targetNode;
                _targetNode = null;
                _inputController.EnableLeftClick();
                _entity.MoveTo(targetNode);
            }
        }

        public void SuspendMovement()
        {
            _onPause = true;
        }

        void PlayStepSound()
        {
            var index = Random.Range(0, _mapController.stepSounds.Length);
            _audioSource.PlayOneShot(_mapController.stepSounds[index]);
        }

        Vector3 GetNodePosition(TileNode node)
        {
            float z = _entity.transform.position.z;
            return new Vector3(node.x, node.y, z);
        }
    }
}