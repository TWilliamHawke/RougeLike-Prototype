using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.Player;
using Entities.AI;
using Core.Input;
using UnityEngine.Events;

namespace Entities
{
    public class EntitiesManager : MonoBehaviour, IInjectionTarget, IDependency
    {
        [SerializeField] PlayerCore _player;
        [SerializeField] Enemy _testEnemy;
        [SerializeField] Injector _inputControllerInjector;
        [SerializeField] Injector _selfInjector;

        [InjectField] InputController _inputController;


        StateMachine _currentStateMachine;

        Stack<StateMachine> _activeEnemies = new Stack<StateMachine>();
        List<StateMachine> _allEntities = new List<StateMachine>();

        public event UnityAction OnReadyForUse;

        bool IInjectionTarget.waitForAllDependencies => false;

        public bool isReadyForUse => _isReadyForUse;
        bool _isReadyForUse = false;

        public void StartUp()
        {
            _player.Init();
            _testEnemy.Init();
            AddEntityToObserve(_testEnemy);
            _player.OnPlayerTurnEnd += StartFirstEnemyTurn;
            _inputControllerInjector.AddInjectionTarget(this);
            _selfInjector.AddDependencyWithState(this);
        }

        public void AddEntityToObserve(IEntityWithAI entity)
        {
            entity.stateMachine.Init();
            entity.stateMachine.OnTurnEnd += StartNextEnemyTurn;
            _allEntities.Add(entity.stateMachine);
        }


        void StartFirstEnemyTurn()
        {
            AddActiveEnemiesToStack();
            StartNextEnemyTurn();
        }

        void StartNextEnemyTurn()
        {
            if (_activeEnemies.Count > 0)
            {
                _currentStateMachine = _activeEnemies.Pop();
                _currentStateMachine.StartTurn();
            }
            else
            {
                _currentStateMachine = null;
                _inputController.EnableLeftClick();
                _player.StartTurn();
            }
        }

        void AddActiveEnemiesToStack()
        {
            foreach (var stateMachine in _allEntities)
            {
                _activeEnemies.Push(stateMachine);
            }
        }

        void IInjectionTarget.FinalizeInjection()
        {
            _isReadyForUse = true;
            OnReadyForUse?.Invoke();
        }
    }
}