using System.Collections;
using System.Collections.Generic;
using Entities.Stats;
using UnityEngine;

namespace Effects
{
	[CreateAssetMenu(fileName = "Damage", menuName = "Effects/Damage Type")]
    public class DamageEffect : Effect
    {
        [SerializeField] StoredResource _damagedStat;
        [SerializeField] StaticStat _resist;

        [SerializeField] StaticStat[] _damageMods;
    }
}
