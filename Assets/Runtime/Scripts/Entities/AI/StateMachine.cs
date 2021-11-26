using System.Collections.Generic;
using Core;
using Entities.Behavior;
using Entities.Combat;
using UnityEngine;

namespace Entities.AI
{
	public class StateMachine : MonoBehaviour
	{
		[SerializeField] GameObjects _gameObjects;
		[SerializeField] MovementController _movementController;
		[SerializeField] MeleeAttackController _meleeAttackController;

		IState _defaultState;
		IState _currentState;

		List<Transition> _transitions = new List<Transition>();

		public bool isDone => _currentState.isDone;

		public void Init()
		{
			_defaultState = new Wait();
			var meleeAttack = new MeleeAttack(_meleeAttackController, _gameObjects.player);

			var canMeleeAttackTarget = new CanMeleeAttackTarget(_movementController, _gameObjects.player);

			AddTransition(canMeleeAttackTarget, meleeAttack);
			_currentState = _defaultState;
		}

	    public void StartTurn()
		{
			foreach (var transition in _transitions)
            {
                if (!transition.condition.IsMeet()) continue;

                _currentState.EndTurn();
                _currentState = transition.to;
                _currentState.StartTurn();
                break;
            }
        }

		public void EndTurn()
		{
			_currentState.EndTurn();
			_currentState = _defaultState;
		}

		void AddTransition(ICondition condition, IState state)
		{
			_transitions.Add(new Transition(condition, state));
		}

		class Transition
		{
            public Transition(ICondition condition, IState to)
            {
                this.condition = condition;
                this.to = to;
            }

            public ICondition condition { get; }
			public IState to { get; }
		}
	}
}