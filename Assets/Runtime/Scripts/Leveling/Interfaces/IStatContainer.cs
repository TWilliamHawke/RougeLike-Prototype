using System.Collections.Generic;
using Effects;

namespace Entities.Stats
{
    public interface IStatContainer
    {
        EffectStorage effectStorage { get; init; }
        void InitStat(StaticStat stat, int baseValue);
        void AddObserver(IObserver<StaticStatStorage> observer, StaticStat stat);
    }

    public interface IResourceContainer
    {
        void AddObserver(IObserver<ResourceStorage> observer, StoredResource stat);
        void InitStat(StoredResource stat, int baseValue);
    }
}
