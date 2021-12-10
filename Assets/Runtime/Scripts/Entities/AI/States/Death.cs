using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.AI
{
    public class Death : IState
    {
		IHealthComponent _health;
        StateMachine _stateMachine;

        public Death(IHealthComponent health, StateMachine stateMachine)
        {
            _health = health;
            _stateMachine = stateMachine;
        }

        public bool Condition()
        {
            return _health.currentHealth <= 0;
        }

        public void EndTurn()
        {
            
        }

        public void StartTurn()
        {
            _stateMachine.EndTurn();
        }
    }
}