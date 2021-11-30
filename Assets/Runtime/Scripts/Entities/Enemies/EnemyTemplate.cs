using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using UnityEngine;

namespace Entities
{
	[CreateAssetMenu(fileName ="EnemyTemplate", menuName ="Entities/EnemyTemplate")]
	public class EnemyTemplate : ScriptableObject, IDamageSource, IHaveInjureSounds
	{
		[SerializeField] string _bodyChar = "-";
		[SerializeField] Color _bodyColor = Color.white;
        [SerializeField] int _minDamage;
		[SerializeField] int _maxDamage;
		[SerializeField] DamageType _damageType;
		[SerializeField] int _health;
		[SerializeField] AudioClip[] _attackSounds;
		[SerializeField] AudioClip[] _deathSounds;
		 
		
        public DamageType damageType => _damageType;
        public int minDamage => _minDamage;
        public int maxDamage => _maxDamage;
		public int health => _health;
        public AudioClip[] attackSounds => _attackSounds;
        public AudioClip[] deathSounds => _deathSounds;
		public string bodyChar => _bodyChar;
		public Color bodyColor => _bodyColor;



	}
}