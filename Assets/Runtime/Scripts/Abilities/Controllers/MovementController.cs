using System.Collections.Generic;
using Core.Settings;
using Map;
using UnityEngine;
using Core.Input;
using UnityEngine.Events;
using Entities;

namespace Abilities
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] GlobalSettings _settings;
        [SerializeField] StepSoundKit _stepSounds;

        [InjectField] TilesGrid _tileGrid;

        MovementAbility _activeAbility;
        float _progress = 0;
        Vector3 _currentNodePosition => _currentNode.intPosition;
        Vector3 _targetNodePosition => _targetNode.intPosition;

        TileNode _targetNode;
        TileNode _currentNode;

        void Update()
        {
            if (_activeAbility is null) return;
            if (_activeAbility.onPause) return;

            _progress += Time.deltaTime * _settings.animationSpeed;

            var updatedPosition = Vector3
                .Lerp(_currentNodePosition, _targetNodePosition, _progress);
            _activeAbility.MoveTarget(updatedPosition);

            if (_progress >= 1)
            {
                _progress = 0;
                _activeAbility.UpdateTargetNode(_currentNode, _targetNode);
                _activeAbility.FinalizeStep();
            }
        }

        public void SelectActiveAbility(MovementAbility ability)
        {
            _activeAbility = ability;
            _currentNode = _tileGrid.GetNode(ability.targetPosition);
            _targetNode = ability.path.Pop();
        }

        public Stack<TileNode> FindPath(IPositionData from, IPositionData to)
        {
            return _tileGrid.FindPath(from.intPosition, to.intPosition);
        }

        public TileNode FindNode(Vector3 position)
        {
            return _tileGrid.GetNode(position.ToInt());
        }

        void PlayStepSound()
        {
            var clip = _stepSounds.GetRandom();
            //_body.PlaySound(clip);
        }
    }
}