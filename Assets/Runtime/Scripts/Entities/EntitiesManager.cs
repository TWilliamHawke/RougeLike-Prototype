using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.PlayerScripts;
using Entities.AI;
using Core.Input;
using UnityEngine.Events;
using Leveling;

namespace Entities
{
    public class EntitiesManager : MonoBehaviour, IInjectionTarget, IDependency
    {
        [SerializeField] Player _player;
        [SerializeField] Enemy _testEnemy;
        [SerializeField] Injector _inputControllerInjector;
        [SerializeField] Injector _selfInjector;

        [InjectField] InputController _inputController;
		[SerializeField] Injector _experienceStorageInjector;

        ExpForKillsController _enemyKillsObserver;

        StateMachine _currentStateMachine;

        Stack<StateMachine> _activeEnemies = new Stack<StateMachine>();
        List<StateMachine> _allEntities = new List<StateMachine>();

        public event UnityAction OnReadyForUse;

        bool IInjectionTarget.waitForAllDependencies => false;

        public bool isReadyForUse => _isReadyForUse;
        bool _isReadyForUse = false;

        public void StartUp()
        {
            _enemyKillsObserver = new ExpForKillsController();
            _experienceStorageInjector.AddInjectionTarget(_enemyKillsObserver);
            _player.Init();
            _testEnemy.Init();
            AddEntityToObserve(_testEnemy);
            _player.OnPlayerTurnEnd += StartFirstEnemyTurn;
            _inputControllerInjector.AddInjectionTarget(this);
            _selfInjector.AddDependency(this);
        }

        public void AddEnemy(Enemy enemy)
        {
            AddEntityToObserve(enemy);
            _enemyKillsObserver.AddEnemyToObserve(enemy);

        }

        void AddEntityToObserve(IEntityWithAI entity)
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