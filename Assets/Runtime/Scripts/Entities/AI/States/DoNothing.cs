using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.AI
{
    public class DoNothing : IState
    {
        StateMachine _stateMachine;

        public DoNothing(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public bool Condition()
        {
            return true;
        }

        public void StartTurn()
        {
            _stateMachine.EndTurn();
        }
    }
}


