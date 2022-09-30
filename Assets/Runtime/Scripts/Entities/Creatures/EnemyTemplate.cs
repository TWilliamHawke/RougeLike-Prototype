using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using Items;
using UnityEngine;

namespace Entities
{
	[CreateAssetMenu(fileName ="EnemyTemplate", menuName ="Entities/EnemyTemplate")]
	public class EnemyTemplate : ScriptableObject, IDamageSource, IHaveLoot
	{
		[SerializeField] string _bodyChar = "-";
		[SerializeField] Color _bodyColor = Color.white;
        [SerializeField] int _minDamage;
		[SerializeField] int _maxDamage;
		[SerializeField] DamageType _damageType;
		[SerializeField] int _health;
		[SerializeField] ResistSet _resists;
		[SerializeField] CreatureSoundKit _sounds;
		[SerializeField] LootTable _lootTable;
		[SerializeField] int _expForKill;
		
        public DamageType damageType => _damageType;
        public int minDamage => _minDamage;
        public int maxDamage => _maxDamage;
		public int health => _health;
		public string bodyChar => _bodyChar;
		public Color bodyColor => _bodyColor;
		public ResistSet resists => _resists;

        public AudioClip[] attackSounds => _sounds.attackSounds;
		public CreatureSoundKit sounds => _sounds;
        public LootTable lootTable => _lootTable;
		public int expForKill => _expForKill;
    }
}