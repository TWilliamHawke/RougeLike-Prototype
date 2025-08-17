using System.Collections;
using System.Collections.Generic;
using Entities.Stats;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "Damage", menuName = "Effects/Damage Type")]
    public class DamageStoredResource : ChangeStoredResource
    {
        [SerializeField] StaticStat _resist;

        protected override int AdjustValue(EffectsStorage storage, StatsStorage statsStorage, int value)
        {
            if (value <= 0) return 0;

            var resistContainer = statsStorage.FindContainer(_resist);
            int resistValue = resistContainer.GetAdjustedValue(storage);
            float resistEfficiency = 1 + (float)resistValue / value;
            value = Mathf.FloorToInt(value / resistEfficiency);
            return -value;
        }
    }

}
