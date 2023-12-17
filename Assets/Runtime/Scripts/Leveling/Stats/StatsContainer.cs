using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Entities.Stats
{
    public class StatsContainer : IStatContainer
    {
        public EffectStorage effectStorage { get; init; }

        public Dictionary<StaticStat, StaticStatStorage> staticStatStorage { get; } = new();
        public Dictionary<CappedStat, CappedStatStorage> cappedStatStorage { get; } = new();

        public StatsContainer(IEffectTarget effectTarget)
        {
            effectStorage = new(effectTarget);
        }

        public void InitStat<T>(Stat<T> stat, int baseValue) where T :IStatStorage
        {
            var storage = stat.SelectStorage(this);
            storage.SetStatValue(baseValue);
        }

        public void AddObserver<T, U>(IObserver<T>  observer, Stat<U> stat) where U : T
        {
            var storage = stat.SelectStorage(this);
            observer.AddToObserve(storage);
        }

        public T FindStorage<T, U>(Stat<U> stat) where U : T
        {
            return stat.SelectStorage(this);
        }
    }
}
