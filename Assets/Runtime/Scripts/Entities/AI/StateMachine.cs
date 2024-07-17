using System.Collections.Generic;
using Core;
using Entities.Behavior;
using Entities.Combat;
using Entities.PlayerScripts;
using Map;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace Entities.AI
{
    [RequireComponent(typeof(MovementController))]
    [RequireComponent(typeof(MeleeAttackController))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(FactionHandler))]
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] CustomEvent _onTurnEnd;

        [InjectField] TilesGrid _tilesGrid;
        [InjectField] Player _player;

        IState _defaultState;

        List<IState> _states = new();
        public Faction faction { get; private set; }

        public void Init()
        {
            faction = GetComponent<IFactionMember>().faction;
        }

        public void StartTurn()
        {
            var currentState = _states.FirstOrDefault(state => state.Condition()) ?? _defaultState;
            currentState.StartTurn();
        }

        public void EndTurn()
        {
            _onTurnEnd.Invoke();
        }

        //used in editor
        public void CreateStates()
        {
            _defaultState = new DoNothing(this);
            _states.Add(new Death(GetComponent<Health>(), this));
            _states.Add(new Wait(this, _tilesGrid));
            _states.Add(new MeleeAttack(_player, this));
        }
    }
}