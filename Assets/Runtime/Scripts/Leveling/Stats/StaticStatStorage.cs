using System;
using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Stats
{
    public class StaticStatStorage : ValueStorage, IStatStorage, IParentStat, IStatValueController
    {
        StaticStat _stat;

        public StaticStatStorage(StaticStat stat) : base(stat.capMin, stat.capMax, stat.defaultValue, !stat.applyMultFirst)
        {
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

        private void SetStatValue(int newValue)
        {
            SetNewValue(newValue);
        }

        public void SetBaseStatValue(int value)
        {
            SetNewValue(value);
        }

        public int GetAdjustedValue(IEffectsIterator effects)
        {
            ResetBonusValues();

            foreach (var effect in effects.GetEffects(_stat))
            {
                AddBonusValue(effect.bonusType, effect.power);
            }

            return GetFinalValue();
        }

    }
}
