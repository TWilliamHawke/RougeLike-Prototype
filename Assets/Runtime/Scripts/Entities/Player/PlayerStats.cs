using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Player
{
    public class PlayerStats : ScriptableObject
    {
        [SerializeField] AudioClip[] _weaponSounds;

        Dictionary<DamageType, int> _resists = new Dictionary<DamageType, int>(5);

        int _currentHealth = 100;
        int _maxHealth = 100;

        public AudioClip[] attackSounds => _weaponSounds;

        public void SubscribeOnHealthComponent(Health health)
        {

        }

        private void OnEnable()
        {
            _currentHealth = _maxHealth;
        }

        public IDamageSource CalculateDamageData()
        {
            //HACK this code should return weapon or skill stats
            return new DamageSource(10, 20, DamageType.physical);
        }

        public Dictionary<DamageType, int> CalculateCurrentResists()
        {
            //apply effects, armour, etc...
            _resists[DamageType.physical] = 10;

            return _resists;
        }

    }
}