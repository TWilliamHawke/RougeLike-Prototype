using System.Collections.Generic;
using Entities;
using Map;
using UnityEngine;

namespace Abilities
{
    public class MovementAbility : AbstractAbility
    {
        float _progress = 0;
        Vector3 _currentNodePosition;
        Vector3 _targetNodePosition;
        PositionController _target;
        bool _onPause = true;

        TileNode _targetNode;
        TileNode _currentNode;

        Stack<TileNode> _path = new Stack<TileNode>();

        public bool onPause => _onPause;
        public float progress => _progress;
        protected override IIconData template => _template;

        MovementAbilityTemplate _template { get; init; }

        [InjectField] MovementController _controller;

        public MovementAbility(MovementAbilityTemplate template)
        {
            _template = template;
        }

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            throw new System.NotImplementedException();
        }

        public override bool TileHasValidTarget(IAbilityUser user, ITileClickData tile)
        {
            return tile.isWalkableAndEmpty;
        }

        public override void Use(IAbilityUser user, IAbilityTarget target)
        {
            var userPosition = user.GetEntityComponent<PositionController>();
            _target = user.GetEntityComponent<PositionController>();
            _currentNode = _controller.FindNode(userPosition.position);
            var finalPos = (target as IPositionData).intPosition;

            _path = _controller.FindPath(userPosition.intPosition, finalPos);
            StartNextStep();
        }

        public void UpdateProgress(float deltaTime)
        {
            _progress += deltaTime;
            var updatedPosition = Vector3
                .Lerp(_currentNodePosition, _targetNodePosition, _progress);
            _target.MoveTo(updatedPosition);
        }

        public void FinalizeStep()
        {
            _onPause = true;
            _progress = 0;
            var entity = _target.GetComponent<IObstacleEntity>();
            _currentNode.RemoveEntity(entity);
            _targetNode.AddEntity(entity);
            _target.MoveTo(_targetNodePosition);
            _currentNode = _targetNode;
            StartNextStep();
        }

        public override IAbilityTarget SelectTarget(ITileClickData tile)
        {
            return _controller.FindNode(tile.intPosition);
        }

        private void StartNextStep()
        {
            if (_path.Count == 0) return;

            _targetNode = _path.Pop();

            _targetNodePosition = _targetNode.intPosition;
            _currentNodePosition = _currentNode.intPosition;
            _onPause = false;
            _controller.SelectActiveAbility(this);
        }
    }
}