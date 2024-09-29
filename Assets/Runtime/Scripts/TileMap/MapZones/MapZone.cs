using System.Collections;
using System.Collections.Generic;
using Entities;
using Entities.PlayerScripts;
using Items;
using Map.Actions;
using UnityEngine;
using UnityEngine.Events;
using Rng = System.Random;


namespace Map.Zones
{
    public abstract class MapZone : MonoBehaviour, IMapZone, IMapActionLocation,
    IObserver<Entity>
    {
        public event UnityAction<IMapZone> OnPlayerEnter;
        public event UnityAction<IMapZone> OnPlayerExit;

        [SerializeField] CustomEvent _onLocalTaskChange;

        [InjectField] EntitiesSpawner _spawner;
        [InjectField] IMapActionsFactory _mapActionsFactory;
        [InjectField] MapZonesObserver _mapZonesObserver;

        IIconData _template;
        KillEnemiesTask _taskController;
        ZoneSpawnQueue _spawnQueue;
        IMapActionsController _actionsController;

        AliveEntitiesStorage _aliveEntitiesStorage = new();
        protected DeadEntitiesStorage _deadEntitiesStorage = new();
        HashSet<Entity> _entities = new();

        public string displayName => _template.displayName;
        public Sprite icon => _template.icon;
        public IInteractiveStorage inventory => _aliveEntitiesStorage;

        public IMapActionList actionList => _actionsController;
        public TaskData currentTask => _taskController.currentTask;

        protected abstract void FillActionsList(IMapActionsController mapActionsController);

        public void BindTemplate(IMapZoneTemplate template, Rng rng)
        {
            _template = template;

            _spawnQueue = new ZoneSpawnQueue(template, this);
            _taskController = new KillEnemiesTask(template, _onLocalTaskChange);
            _spawnQueue.AddObserver(_taskController);
            _spawnQueue.AddObserver(_aliveEntitiesStorage);
            _spawnQueue.AddObserver(_deadEntitiesStorage);
            _spawnQueue.AddObserver(this);
            _spawnQueue.AddToQueue(template.enemies, rng);
            this.StartInjection();
        }

        public void FinalizeInjection()
        {
            if (_spawner is null) return;
            if (_mapActionsFactory is null) return;
            if (_mapZonesObserver is null) return;

            _mapZonesObserver.AddToObserve(this);
            _spawnQueue.SpawnAll(_spawner);
            _actionsController = new OpenWorldActionsController(_mapActionsFactory, this);
            FillActionsList(_actionsController);
        }

        public void AddToObserve(InteractionZone target)
        {
            target.OnPlayerEnter += HandlePlayerEnter;
            target.OnPlayerExit += HandlePlayerExit;
        }

        public void RemoveFromObserve(InteractionZone target)
        {
            target.OnPlayerEnter -= HandlePlayerEnter;
            target.OnPlayerExit -= HandlePlayerExit;
        }

        public void ReplaceFactionForAll(Faction replacer)
        {
            _entities.ForEach(entitiy => entitiy.GetEntityComponent<IFactionMember>().ReplaceFaction(replacer));
        }

        public void AddToObserve(Entity target)
        {
            target.InitInteractiveZone(this);
            _entities.Add(target);
        }

        public void RemoveFromObserve(Entity target)
        {
            _entities.Remove(target);
        }

        private void HandlePlayerEnter()
        {
            OnPlayerEnter?.Invoke(this);
        }

        private void HandlePlayerExit()
        {
            OnPlayerExit?.Invoke(this);
        }
    }
}

