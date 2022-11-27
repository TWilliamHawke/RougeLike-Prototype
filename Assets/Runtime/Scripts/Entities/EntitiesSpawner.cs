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

        [SerializeField] Enemy _enemyPrefab;
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

		public Enemy SpawnEnemyAsChild(EnemyTemplate template, Vector3Int position, Component parent)
		{
			var enemy = parent.CreateChild(_enemyPrefab, position);
			enemy.Init(template);
			_entitiesManager.AddEnemy(enemy);
            return enemy;
		}
    }
}

