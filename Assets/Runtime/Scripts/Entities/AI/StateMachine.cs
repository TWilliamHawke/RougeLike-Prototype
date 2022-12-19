using System.Collections.Generic;
using Core;
using Entities.Behavior;
using Entities.Combat;
using Entities.PlayerScripts;
using Map;
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
        [SerializeField] Injector _tilesGridInjector;

        [InjectField] TilesGrid _tilesGrid;
		[InjectField] Player _player;

		MeleeAttackController _meleeAttackController;
		Health _health;

		IState _defaultState;
		IState _currentState;

		List<IState> _states = new List<IState>();
		public Faction faction { get; private set; }

        public bool waitForAllDependencies => true;

        public void Init()
		{
			_health = GetComponent<Health>();
			faction = GetComponent<Entity>().faction;
			
			_playerInjector.AddInjectionTarget(this);
			_tilesGridInjector.AddInjectionTarget(this);
		}

	    public void StartTurn()
		{
			_currentState = _defaultState;
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
			OnTurnEnd?.Invoke();
		}

        public void FinalizeInjection()
        {
			_defaultState = new DoNothing(this);
			_states.Add(new Death(_health, this));
			_states.Add(new Wait(this, _tilesGrid));
            _states.Add(new MeleeAttack(_player, this));
        }
    }
}