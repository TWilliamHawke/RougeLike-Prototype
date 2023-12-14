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
        [Header("Injectors")]
        [SerializeField] Injector _experienceStorageInjector;

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
    }
}