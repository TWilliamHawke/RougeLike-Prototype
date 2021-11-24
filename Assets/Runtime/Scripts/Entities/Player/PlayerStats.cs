using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using UnityEngine;

namespace Entities.PlayerScripts
{
	public class PlayerStats : ScriptableObject
	{
	    [SerializeField] Inventory _inventory;
		[SerializeField] AudioClip[] _weaponSounds;

		Dictionary<DamageType, int> _resists = new Dictionary<DamageType, int>(5);

		public IDamageSource CalculateDamageData()
		{
			//HACK this code should return weapon or skill stats
			return new DamageSource(10, 20, DamageType.physical, _weaponSounds);
		}

		public Dictionary<DamageType, int> CalculateCurrentResists()
		{
			//apply effects, armour, etc...

			return _resists;
		}
    }
}