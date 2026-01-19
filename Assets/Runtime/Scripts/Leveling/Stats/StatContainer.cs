using Effects;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Stats
{
    public class StatContainer : ValueStorage, IStatContainer, IParentStat, IStatValueController
    {
        StaticStat _stat;

        public event UnityAction<float> OnFloatValueChanged;

        public float floatValue => currentValue * _stat.floatValueMod;

        public StatContainer(StaticStat stat) : base(stat.minValue, stat.maxValue, stat.defaultValue, stat.bonusesOrder)
        {
            OnValueChange += TriggerFloatValueEvent;
            _stat = stat;
        }

        public void ChangeStat(int value)
        {
            SetNewValue(currentValue + value);
        }

        // public override int GetFinalValue()
        // {
        //     int finalValue = base.GetFinalValue();
        //     Debug.Log(_stat.name + " " + finalValue);

        //     if (_stat.minReductionMod > 0f)
        //     {
        //         int minFinalValue = NormalizeValue(currentValue * _stat.minReductionMod);
        //         finalValue = Math.Max(finalValue, minFinalValue);
        //     }

        //     return finalValue;
        // }

        public void SetBaseStatValue(int value)
        {
            SetNewValue(value);
        }

        public int GetAdjustedValue(IEffectsIterator effects)
        {
            ResetBonusValues();

            foreach (var effect in effects.GetEffects(_stat))
            {
                AddBonusValue(effect.bonusType, effect.magnitude);
            }

            return GetFinalValue();
        }

        private void TriggerFloatValueEvent(int value)
        {
            OnFloatValueChanged?.Invoke(value * _stat.floatValueMod);
        }

    }
}
