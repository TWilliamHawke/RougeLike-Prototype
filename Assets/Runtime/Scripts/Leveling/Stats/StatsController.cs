using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Entities.Stats
{
    public class StatsController
    {
        public EffectStorage effectStorage { get; init; }


        public StatsController(IEffectTarget effectTarget)
        {
            effectStorage = new(effectTarget);
        }

        Dictionary<Stat, StatStorage> _statsStorage = new();

        public StatStorage InitStat(Stat stat, int baseValue)
        {
            if (stat.parentStat != null)
            {
                InitStat(stat.parentStat, baseValue);
            }

            if (!_statsStorage.TryGetValue(stat, out var storage))
            {
                storage = CreateStorage(stat);
            }
            storage.SetStatValue(baseValue);
            return storage;
        }

        public void AddObserver(IObserver<IStatData> observer, Stat stat)
        {
            if (!_statsStorage.TryGetValue(stat, out var storage))
            {
                storage = CreateStorage(stat);
            }

            observer.AddToObserve(storage);
        }

        private StatStorage CreateStorage(Stat stat)
        {
            if (_statsStorage.TryGetValue(stat, out var storage))
            {
                return storage;
            }

            if (stat.parentStat is null)
            {
                storage = new StatStorage(stat, effectStorage);
                _statsStorage.Add(stat, storage);
            }
            else
            {
                var parent = CreateStorage(stat.parentStat);
                storage = new StatStorage(parent, effectStorage);
            }

            return storage;
        }

    }
}
