using Entities.Combat;
using Entities.Behavior;
using UnityEngine;

namespace Entities.AI
{
    public class CanMeleeAttackTarget : ICondition
    {
		MovementController _movementController;
		IAttackTarget _target;

		const float _maxTargetDistance = 1.5f;

        public CanMeleeAttackTarget(MovementController movementController, IAttackTarget target)
        {
            _movementController = movementController;
            _target = target;
        }

        public bool IsMeet()
        {
            var enetityPos = _movementController.transform.position;
			var targetPos = _target.transform.position;

			return Vector3.Distance(enetityPos, targetPos) < _maxTargetDistance;
        }
    }
}