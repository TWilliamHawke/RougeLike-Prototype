using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Stats
{
    public class CappedStatStorage : IStatStorage, IDynamicStat, IObserver<IParentStat>
    {
        public int value => _value;

        int _value;
        float _pctOfMax = 1f;

        IParentStat _parentStat;

        public event UnityAction<int> OnValueChange;
        public event UnityAction OnReachMax;
        public event UnityAction OnReachMin;


        public void ChangeStat(int value)
        {
            SetStatValue(_value + value);
        }

        public void ChangeStatPctOfParent(float statMod)
        {
            SetStatValue((_parentStat.maxValue - _parentStat.minValue) * statMod);
            _pctOfMax = statMod;
        }

        public bool TryReduceStat(int value)
        {
            if (_value - value < _parentStat.minValue) return false;

            SetStatValue(_value - value);
            return true;
        }

        public void SetStatValue(int newValue)
        {
            if (_value == newValue) return;

            _value = Mathf.Clamp(newValue, _parentStat.minValue, _parentStat.maxValue);
            _pctOfMax = (float)_value / (_parentStat.maxValue - _parentStat.minValue);
            OnValueChange?.Invoke(_value);

            if (_value == _parentStat.minValue)
            {
                OnReachMin?.Invoke();
            }
            if (_value == _parentStat.maxValue)
            {
                OnReachMax?.Invoke();
            }
        }

        public void AddToObserve(IParentStat target)
        {
            _parentStat = target;
            _value = _parentStat.maxValue;
            _pctOfMax = 1f;
            _parentStat.OnValueChange += AjustValueToParent;
            OnValueChange?.Invoke(_value);
        }

        public void RemoveFromObserve(IParentStat target)
        {
        }

        private void SetStatValue(float value)
        {
            SetStatValue(Mathf.Ceil(value));
        }

        private void AjustValueToParent(int newParentValue)
        {
            SetStatValue(newParentValue * _pctOfMax);
        }

    }
}
