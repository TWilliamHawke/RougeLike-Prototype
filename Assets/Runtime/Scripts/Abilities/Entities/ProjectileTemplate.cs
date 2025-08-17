using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using UnityEngine;

namespace Abilities
{
	[CreateAssetMenu(fileName = " ProjectileTemplate", menuName = "Entities/ProjectileTemplate")]
	public class ProjectileTemplate : ScriptableObject
	{
	    [SerializeField] string _char = "o";
		[SerializeField] Color _color = Color.red;
		[SerializeField] float _speedMult = 1f;
		[Header("Damage Data")]
		[SerializeField] int _AOERadius = 0;
		[SerializeField] float _AOEDamageMult = 1f;
		[Space(10)]
		[SerializeField] AudioClip[] _fireSounds;
		[SerializeField] AudioClip[] _impactSounds;

		public string bodyChar => _char;
		public float speedMult => _speedMult;
		public AudioClip impactSound => _impactSounds.GetRandom();
		public AudioClip fireSound => _fireSounds.GetRandom();
		public Color color => _color;

        public int radius => _AOERadius;
        public float aoeDamageMult => _AOEDamageMult;
    }
}