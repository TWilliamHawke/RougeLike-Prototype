using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using UnityEngine;

namespace Entities
{
	[CreateAssetMenu(fileName ="EnemyTemplate", menuName ="Entities/EnemyTemplate")]
	public class EnemyTemplate : ScriptableObject, IDamageSource
	{
        public DamageType damageType => _damageType;
        public int minDamage => _minDamage;
        public int maxDamage => _maxDamage;
		public int health => _health;
        public AudioClip[] attackSounds => _attackSounds;

        [SerializeField] int _minDamage;
		[SerializeField] int _maxDamage;
		[SerializeField] DamageType _damageType;
		[SerializeField] int _health;
		[SerializeField] AudioClip[] _attackSounds;

	}
}