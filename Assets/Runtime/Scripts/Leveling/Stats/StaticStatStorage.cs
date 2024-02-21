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

        public override int GetFinalValue()
        {
            int finalValue = base.GetFinalValue();

            if (_stat.minReductionMod > 0f)
            {
                int minFinalValue = NormalizeValue(currentValue * _stat.minReductionMod);
                finalValue = Math.Max(finalValue, minFinalValue);
            }

            return finalValue;
        }

        private void SetStatValue(int newValue)
        {
            SetNewValue(newValue);
        }

        void IStatStorage.SetBaseStatValue(int value)
        {
            SetNewValue(value);
        }
    }
}
