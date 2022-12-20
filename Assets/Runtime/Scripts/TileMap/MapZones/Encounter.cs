using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using Entities.NPCScripts;

namespace Map.Zones
{
    public class Encounter : MapZone, INpcActionTarget, IMapActionList
    {
        [SerializeField] CustomEvent _onLocalTaskChange;

        [InjectField] EntitiesSpawner _spawner;
        [InjectField] IMapActionsFactory _mapActionsFactory;

        new EncounterTemplate _template;
        public override IMapActionList mapActionList => this;
        public override TaskData currentTask => _taskController.currentTask;

        public int count => _encounterActions.Count;

        public IMapAction this[int idx] => _encounterActions[idx];

        KillEnemiesTask _taskController;
        List<IMapAction> _encounterActions = new();
        NPC _mainNpc;

        public void FinalizeInjection()
        {
            if (_spawner is null) return;
            if (_mapActionsFactory is null) return;
            if (_template is null) return;

            SpawnEntities();
            FillActionsList();
        }

        public void BindTemplate(EncounterTemplate template)
        {
            base.BindTemplate(template);
            _template = template;
            _taskController = new KillEnemiesTask(template, _onLocalTaskChange);

            FinalizeInjection();
        }

        public void ReplaceFactionForAll(Faction replacer)
        {
            _mainNpc.ReplaceFaction(replacer);
        }

        private void FillActionsList()
        {
            foreach(var actionTemplate in _template.possibleActions)
            {
                var action = _mapActionsFactory.CreateNPCAction(actionTemplate, this);
                _encounterActions.Add(action);
            }
        }

        private void SpawnEntities()
        {
            _mainNpc = _spawner.SpawnNpc(new SpawnData<NPCTemplate>(_template.mainNPC, transform.position.ToInt()), this);
        }

    }
}


