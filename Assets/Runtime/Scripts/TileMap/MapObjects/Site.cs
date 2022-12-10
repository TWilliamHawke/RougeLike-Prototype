using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

using Rng = System.Random;

namespace Map.Objects
{
    public class Site : MapObject, IInjectionTarget
    {
        [SerializeField] BoxCollider2D _collider;
        [SerializeField] Injector _enemySpawnerInjector;
        [SerializeField] Injector _lootPanelInjector;
        [SerializeField] MapActionTemplate _lootBodiesAction;

        [InjectField] EntitiesSpawner _spawner;

        MapObjectTaskData _task = new MapObjectTaskData("Kill All wolves", true);
        List<Entity> _enemiesFromSite;
        List<EntitySpawnData> _spawnerData;
        RandomStack<Vector3Int> _tileStorage;
        SiteTemplate _template;
        Rng _rng;

        int _posX;
        int _posY;


        public bool waitForAllDependencies => true;
        public override MapObjectTemplate template => _template;
        public override MapObjectTaskData task => _task;

        public override IMapActionsController actionsController => _actionsController;
        IMapActionsController _actionsController = new DefaultMapActionsController();

        public void FinalizeInjection()
        {
            SpawnEnemies();
            FillActionsList();
        }

        public void BindTemplate(SiteTemplate template, Rng rng)
        {
            _template = template;
            _rng = rng;
            _collider.size = new Vector2(template.width, template.height);
            _posX = (int)transform.position.x;
            _posY = (int)transform.position.y;
            _enemySpawnerInjector.AddInjectionTarget(this);
        }

        private void FillActionsList()
        {
            var lootBodiesLogic = new LootBodies(_lootBodiesAction);
            _lootPanelInjector.AddInjectionTarget(lootBodiesLogic);
            lootBodiesLogic.CreateLoot(_enemiesFromSite);
            _actionsController.AddLogic(lootBodiesLogic);

            foreach(var action in _template.possibleActions)
            {
                _actionsController.AddLogic(action);
            }
        }

        private void SpawnEnemies()
        {
            if (_tileStorage is null)
            {
                FillStorageWithWalkableTile();
            }

            if (_tileStorage.isEmpty) return;

            _enemiesFromSite = new List<Entity>();
            var enemies = _template.enemies.GetCreatures(_rng);

            foreach (var enemyTemplate in enemies)
            {
                if (_tileStorage.TryPull(_rng, out var position))
                {
                    var enemy = _spawner.SpawnEnemyAsChild(new EntitySpawnData(enemyTemplate, position), this);
                    _enemiesFromSite.Add(enemy);
                    enemy.OnDeath += RemoveDeadEnemy;
                }
            }

            _task.taskText = ConstructKillString(_enemiesFromSite.Count);
        }

        private string ConstructKillString(int enemiesCount)
        {
            return $"Kill all wolves ({enemiesCount} remains)";
        }


        private void FillStorageWithWalkableTile()
        {
            int tilesCount = _template.width * _template.height;
            if (!_template.tilesIsWalkable)
            {
                tilesCount -= _template.tilesWidth * _template.tilesHeight;
            }

            _tileStorage = new RandomStack<Vector3Int>(tilesCount);

            for (int x = _posX - _template.width / 2; x <= _posX + _template.width / 2; x++)
            {
                for (int y = _posY - _template.height / 2; y <= _posY + _template.height / 2; y++)
                {
                    if (!TileIsWalkable(x, y)) continue;
                    _tileStorage.Push(new Vector3Int(x, y, 0));
                }
            }
        }

        private bool TileIsWalkable(int x, int y)
        {
            if (_template.tilesIsWalkable) return true;

            if (x < _posX - _template.tilesWidth / 2 || x >= _posX + 1 + _template.tilesWidth / 2) return true;
            if (y < _posY - _template.tilesHeight / 2 || y >= _posY + 1 + _template.tilesHeight / 2) return true;

            return false;
        }

        private void RemoveDeadEnemy(Entity enemy)
        {
            if(!_enemiesFromSite.Contains(enemy)) return;
            _enemiesFromSite.Remove(enemy);
            
            if(_enemiesFromSite.Count > 0)
            {
                _task.taskText = ConstructKillString(_enemiesFromSite.Count);
            }
            else
            {
                _task = new MapObjectTaskData("Click to loot", false);
            }

            UpdateTask();
        }
    }
}

