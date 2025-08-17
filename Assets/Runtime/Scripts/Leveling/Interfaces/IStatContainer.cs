using UnityEngine.Events;

namespace Entities.Stats
{
    public interface IStatContainer
    {
        void SetBaseStatValue(int value);
    }

    public interface IStatValues
    {
        int currentValue { get; }
        int maxValue { get; }
        event UnityAction<int> OnValueChange;
    }

    public interface IStatsController
    {
        void AddObserver(IObserver<StaticStatStorage> observer, StaticStat stat);
        ResourceContainer FindContainer(StoredResource stat);
        StaticStatStorage FindContainer(StaticStat stat);
    }

    public interface IStatValueController
    {
        void ChangeStat(int value);
    }

    public interface ISafeStatController
    {
        int currentValue { get; }
        bool TryReduceStat(int value);
    }
}
