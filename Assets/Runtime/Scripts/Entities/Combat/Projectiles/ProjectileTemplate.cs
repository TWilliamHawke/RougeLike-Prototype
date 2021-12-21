using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Combat
{
	[CreateAssetMenu(fileName = " ProjectileTemplate", menuName = "Entities/ProjectileTemplate")]
	public class ProjectileTemplate : ScriptableObject, IDamageSource
	{
	    [SerializeField] string _char = "o";
		[SerializeField] Color _color = Color.red;
		[SerializeField] float _speedMult = 1f;
		[Header("Damage Data")]
		[SerializeField] DamageType _damageType;
		[SerializeField] int _minDamage;
		[SerializeField] int _maxDamage;
		[Space(10)]
		[SerializeField] AudioClip[] _fireSounds;
		[SerializeField] AudioClip[] _impactSounds;

		public string bodyChar => _char;
		public float speedMult => _speedMult;
		public AudioClip[] fireSounds => _fireSounds;
		public AudioClip[] impactSounds => _impactSounds;
		public Color color => _color;

        int IDamageSource.minDamage => _minDamage;
        int IDamageSource.maxDamage => _maxDamage;
        DamageType IDamageSource.damageType => _damageType;
    }
}