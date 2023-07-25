using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.Events;
using Entities.NPCScripts;

namespace Entities
{
    public class EntitiesSpawner : MonoBehaviour
    {
        [InjectField] EntitiesManager _entitiesManager;

        [SerializeField] Creature _enemyPrefab;
        [SerializeField] NPC _npcPrefab;
        [SerializeField] Injector _entitiesManagerInjector;
        [SerializeField] Injector _selfInjector;


        // public void SpawnEnemy(EnemyTemplate template, Vector3 position)
        // {
        // 	SpawnEnemyAsChild(template, position, this);
        // }

        public NPC SpawnNpc(NPCTemplate template, Vector3 position)
        {
            var npc = Instantiate(_npcPrefab, position, Quaternion.identity);
            npc.BindTemplate(template);
            _entitiesManager.AddEntity(npc);
            return npc;
        }

        public Creature SpawnCreature(CreatureTemplate template, Vector3 position)
        {
            var creature = Instantiate(_enemyPrefab, position, Quaternion.identity);
            creature.BindTemplate(template);
            _entitiesManager.AddEntity(creature);
            return creature;
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

