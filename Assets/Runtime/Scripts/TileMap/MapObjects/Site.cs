using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

using Rng = System.Random;

namespace Map.Objects
{
    public class Site : MapObject, IInjectionTarget
    {
        SiteTemplate _template;
        int _currentEnemiesCount;
        TileStorage _tileStorage;

        int _posX;
        int _posY;

        [SerializeField] BoxCollider2D _collider;
        [SerializeField] Injector _enemySpawnerInjector;

        [InjectField] EntitiesSpawner _spawner;
        Rng _rng;

        public bool waitForAllDependencies => true;

        public override MapObjectTemplate template => _template;

        public void FinalizeInjection()
        {
            SpawnEnemies();
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

        void SpawnEnemies()
        {
            if (_tileStorage is null)
            {
                FillStorageWithWalkableTile();
            }

            if (_tileStorage.isEmpty) return;

            int enemiesCount = _rng.Next(_template.minEnimiesCount, _template.maxEnimiesCount);

            for (int i = 0; i < enemiesCount; i++)
            {
                if (_tileStorage.Pull(_rng, out var position))
                {
                    _spawner.SpawnEnemyAsChild(_template.enemies, position, this);
                }
            }
        }


        void FillStorageWithWalkableTile()
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

        bool TileIsWalkable(int x, int y)
        {
            if (_template.tilesIsWalkable) return true;

            if (x < _posX - _template.tilesWidth / 2 || x >= _posX + 1 + _template.tilesWidth / 2) return true;
            if (y < _posY - _template.tilesHeight / 2 || y >= _posY + 1 + _template.tilesHeight / 2) return true;

            return false;
        }
    }
}

