using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using UnityEngine;

namespace Entities
{
	[CreateAssetMenu(fileName ="EnemyTemplate", menuName ="Entities/EnemyTemplate")]
	public class EnemyTemplate : ScriptableObject, IDamageSource
	{
        public DamageType damageType => throw new System.NotImplementedException();
        public int minDamage => throw new System.NotImplementedException();
        public int maxDamage => throw new System.NotImplementedException();
		public int health => _health;
        public AudioClip[] attackSounds => throw new System.NotImplementedException();

        [SerializeField] int _minDamage;
		[SerializeField] int _maxDamage;
		[SerializeField] DamageType _damageType;
		[SerializeField] int _health;

	}
}