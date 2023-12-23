using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.Events;
using Entities.NPC;

namespace Entities
{
    public class EntitiesSpawner : MonoBehaviour
    {
        [InjectField] EntitiesManager _entitiesManager;

        [SerializeField] Creature _enemyPrefab;
        [SerializeField] NPC.NPC _npcPrefab;

        List<Entity> _uninitedCreatures = new();

        public NPC.NPC SpawnNpc(NPCTemplate template, Vector3 position)
        {
            var npc = Instantiate(_npcPrefab, position, Quaternion.identity);
            npc.BindTemplate(template);
            AddEntity(npc);
            return npc;
        }

        public Creature SpawnCreature(CreatureTemplate template, Vector3 position)
        {
            var creature = Instantiate(_enemyPrefab, position, Quaternion.identity);
            creature.BindTemplate(template);
            AddEntity(creature);
            return creature;
        }

        //editor event
        public void AddAllEntities()
        {
            _uninitedCreatures.ForEach(entity => _entitiesManager.AddEntity(entity));
            _uninitedCreatures.Clear();
        }

        private void AddEntity(Entity entity)
        {
            if (_entitiesManager)
            {
                _entitiesManager.AddEntity(entity);
            }
            else
            {
                _uninitedCreatures.Add(entity);
            }
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

