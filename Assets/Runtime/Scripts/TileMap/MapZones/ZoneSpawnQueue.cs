using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using Rng = System.Random;


namespace Map.Zones
{
    public class ZoneSpawnQueue
    {
        ISpawnZoneTemplate _template;
        Vector3Int _center;
        RandomStack<Vector3Int> _tileStorage;
        Component _parentComponent;

        List<IObserver<Entity>> _observers = new();
        Queue<SpawnData<IEntityTemplate>> _spawnQueue = new();

        public ZoneSpawnQueue(ISpawnZoneTemplate template, Component parentComponent)
        {
            _template = template;
            _parentComponent = parentComponent;
            _center = parentComponent.transform.position.ToInt();
            FillStorageWithWalkableTiles();
        }

        public void AddObserver(IObserver<Entity> observer)
        {
            _observers.Add(observer);
        }

        public void AddToQueue(CreaturesTable entitiesTable, Rng rng)
        {
            var templates = entitiesTable.GetTemplates(rng);
            templates.ForEach(template => AddToQueue(template, rng));
        }

        public void AddToQueue(IEntityTemplate template, Rng rng)
        {
            if (_tileStorage.TryPull(rng, out var position))
            {
                _spawnQueue.Enqueue(new SpawnData<IEntityTemplate>(template, position));
            }
        }

        public void SpawnAll(EntitiesSpawner spawner)
        {
            foreach (var data in _spawnQueue)
            {
                var entity = data.template.CreateEntity(spawner, data.position);
                _observers.ForEach(observer => observer.AddToObserve(entity));
                entity.SetParent(_parentComponent);
            }

            _spawnQueue.Clear();
        }

        private void FillStorageWithWalkableTiles()
        {
            int tilesCount = _template.size.x * _template.size.y;
            if (!_template.centerZoneIsWalkable)
            {
                tilesCount -= _template.centerZoneSize.x * _template.centerZoneSize.y;
            }

            _tileStorage = new RandomStack<Vector3Int>(tilesCount);
            var halfSize = _template.size / 2;

            for (int x = _center.x - halfSize.x; x <= _center.x + halfSize.x; x++)
            {
                for (int y = _center.y - halfSize.y; y <= _center.y + halfSize.y; y++)
                {
                    if (!TileIsWalkable(x, y)) continue;
                    _tileStorage.Push(new Vector3Int(x, y, 0));
                }
            }
        }

        private bool TileIsWalkable(int x, int y)
        {
            if (_template.centerZoneIsWalkable) return true;
            var halfCenterSize = _template.centerZoneSize / 2;

            if (x < _center.x - halfCenterSize.x || x > _center.x + halfCenterSize.x) return true;
            if (y < _center.y - halfCenterSize.y || y > _center.y + halfCenterSize.y) return true;

            return false;
        }
    }
}


