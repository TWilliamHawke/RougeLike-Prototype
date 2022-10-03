using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

using Rng = System.Random;

namespace Map.Objects
{
    public class Site : MapObject, IInjectionTarget
    {
        MapObjectTask _task = new MapObjectTask("Kill All wolves", true);
        List<Enemy> _enemiesFromSite;
        TileStorage _tileStorage;
        SiteTemplate _template;
        Rng _rng;

        int _posX;
        int _posY;

        [SerializeField] BoxCollider2D _collider;
        [SerializeField] Injector _enemySpawnerInjector;
        [SerializeField] Injector _lootPanelInjector;
        [SerializeField] MapObjectAction _lootBodiesAction;

        [InjectField] EntitiesSpawner _spawner;

        public bool waitForAllDependencies => true;
        public override MapObjectTemplate template => _template;
        public override MapObjectTask task => _task;

        public override IMapActionsController actionsController => _actionsController;
        DefaultMapActionsController _actionsController = new DefaultMapActionsController();

        public void FinalizeInjection()
        {
            SpawnEnemies();
            FillActionsList();
        }

        public void SetTemplate(SiteTemplate template, Rng rng)
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
                _actionsController.AddLogic(action.CreateActionLogic());
            }
        }

        private void SpawnEnemies()
        {
            if (_tileStorage is null)
            {
                FillStorageWithWalkableTile();
            }

            if (_tileStorage.isEmpty) return;

            _enemiesFromSite = new List<Enemy>();
            var enemies = _template.enemies.GetCreatures(_rng);

            foreach (var enemyTemplate in enemies)
            {
                if (_tileStorage.Pull(_rng, out var position))
                {
                    var enemy = _spawner.SpawnEnemyAsChild(enemyTemplate, position, this);
                    _enemiesFromSite.Add(enemy);
                    enemy.OnDeath += RemoveDeathEnemy;
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

            _tileStorage = new TileStorage(tilesCount);

            for (int x = _posX - _template.width / 2; x <= _posX + _template.width / 2; x++)
            {
                for (int y = _posY - _template.height / 2; y <= _posY + _template.height / 2; y++)
                {
                    if (!TileIsWalkable(x, y)) continue;
                    _tileStorage.Push(new Vector3(x, y, 0));
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

        private void RemoveDeathEnemy(Enemy enemy)
        {
            if(!_enemiesFromSite.Contains(enemy)) return;
            _enemiesFromSite.Remove(enemy);
            
            if(_enemiesFromSite.Count > 0)
            {
                _task.taskText = ConstructKillString(_enemiesFromSite.Count);
            }
            else
            {
                _task = new MapObjectTask("Click to loot", false);
            }

            InvokeTaskEvent();
        }
    }
}

