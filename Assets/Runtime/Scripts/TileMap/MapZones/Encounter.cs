using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using Entities.NPCScripts;
using Rng = System.Random;


namespace Map.Zones
{
    public class Encounter : MonoBehaviour, INpcActionTarget, IMapActionList, IMapZoneLogic, IObserver<Entity>
    {
        [SerializeField] CustomEvent _onLocalTaskChange;

        [InjectField] EntitiesSpawner _spawner;
        [InjectField] IMapActionsFactory _mapActionsFactory;
        [InjectField] MapZonesObserver _mapZonesObserver;

        EncounterTemplate _template;

        public int count => _encounterActions.Count;
        public TaskData currentTask => _taskController.currentTask;
        public IMapZoneTemplate template => _template;
        public IMapActionList actionList => this;

        public IMapAction this[int idx] => _encounterActions[idx];

        KillEnemiesTask _taskController;
        List<IMapAction> _encounterActions = new();
        HashSet<Entity> _entities = new();
        //NPC _mainNpc;
        ZoneEntitiesSpawner _spawnQueue;

        public void FinalizeInjection()
        {
            if (_spawner is null) return;
            if (_mapActionsFactory is null) return;
            if (_template is null) return;

            _spawnQueue.SpawnAll(_spawner);
            FillActionsList();
        }

        public void BindTemplate(EncounterTemplate template, Rng rng)
        {
            _template = template;
            _taskController = new KillEnemiesTask(template, _onLocalTaskChange);
            _spawnQueue = new ZoneEntitiesSpawner(template, this);
            _spawnQueue.AddObserver(this);
            _spawnQueue.AddToQueue(template.mainNPC, rng);

            FinalizeInjection();
        }

        public void ReplaceFactionForAll(Faction replacer)
        {
            _entities.ForEach(entitiy => entitiy.ReplaceFaction(replacer));
        }

        private void FillActionsList()
        {
            foreach(var actionTemplate in _template.possibleActions)
            {
                var action = _mapActionsFactory.CreateNPCAction(actionTemplate, this);
                _encounterActions.Add(action);
            }
        }

        public void AddToObserve(Entity target)
        {
            (target as NPC)?.InitInteractiveZone(this);
            _entities.Add(target);
        }

        public void RemoveFromObserve(Entity target)
        {
            _entities.Remove(target);
        }
    }
}


