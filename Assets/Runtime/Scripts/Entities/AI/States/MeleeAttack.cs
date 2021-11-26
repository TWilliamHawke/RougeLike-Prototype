using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.Combat;

namespace Entities.AI
{
    public class MeleeAttack : IState
    {
        MeleeAttackController _meleeAttackController;
        IAttackTarget _target;

        public MeleeAttack(MeleeAttackController meleeAttackController, IAttackTarget target)
        {
            _meleeAttackController = meleeAttackController;
            _target = target;
        }

        public bool isDone => !_meleeAttackController.isAttack;

        public void StartTurn()
        {
            _meleeAttackController.StartAttack(_target);
        }

        public void EndTurn()
        {
            
        }

    }
}