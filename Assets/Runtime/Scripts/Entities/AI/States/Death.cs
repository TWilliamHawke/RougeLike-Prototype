using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.AI
{
    public class Death : IState
    {
		IHealthComponent _health;

        public Death(IHealthComponent health)
        {
            _health = health;
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
            
        }
    }
}