using System.Collections.Generic;
using Core;
using Entities.Behavior;
using Entities.Combat;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.AI
{
	[RequireComponent(typeof(MovementController))]
	[RequireComponent(typeof(MeleeAttackController))]
	[RequireComponent(typeof(Health))]
	public class StateMachine : MonoBehaviour
	{
		public event UnityAction OnTurnEnd;

		[SerializeField] GameObjects _gameObjects;

		MeleeAttackController _meleeAttackController;
		Health _health;

		IState _defaultState;
		IState _currentState;

		List<IState> _states = new List<IState>();

		public void Init()
		{
			_health = GetComponent<Health>();
			
			_defaultState = new Wait(this);
			_states.Add(new Death(_health, this));
			_states.Add(new MeleeAttack(_gameObjects.player, this));


			_currentState = _defaultState;
		}

	    public void StartTurn()
		{
			foreach (var state in _states)
            {
                if (!state.Condition()) continue;

                _currentState = state;
				break;
            }

			_currentState.StartTurn();

        }

		public void EndTurn()
		{
			_currentState = _defaultState;
			OnTurnEnd?.Invoke();
		}

		
	}
}