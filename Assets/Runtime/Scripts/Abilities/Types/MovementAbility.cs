using System.Collections.Generic;
using Entities;
using Map;
using UnityEngine;

namespace Abilities
{
    public class MovementAbility : AbstractAbility
    {
        PositionController _target;
        bool _onPause = true;

        Stack<TileNode> _path = new Stack<TileNode>();

        public bool onPause => _onPause;
        protected override IIconData template => _template;
        public Vector3Int targetPosition => _target.intPosition;
        public Stack<TileNode> path => _path;
        public override bool fitForMainSlot => false;

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

        public void MoveTarget(Vector3 position)
        {
            _target.MoveTo(position);
        }

        public void UpdateTargetNode(TileNode from, TileNode to)
        {
            var entity = _target.GetComponent<IObstacleEntity>();
            from.RemoveEntity(entity);
            to.AddEntity(entity);
        }

        public override bool TileHasValidTarget(IAbilityUser user, ITileClickData tile)
        {
            return tile.isWalkableAndEmpty;
        }

        public override void Use(IAbilityUser user, IAbilityTarget target)
        {
            var start = user.GetEntityComponent<PositionController>();
            _target = user.GetEntityComponent<PositionController>();
            var destination = target as IPositionData;

            if (start == null || destination == null) return;

            _path = _controller.FindPath(start, destination);
            StartNextStep();
        }

        public void FinalizeStep()
        {
            _onPause = true;
            StartNextStep();
        }

        public override IAbilityTarget SelectTarget(ITileClickData tile)
        {
            return _controller.FindNode(tile.intPosition);
        }

        private void StartNextStep()
        {
            if (_path.Count == 0) return;
            _onPause = false;
            _controller.SelectActiveAbility(this);
        }
    }
}