using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.Events;
using Rng = System.Random;
using Map.Actions;
using Items;

namespace Map.Zones
{
    public class Site : MapZone, IMapActionLocation
    {
        [SerializeField] MapActionTemplate _lootBodiesAction;
        [SerializeField] CustomEvent _onLocalTaskChange;

        [InjectField] EntitiesSpawner _spawner;
        [InjectField] IMapActionsFactory _mapActionsFactory;
        [InjectField] MapZonesObserver _mapZonesObserver;

        SiteTemplate _template;

        IMapActionsController _actionsController;
        KillEnemiesTask _taskController;
        ZoneEntitiesSpawner _spawnQueue;

        AliveEntitiesStorage _aliveEntitiesStorage = new();
        DeadEntitiesStorage _deadEntitiesStorage = new();

        public override IMapActionList mapActionList => _actionsController;
        public override TaskData currentTask => _taskController.currentTask;
        public override MapZonesObserver mapZonesObserver => _mapZonesObserver;

        public IContainersList inventory => _aliveEntitiesStorage;

        //should be called in Awake
        public void BindTemplate(SiteTemplate template, Rng rng)
        {
            _template = template;

            base.BindTemplate(template);
            _spawnQueue = new ZoneEntitiesSpawner(template, this);
            _taskController = new KillEnemiesTask(template, _onLocalTaskChange);
            _spawnQueue.AddObserver(_taskController);
            _spawnQueue.AddObserver(_aliveEntitiesStorage);
            _spawnQueue.AddObserver(_deadEntitiesStorage);
            _spawnQueue.AddToQueue(_template.enemies, rng);
            this.StartInjection();
        }

        public void FinalizeInjection()
        {
            if (_spawner is null) return;
            if (_mapActionsFactory is null) return;
            if (_mapZonesObserver is null) return;

            AddToObserver();
            _spawnQueue.SpawnAll(_spawner);
            FillActionsList();
        }

        private void FillActionsList()
        {
            _actionsController = new OpenWorldActionsController(_mapActionsFactory, this);
            _actionsController.AddLootAction(_lootBodiesAction, _deadEntitiesStorage);
            _template.possibleActions.ForEach(action => _actionsController.AddAction(action));
        }

        public void ReplaceFactionForAll(Faction replacer)
        {

        }
    }
}

