using System.Collections.Generic;
using Core.Settings;
using Map;
using UnityEngine;
using Core.Input;
using Core;
using UnityEngine.Events;

namespace Entities.Behavior
{
    public class MovementController : MonoBehaviour, IInjectionTarget
    {
        public event UnityAction OnStepEnd;

        [SerializeField] GlobalSettings _settings;
        [SerializeField] Body _body;
        [SerializeField] Injector _inputControllerInjector;
        [SerializeField] Injector _tileGridInjector;
        [SerializeField] StepSoundKit _stepSounds;

        [InjectField] InputController _inputController;
        [InjectField] TilesGrid _tileGrid;

        Stack<TileNode> _path = new Stack<TileNode>();
        TileNode _targetNode;

        Vector3 _currentNodePosition;
        Vector3 _targetNodePosition;
        float _progress = 0;
        float _directionMult = 1;
        bool _onPause = true;

        TileNode _currentNode;
        public int pathLength => _path?.Count ?? 0;

        bool IInjectionTarget.waitForAllDependencies => false;

        public void Init(TileNode spawnNode)
        {
            _currentNode = spawnNode;
            _inputControllerInjector.AddInjectionTarget(this);
            _tileGridInjector.AddInjectionTarget(this);
        }

        public void ClearPath()
        {
            _path.Clear();
        }

        public void SetDestination(TileNode node)
        {
            if (!_onPause) return;
            _path = _tileGrid.FindPath(_currentNode, node);
        }

        public void SetDestination(IInteractive target)
        {
            if (_tileGrid.TryGetNode(target.transform.position.ToInt(), out var node))
            {
                SetDestination(node);
            }
        }

        public void TakeAStep()
        {
            if(_inputController is null) return;
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
            if (_targetNode is null) return;
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
                OnStepEnd?.Invoke();
            }
        }

        void PlayStepSound()
        {
            var clip = _stepSounds.GetRandom();
            _body.PlaySound(clip);
        }

        Vector3 GetNodePosition(TileNode node)
        {
            float z = transform.position.z;
            return new Vector3(node.x, node.y, z);
        }

        void IInjectionTarget.FinalizeInjection()
        {
            
        }
    }
}