using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Stats
{
    public class StaticStatStorage : IStatStorage, IParentStat
    {
        int _value;
        int IParentStat.maxValue => _value;
        int IParentStat.minValue => _stat.capMin;

        StaticStat _stat;
        EffectStorage _effectStorage;

        public event UnityAction<int> OnValueChange;

        public StaticStatStorage(EffectStorage effectStorage, StaticStat stat)
        {
            _effectStorage = effectStorage;
            _stat = stat;
            _value = stat.defaultValue;
        }

        public void ChangeStat(int value)
        {
            SetStatValue(_value + value);
        }

        public void SetStatValue(int newValue)
        {
            if (_value == newValue) return;
            _value = Mathf.Clamp(newValue, _stat.capMin, _stat.capMax);
            OnValueChange?.Invoke(_value);
        }
    }
}
