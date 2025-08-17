using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;
using Type = System.Type;

namespace Entities.Stats
{
    public class StatsStorage : MonoBehaviour, IEntityComponent, IStatStorage, IResourceStorage, IStatsController
    {
        public Dictionary<StaticStat, StaticStatStorage> staticStatStorage { get; } = new();
        public Dictionary<StoredResource, ResourceContainer> cappedStatStorage { get; } = new();

        public void InitStat(StaticStat stat, int baseValue)
        {
            var storage = FindContainer(stat);
            storage.SetBaseStatValue(baseValue);
        }

        public void InitStat(StoredResource resource, int baseValue)
        {
            var storage = FindContainer(resource);
            storage.SetBaseStatValue(baseValue);
        }

        public void AddObserver(IObserver<StaticStatStorage> observer, StaticStat stat) 
        {
            StaticStatStorage storage = FindContainer(stat);
            observer.AddToObserve(storage);
        }

        public void AddObserver(IObserver<ResourceContainer> observer, StoredResource stat)
        {
            ResourceContainer storage = FindContainer(stat);
            observer.AddToObserve(storage);
        }

        public ResourceContainer FindContainer(StoredResource stat)
        {
            if (!cappedStatStorage.TryGetValue(stat, out var storage))
            {
                storage = stat.CreateStorage(this);
                cappedStatStorage.Add(stat, storage);
            }

            return storage;
        }

        public StaticStatStorage FindContainer(StaticStat stat)
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
