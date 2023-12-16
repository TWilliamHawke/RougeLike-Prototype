using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Entities.Stats
{
    public class StatsController : IStatController
    {
        public EffectStorage effectStorage { get; init; }
        Dictionary<Stat, IStatStorage> _statsStorage = new();

        public StatsController(IEffectTarget effectTarget)
        {
            effectStorage = new(effectTarget);
        }

        public void InitStat(Stat stat, int baseValue)
        {
            if (!_statsStorage.TryGetValue(stat, out var storage))
            {
                storage = CreateStorage(stat);
            }
            storage.SetStatValue(baseValue);
        }

        public void AddObserver<T>(IObserver<T>  observer, Stat stat) where T : class
        {
            if (!_statsStorage.TryGetValue(stat, out var storage))
            {
                storage = CreateStorage(stat);
            }

            if (storage is not T) return;

            observer.AddToObserve(storage as T);
        }

        private IStatStorage CreateStorage(Stat stat)
        {
            if (_statsStorage.TryGetValue(stat, out var storage))
            {
                return storage;
            }

            storage = stat.CreateStorage(this);
            _statsStorage.Add(stat, storage);

            return storage;
        }

    }
}
