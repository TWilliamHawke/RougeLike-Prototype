using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;
using Type = System.Type;

namespace Entities.Stats
{
    public class StatsContainer : MonoBehaviour, IEntityComponent, IStatContainer, IResourceContainer, IStatsController
    {
        public Dictionary<StaticStat, StaticStatStorage> staticStatStorage { get; } = new();
        public Dictionary<StoredResource, ResourceStorage> cappedStatStorage { get; } = new();

        public void InitStat(StaticStat stat, int baseValue)
        {
            var storage = FindStorage(stat);
            storage.SetBaseStatValue(baseValue);
        }

        public void InitStat(StoredResource resource, int baseValue)
        {
            var storage = FindStorage(resource);
            storage.SetBaseStatValue(baseValue);
        }

        public void AddObserver(IObserver<StaticStatStorage> observer, StaticStat stat) 
        {
            StaticStatStorage storage = FindStorage(stat);
            observer.AddToObserve(storage);
        }

        public void AddObserver(IObserver<ResourceStorage> observer, StoredResource stat)
        {
            ResourceStorage storage = FindStorage(stat);
            observer.AddToObserve(storage);
        }

        public ResourceStorage FindStorage(StoredResource stat)
        {
            if (!cappedStatStorage.TryGetValue(stat, out var storage))
            {
                storage = stat.CreateStorage(this);
                cappedStatStorage.Add(stat, storage);
            }

            return storage;
        }

        public StaticStatStorage FindStorage(StaticStat stat)
        {
            if (!staticStatStorage.TryGetValue(stat, out var storage))
            {
                storage = stat.CreateStorage(this);
                staticStatStorage.Add(stat, storage);
            }

            return storage;
        }
    }
}
