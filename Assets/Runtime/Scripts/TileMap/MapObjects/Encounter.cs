using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using Entities.NPCScripts;

namespace Map.Objects
{
    public class Encounter : MapObject
    {
        [SerializeField] CustomEvent _onLocalTaskChange;

        [InjectField] EntitiesSpawner _spawner;
        [InjectField] IMapActionsFactory _mapActionsFactory;

        new EncounterTemplate _template;
        public override IMapActionList mapActionList => _actionsController;
        public override TaskData currentTask => _taskController.currentTask;

        KillEnemiesTask _taskController;
        IMapActionsController _actionsController;

        public void FinalizeInjection()
        {
            if (_spawner is null) return;
            if (_mapActionsFactory is null) return;
            if (_template is null) return;

            _actionsController = new OpenWorldActionsController(_mapActionsFactory);
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

        private void FillActionsList()
        {

        }

        private void SpawnEntities()
        {
            _spawner.SpawnNpc(new SpawnData<NPCTemplate>(_template.mainNPC, transform.position.ToInt()), this);
        }

    }
}


