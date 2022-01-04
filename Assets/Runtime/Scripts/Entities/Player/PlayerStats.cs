using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Player
{
    public class PlayerStats : ScriptableObject, IManaComponent
    {
        [SerializeField] AudioClip[] _weaponSounds;

        Dictionary<DamageType, int> _resists = new Dictionary<DamageType, int>(5);

        int _maxMana = 100;
        int _currentMana = 100;

        public AudioClip[] attackSounds => _weaponSounds;
        public int maxMana => _maxMana;
        public int curentMana => _currentMana;

        public void SubscribeOnHealthEvents(Health health)
        {
            
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

        public bool TrySpendMana(int count)
        {
            if(count >= _currentMana)
            {
                _currentMana -= count;
                return true;
            }

            return false;
        }
    }
}