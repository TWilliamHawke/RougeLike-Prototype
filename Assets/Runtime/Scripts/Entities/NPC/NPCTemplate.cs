using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using Items;
using UnityEngine;

namespace Entities.NPC
{
    [CreateAssetMenu(fileName = "NPCTemplate", menuName = "Entities/NPCTemplate")]
    public class NPCTemplate : EntityTemplate
    {
		[UseFileName]
		[SerializeField] string _npcName;
        [SerializeField] NPCInventoryTemplate _inventory;
        [SerializeField] Vector2Int _interactionZoneSize;

        public override int minDamage => throw new System.NotImplementedException();
        public override int maxDamage => throw new System.NotImplementedException();
        public override DamageType damageType => throw new System.NotImplementedException();

        public override Entity CreateEntity(EntitiesSpawner spawner, Vector3 position)
        {
            return spawner.SpawnNpc(this, position);
        }

        public INPCInventory CreateInventory()
        {
            return _inventory.CreateInventory();
        }
    }
}


