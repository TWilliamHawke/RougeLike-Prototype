using System.Collections.Generic;
using Effects;

namespace Entities.Stats
{
    public interface IStatContainer
    {
        EffectStorage effectStorage { get; init; }
        void InitStat<T>(IStat<T> stat, int baseValue) where T: IStatStorage;
        void AddObserver<T, U>(IObserver<T>  observer, IStat<U> stat) where U : T;
        Dictionary<StaticStat, StaticStatStorage> staticStatStorage { get; }
        Dictionary<StoredResource, ResourceStorage> cappedStatStorage { get; }
    }
}
