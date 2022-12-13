using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.Events;
using Rng = System.Random;

namespace Map.Objects
{
    [RequireComponent(typeof(MapObject))]
    public class Site : MonoBehaviour, IMapObjectBehavior
    {
        [SerializeField] MapActionTemplate _lootBodiesAction;

        [InjectField] EntitiesSpawner _spawner;
        [InjectField] IMapActionsFactory _mapActionsFactory;

        public event UnityAction<TaskData> OnTaskChange;


        TaskData _task;
        List<Entity> _enemiesFromSite;
        List<EntitySpawnData> _spawnerData;
        SiteTemplate _template;
        Rng _rng;

        IMapObject _mapObject;


        public bool waitForAllDependencies => true;
        public TaskData task => _task;

        public IMapActionsController actionsController => _actionsController;
        IMapActionsController _actionsController;

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
            _template = template;
            _rng = rng;
            _mapObject = GetComponent<IMapObject>();
            _mapObject.BindTemplate(template);
            FinalizeInjection();
        }

        private void FillActionsList()
        {
            _actionsController.CreateLootAction(_lootBodiesAction, _enemiesFromSite);

            foreach (var action in _template.possibleActions)
            {
                _actionsController.AddAction(action);
            }
        }

        private void SpawnEnemies()
        {
            var walkableTiles = _mapObject.GetWalkableTiles();

            _enemiesFromSite = new List<Entity>();
            var enemies = _template.enemies.GetCreatures(_rng);

            foreach (var enemyTemplate in enemies)
            {
                if (walkableTiles.TryPull(_rng, out var position))
                {
                    var enemy = _spawner.SpawnEnemyAsChild(new EntitySpawnData(enemyTemplate, position), this);
                    _enemiesFromSite.Add(enemy);
                    enemy.OnDeath += RemoveDeadEnemy;
                }
            }

            CreateKillTask(_enemiesFromSite.Count);
        }

        private void CreateKillTask(int enemiesCount)
        {
            _task = new TaskData
            {
                displayName = _template.displayName,
                icon = _template.icon,
                taskText = $"Kill all wolves ({enemiesCount} remains)",
                objectIsLocked = true,
            };
        }

        private void RemoveDeadEnemy(Entity enemy)
        {
            if (!_enemiesFromSite.Contains(enemy)) return;
            _enemiesFromSite.Remove(enemy);

            if (_enemiesFromSite.Count > 0)
            {
                CreateKillTask(_enemiesFromSite.Count);
            }
            else
            {
                _task = new TaskData
                {
                    displayName = _template.displayName,
                    icon = _template.icon,
                    taskText = "click to loot",
                    objectIsLocked = false,
                };

            }

            OnTaskChange?.Invoke(_task);
        }
    }
}

