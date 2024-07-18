using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Stats
{
    public class ResourceStorage : IStatStorage, IResourceStorageData, IObserver<IParentStat>, IStoredResourceEvents, IStatValueController, ISafeStatController
    {
        public int value => _value;

        public int currentValue => _value;
        public int maxValue => _parentStat.currentValue;

        int _value;
        float _pctOfMax = 1f;

        IParentStat _parentStat;

        public event UnityAction<int, int> OnValueChange;
        public event UnityAction OnReachMax;
        public event UnityAction OnReachMin;


        public void ChangeStat(int value)
        {
            SetStatValue(_value + value);
        }

        public void ChangeStatPctOfParent(float statMod)
        {
            SetStatValue((_parentStat.currentValue - _parentStat.minValue) * statMod);
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

            var oldPctOfMax = _pctOfMax;
            _value = Mathf.Clamp(newValue, _parentStat.minValue, _parentStat.currentValue);
            _pctOfMax = (float)_value / (_parentStat.currentValue - _parentStat.minValue);
            OnValueChange?.Invoke(_value, maxValue);

            if (_value == _parentStat.minValue && oldPctOfMax > 0f)
            {
                OnReachMin?.Invoke();
            }
            if (_value == _parentStat.currentValue && oldPctOfMax < 1f)
            {
                OnReachMax?.Invoke();
            }
        }

        public void AddToObserve(IParentStat target)
        {
            _parentStat = target;
            _value = _parentStat.currentValue;
            _pctOfMax = 1f;
            _parentStat.OnValueChange += AjustValueToParent;
            OnValueChange?.Invoke(_value, maxValue);
        }

        public void RemoveFromObserve(IParentStat target)
        {
        }

        private void SetStatValue(float value)
        {
            SetStatValue(Mathf.CeilToInt(value));
        }

        private void AjustValueToParent(int newParentValue)
        {
            SetStatValue(newParentValue * _pctOfMax);
        }

        public void SetBaseStatValue(int value)
        {
            _parentStat.SetBaseStatValue(value);
        }
    }
}
