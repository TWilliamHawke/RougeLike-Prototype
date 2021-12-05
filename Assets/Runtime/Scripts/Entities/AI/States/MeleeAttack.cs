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

        IAttackTarget _target;
        StateMachine _stateMachine;

        const float _maxTargetDistance = 1.5f;


        public MeleeAttack(MeleeAttackController meleeAttackController, IAttackTarget target, StateMachine stateMachine)
        {
            _meleeAttackController = meleeAttackController;
            _target = target;
            _stateMachine = stateMachine;
            _meleeAttackController.OnAttackEnd += EndTurn;
        }

        public void StartTurn()
        {
            _meleeAttackController.StartAttack(_target);
        }

        void EndTurn()
        {
            _stateMachine.EndTurn();
        }

        public bool Condition()
        {
            var enetityPos = _meleeAttackController.transform.position;
            var targetPos = _target.transform.position;

            return Vector3.Distance(enetityPos, targetPos) < _maxTargetDistance;
        }
    }
}