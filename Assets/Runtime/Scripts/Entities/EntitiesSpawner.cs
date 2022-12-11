using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class EntitiesSpawner : MonoBehaviour, IInjectionTarget
    {
        [InjectField] EntitiesManager _entitiesManager;

        [SerializeField] Creature _enemyPrefab;
        [SerializeField] Injector _entitiesManagerInjector;
        [SerializeField] Injector _selfInjector;

        public bool waitForAllDependencies => true;

        private void Awake()
        {
            _selfInjector.SetDependency(this);
            _entitiesManagerInjector.AddInjectionTarget(this);
        }

        public void FinalizeInjection()
        {
        }

        // public void SpawnEnemy(EnemyTemplate template, Vector3 position)
        // {
        // 	SpawnEnemyAsChild(template, position, this);
        // }

        public Entity SpawnEnemyAsChild(EntitySpawnData spawnData, Component parent)
        {
            var enemy = parent.CreateChild(_enemyPrefab, spawnData.position);
            enemy.BindTemplate(spawnData.template);
            _entitiesManager.AddEnemy(enemy);
            return enemy;
        }
    }

    public struct EntitySpawnData
    {
        public CreatureTemplate template { get; init;}
        public Vector3Int position { get; init;}

        public EntitySpawnData(CreatureTemplate template, Vector3Int position)
        {
            this.template = template;
            this.position = position;
        }
    }
}

