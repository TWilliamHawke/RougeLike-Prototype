using System.Collections.Generic;
using Core;
using Entities.Behavior;
using Entities.Combat;
using Entities.PlayerScripts;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.AI
{
	[RequireComponent(typeof(MovementController))]
	[RequireComponent(typeof(MeleeAttackController))]
	[RequireComponent(typeof(Health))]
	public class StateMachine : MonoBehaviour, IInjectionTarget
	{
		public event UnityAction OnTurnEnd;

		[SerializeField] Injector _playerInjector;

		[InjectField] Player _player;

		MeleeAttackController _meleeAttackController;
		Health _health;

		IState _defaultState;
		IState _currentState;

		List<IState> _states = new List<IState>();

        public bool waitForAllDependencies => false;

        public void Init()
		{
			_health = GetComponent<Health>();
			
			_defaultState = new Wait(this);
			_states.Add(new Death(_health, this));
			_playerInjector.AddInjectionTarget(this);

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

        public void FinalizeInjection()
        {
            _states.Add(new MeleeAttack(_player, this));
        }
    }
}