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
    public class EntitiesManager : MonoBehaviour
    {
        [SerializeField] CustomEvent _onPlayerTurnStart;

        [Header("Injectors")]
        [SerializeField] Injector _experienceStorageInjector;

        [InjectField] TilesGrid _tilesGrid;

        Stack<StateMachine> _activeEnemies = new();
        List<Entity> _allEntities = new();
        List<IObserver<Entity>> _entitiesObservers = new();

        public void Awake()
        {
            var enemyKillsObserver = new ExpForKillsController();
            AddObserver(enemyKillsObserver);
            _experienceStorageInjector.AddInjectionTarget(enemyKillsObserver);
        }

        public void AddEntity(Entity entity)
        {
            entity.stateMachine.Init();
            _allEntities.Add(entity);
            _entitiesObservers.ForEach(observer => observer.AddToObserve(entity));
        }

        public void AddObserver(IObserver<Entity> observer)
        {
            _allEntities.ForEach(entity => observer.AddToObserve(entity));
            _entitiesObservers.Add(observer);
        }

        //used in editor
        public void StartFirstEnemyTurn()
        {
            AddActiveEnemiesToStack();
            StartNextEnemyTurn();
        }

        //used in editor
        public void StartNextEnemyTurn()
        {
            if (_activeEnemies.Count > 0)
            {
                var currentStateMachine = _activeEnemies.Pop();
                currentStateMachine.StartTurn();
            }
            else
            {
                _onPlayerTurnStart.Invoke();
            }
        }

        void AddActiveEnemiesToStack()
        {
            foreach (var entity in _allEntities)
            {
                _activeEnemies.Push(entity.stateMachine);
            }
        }

        //used in editor
        public void FinalizeInjection()
        {
            AddObserver(_tilesGrid);
        }
    }
}