using System.Collections;
using System.Collections.Generic;
using Entities.Stats;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "Damage", menuName = "Effects/Damage Type")]
    public class ResourceChangeFactor : Effect, IEffectSignature
    {
        [SerializeField] BonusValueType _type;
        [SerializeField] StoredResource _targetStat;
        [SerializeField] StaticStat _resist;

        [SerializeField] StaticStat[] _factormods;

        public int AdjustValue(int baseValue, StatsContainer statsContainer, IEffectsIterator effects)
        {
            float updatedValue = baseValue;

            foreach (var stat in _factormods)
            {
                var storage = statsContainer.FindStorage(stat);
                int statValue = storage.GetAdjustedValue(effects);
                updatedValue = updatedValue * (1 + statValue * 0.01f);
            }

            return Mathf.FloorToInt(updatedValue);
        }

        public override void ApplyEffect(EffectsStorage storage, IEffectSource source, SourceEffectData effectData)
        {
            var statsStorage = storage.GetComponent<StatsContainer>();
            int newValue = effectData.magnitude;

            if (effectData.duration > 0)
            {
                var updatedEffect = effectData.Clone(newValue);
                storage.AddTemporaryEffect(updatedEffect);
            }
            else
            {
                var targetStatData = statsStorage.FindStorage(_targetStat);
                if (!isPositiveValueGood)
                {
                    newValue *= -1;
                }
                targetStatData.ChangeStat(newValue);
            }
        }
    }
}
