using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Stats
{
    public class StatStorage : IParentStat, IStatData, IStatTools
    {
        IParentStat _stat;
        EffectStorage _effectStorage;
        int _value;
        float _pctOfMax = 1f;

        public int ajustedValue => _value;

        int IParentStat.maxValue => ajustedValue;
        int IParentStat.minValue => _stat.minValue;
        int IParentStat.baseValue => _value;

        public event UnityAction<int> OnValueChange;
        public event UnityAction OnReachMaxCap;
        public event UnityAction OnReachMinCap;

        public StatStorage(IParentStat stat, EffectStorage effectStorage)
        {
            _stat = stat;
            _effectStorage = effectStorage;
            _value = stat.baseValue;

            if (stat is IDynamicStat)
            {
                (stat as IDynamicStat).OnValueChange += AjustValueToParent;
            }
        }

        public void SetStatValue(int newValue)
        {
            _value = Mathf.Clamp(newValue, _stat.minValue, _stat.maxValue);
            _pctOfMax = (float)_value / (_stat.maxValue - _stat.minValue);
            OnValueChange?.Invoke(_value);

            if (_value == _stat.minValue)
            {
                OnReachMinCap?.Invoke();
            }
            if (_value == _stat.maxValue)
            {
                OnReachMaxCap?.Invoke();
            }
        }

        public void ChangeStat(int value)
        {
            SetStatValue(_value + value);
        }

        public void ChangeStatPctOfParent(float statMod)
        {
            SetStatValue((_stat.maxValue - _stat.minValue) * statMod);
        }

        public bool TryReduceStat(int value)
        {
            if (_value - value < _stat.minValue) return false;

            SetStatValue(_value - value);
            return true;
        }

        private void SetStatValue(float value)
        {
            SetStatValue((int)value);
        }

        private void AjustValueToParent(int newParentValue)
        {
            SetStatValue(newParentValue * _pctOfMax);
        }

    }

    public interface IStatTools
    {
        int ajustedValue { get; }
        void ChangeStat(int value);
        void ChangeStatPctOfParent(float statMod);
        bool TryReduceStat(int value);
    }

    public interface IStatData : IDynamicStat
    {
        int ajustedValue { get; }
    }

    public interface IParentStat
    {
        int maxValue { get; }
        int minValue { get; }
        int baseValue { get; }
        event UnityAction<int> OnValueChange;
    }

    public interface IDynamicStat
    {
        event UnityAction<int> OnValueChange;
        event UnityAction OnReachMaxCap;
        event UnityAction OnReachMinCap;
    }
}
