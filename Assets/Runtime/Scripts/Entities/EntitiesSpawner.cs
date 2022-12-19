using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.Events;
using Entities.NPCScripts;

namespace Entities
{
    public class EntitiesSpawner : MonoBehaviour, IInjectionTarget
    {
        [InjectField] EntitiesManager _entitiesManager;

        [SerializeField] Creature _enemyPrefab;
        [SerializeField] NPC _npcPrefab;
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

        public NPC SpawnNpc(SpawnData<NPCTemplate> spawnData, Component parent)
        {
            var npc = parent.CreateChild(_npcPrefab, spawnData.position);
            npc.BindTemplate(spawnData.template);
            _entitiesManager.AddEntity(npc);
            return npc;
        }

        public Entity SpawnEnemyAsChild(SpawnData<CreatureTemplate> spawnData, Component parent)
        {
            var enemy = parent.CreateChild(_enemyPrefab, spawnData.position);
            enemy.BindTemplate(spawnData.template);
            _entitiesManager.AddEntity(enemy);
            return enemy;
        }
    }

    public struct SpawnData<T>
    {
        public T template { get; init;}
        public Vector3Int position { get; init;}

        public SpawnData(T template, Vector3Int position)
        {
            this.template = template;
            this.position = position;
        }
    }
}

