using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.PlayerScripts;
using Entities.AI;
using Core.Input;
using UnityEngine.Events;
using Leveling;
using Map;

namespace Entities
{
    public class EntitiesManager : MonoBehaviour, IInjectionTarget
    {
        [SerializeField] Player _player;
        [SerializeField] Entity _testEnemy;

        [Header("Injectors")]
        [SerializeField] Injector _inputControllerInjector;
        [SerializeField] Injector _selfInjector;
        [SerializeField] Injector _experienceStorageInjector;
        [SerializeField] Injector _tilesGridInjector;

        [InjectField] InputController _inputController;
        [InjectField] TilesGrid _tilesGrid;

        ExpForKillsController _enemyKillsObserver;

        StateMachine _currentStateMachine;

        Stack<StateMachine> _activeEnemies = new Stack<StateMachine>();
        List<StateMachine> _allEntities = new List<StateMachine>();

        bool IInjectionTarget.waitForAllDependencies => true;

        public void StartUp()
        {
            _enemyKillsObserver = new ExpForKillsController();
            _experienceStorageInjector.AddInjectionTarget(_enemyKillsObserver);
            _player.Init();
            _testEnemy.Init();
            _player.OnPlayerTurnEnd += StartFirstEnemyTurn;
            _inputControllerInjector.AddInjectionTarget(this);
            _selfInjector.SetDependency(this);
            _tilesGridInjector.AddInjectionTarget(this);
        }

        public void AddEnemy(Entity enemy)
        {
            AddEntityToObserve(enemy);
            _tilesGrid.TryAddEntityToTile(enemy);
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
            AddEnemy(_testEnemy);
        }
    }
}