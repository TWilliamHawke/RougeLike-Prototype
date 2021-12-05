using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.AI
{
    public class Wait : IState
    {
        StateMachine _stateMachine;

        public Wait(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void StartTurn()
        {
            _stateMachine.EndTurn();
        }
        
        public void EndTurn()
        {

        }

        public bool Condition()
        {
            return true;
        }
    }
}