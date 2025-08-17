using Entities.Stats;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "Restoration", menuName = "Effects/ResourceRestoration")]
    public class IncreaceStoredResource : ChangeStoredResource
    {
        protected override int AdjustValue(EffectsStorage storage, StatsStorage statsStorage, int value)
        {
            //apply healing efficiency of target
            return ApplyEffectsToValue(value, statsStorage, storage);
        }
    }

}
