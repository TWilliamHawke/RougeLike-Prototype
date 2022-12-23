using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using Items;
using UnityEngine;

namespace Entities
{
	[CreateAssetMenu(fileName ="EnemyTemplate", menuName ="Entities/EnemyTemplate")]
	public class CreatureTemplate : EntityTemplate, IHaveLoot
	{
		[SerializeField] CreatureSoundKit _sounds;
        [SerializeField] int _minDamage;
		[SerializeField] int _maxDamage;
		[SerializeField] DamageType _damageType;
		[SerializeField] ResistSet _resists;
		[SerializeField] LootTable _lootTable;
		
        public override DamageType damageType => _damageType;
        public override int minDamage => _minDamage;
        public override int maxDamage => _maxDamage;
		public ResistSet resists => _resists;

        public AudioClip[] attackSounds => _sounds.attackSounds;
		public CreatureSoundKit sounds => _sounds;
        public LootTable lootTable => _lootTable;

        public override Entity CreateEntity(EntitiesSpawner spawner, Vector3 position)
        {
            return spawner.SpawnCreature(this, position);
        }
    }
}