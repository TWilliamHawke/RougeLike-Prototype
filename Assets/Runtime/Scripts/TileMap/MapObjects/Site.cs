using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.Events;
using Rng = System.Random;

namespace Map.Objects
{
    public class Site : MapObject
    {
        [SerializeField] MapActionTemplate _lootBodiesAction;
        [SerializeField] CustomEvent _onLocalTaskChange;

        [InjectField] EntitiesSpawner _spawner;
        [InjectField] IMapActionsFactory _mapActionsFactory;

        List<EntitySpawnData> _spawnerData;
        Rng _rng;
        new SiteTemplate _template;
        IMapActionsController _actionsController;
        KillEnemiesTask _taskController;

        public override IMapActionList mapActionList => _actionsController;
        public override TaskData currentTask => _taskController.currentTask;

        public void FinalizeInjection()
        {
            if (_spawner is null) return;
            if (_mapActionsFactory is null) return;
            if (_template is null) return;

            _actionsController = new OpenWorldActionsController(_mapActionsFactory);
            SpawnEnemies();
            FillActionsList();
        }

        public void BindTemplate(SiteTemplate template, Rng rng)
        {
            base.BindTemplate(template);
            _template = template;
            _rng = rng;
            _taskController = new KillEnemiesTask(template, _onLocalTaskChange);
            FinalizeInjection();
        }

        private void FillActionsList()
        {
            _actionsController.AddLootAction(_lootBodiesAction, _taskController.enemiesFromLocation);

            foreach (var action in _template.possibleActions)
            {
                _actionsController.AddAction(action);
            }
        }

        private void SpawnEnemies()
        {
            var enemies = _template.enemies.GetCreatures(_rng);

            foreach (var enemyTemplate in enemies)
            {
                if (_tileStorage.TryPull(_rng, out var position))
                {
                    var enemy = _spawner.SpawnEnemyAsChild(new EntitySpawnData(enemyTemplate, position), this);
                    _taskController.RegisterEnemy(enemy);
                }
            }
        }
    }
}

