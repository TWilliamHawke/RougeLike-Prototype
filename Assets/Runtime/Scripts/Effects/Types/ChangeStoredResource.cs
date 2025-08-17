using Entities.Stats;
using UnityEngine;

namespace Effects
{
    public abstract class ChangeStoredResource : Effect, IEffectSignature
    {
        [SerializeField] BonusValueType _type;
        [SerializeField] StoredResource _targetStat;

        [SerializeField] StaticStat[] _factormods;

        protected abstract void ChangeResource(ResourceContainer container, int value);

        public int AdjustValue(int baseValue, StatsStorage statsContainer, IEffectsIterator effects)
        {
            float updatedValue = baseValue;

            foreach (var stat in _factormods)
            {
                var storage = statsContainer.FindContainer(stat);
                int statValue = storage.GetAdjustedValue(effects);
                updatedValue = updatedValue * (1 + statValue * 0.01f);
            }

            return Mathf.FloorToInt(updatedValue);
        }

        public override void ApplyEffect(EffectsStorage storage, IEffectSource source, SourceEffectData effectData)
        {
            var statsStorage = storage.GetComponent<StatsStorage>();
            int statChange = effectData.magnitude;

            if (effectData.duration > 0)
            {
                var updatedEffect = effectData.Clone(statChange);
                storage.AddTemporaryEffect(updatedEffect);
            }
            else
            {
                var targetStatData = statsStorage.FindContainer(_targetStat);
                ChangeResource(targetStatData, statChange);
            }
        }
    }
}
