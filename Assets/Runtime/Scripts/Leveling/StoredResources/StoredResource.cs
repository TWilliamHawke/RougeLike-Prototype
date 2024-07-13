using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Stats
{
    [CreateAssetMenu(fileName = "Stat", menuName = "Entities/CappedStat")]
    public class StoredResource : DisplayedObject, IStat<ResourceStorage>
    {
        static readonly int _minValue = 0;
        static readonly int _maxValue = System.Int32.MaxValue;

        [SerializeField] StaticStat _parentStat;

        public ResourceStorage CreateStorage(IStatContainer controller)
        {
            //TODO works only for init, should be ajusted for save/load
            var storage = new ResourceStorage();
        
            if (_parentStat)
            {
                controller.AddObserver(storage, _parentStat);
            }
            else
            {
                IParentStat dummy = new DummyParentStat(_minValue, _minValue);
                storage.AddToObserve(dummy);
                dummy.SetBaseStatValue(_maxValue);
            }

            controller.cappedStatStorage.Add(this, storage);
            return storage;
        }

        public ResourceStorage SelectStorage(IStatContainer controller)
        {
            if (!controller.cappedStatStorage.TryGetValue(this, out var storage))
            {
                storage = CreateStorage(controller);
            }
            return storage;
        }

        public struct DummyParentStat : IParentStat
        {
            public int currentValue => _currentValue;
            public int minValue => _minValue;

            int _currentValue;
            int _minValue;

            public event UnityAction<int> OnValueChange;

            public DummyParentStat(int currentValue, int minValue) : this()
            {
                _currentValue = currentValue;
                _minValue = minValue;
                OnValueChange?.Invoke(_currentValue);
            }

            public void SetBaseStatValue(int value)
            {
                _currentValue = value;
            }
        }
    }
}
