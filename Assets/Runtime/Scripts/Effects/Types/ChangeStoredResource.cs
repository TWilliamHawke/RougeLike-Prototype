using Entities.Stats;
using UnityEngine;

namespace Effects
{
    public abstract class ChangeStoredResource : Effect, IEffectSignature
    {
        [SerializeField] BonusValueType _type;
        [SerializeField] StoredResource _targetStat;

        [SerializeField] StaticStat[] _factormods;

        protected abstract int AdjustValue(EffectsStorage storage, StatsStorage statsStorage, int value);

        public int ApplyEffectsToValue(int baseValue, StatsStorage statsStorage, IEffectsIterator effects)
        {
            float updatedValue = baseValue;

            foreach (var stat in _factormods)
            {
                var container = statsStorage.FindContainer(stat);
                //get the value of stat that can change baseValue
                //with all bonuses from effect list
                int statValue = container.GetAdjustedValue(effects);
                updatedValue *= 1 + statValue * 0.01f;
            }

            return Mathf.FloorToInt(updatedValue);
        }

        public override void ApplyEffect(EffectsStorage storage, IEffectSource source, SourceEffectData effectData)
        {

            if (effectData.duration > 0)
            {
                var updatedEffect = effectData.Clone();
                storage.AddTemporaryEffect(updatedEffect);
            }
            else
            {
                var statsStorage = storage.GetComponent<StatsStorage>();
                int newValue = AdjustValue(storage, statsStorage, effectData.magnitude);
                if (newValue == 0) return;
                var container = statsStorage.FindContainer(_targetStat);
                container.ChangeStat(newValue);
            }
        }
    }
}
