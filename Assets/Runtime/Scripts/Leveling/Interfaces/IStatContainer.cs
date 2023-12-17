using System.Collections.Generic;
using Effects;

namespace Entities.Stats
{
    public interface IStatContainer
    {
        EffectStorage effectStorage { get; init; }
        void InitStat<T>(Stat<T> stat, int baseValue) where T: IStatStorage;
        void AddObserver<T, U>(IObserver<T>  observer, Stat<U> stat) where U : T;
        Dictionary<StaticStat, StaticStatStorage> staticStatStorage { get; }
        Dictionary<CappedStat, CappedStatStorage> cappedStatStorage { get; }
    }
}
