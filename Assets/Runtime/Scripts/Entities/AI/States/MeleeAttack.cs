using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.Combat;
using Entities.Behavior;

namespace Entities.AI
{
    public class MeleeAttack : IState
    {
        MeleeAttackController _meleeAttackController;
        MovementController _movementController;

        IAttackTarget _target;

        const float _maxTargetDistance = 1.5f;


        public MeleeAttack(MeleeAttackController meleeAttackController, IAttackTarget target, MovementController movementController)
        {
            _meleeAttackController = meleeAttackController;
            _target = target;
            _movementController = movementController;
        }

        public bool endTurnImmediantly => false;

        public void StartTurn()
        {
            _meleeAttackController.StartAttack(_target);
        }

        public void EndTurn()
        {

        }

        public bool Condition()
        {
            var enetityPos = _movementController.transform.position;
            var targetPos = _target.transform.position;

            return Vector3.Distance(enetityPos, targetPos) < _maxTargetDistance;
        }
    }
}