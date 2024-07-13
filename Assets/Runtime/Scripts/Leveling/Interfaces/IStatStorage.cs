using UnityEngine.Events;

namespace Entities.Stats
{
    public interface IStatStorage
    {
        void SetBaseStatValue(int value);
    }

    public interface IStatValues
    {
        int currentValue { get; }
        int maxValue { get; }
        event UnityAction<int> OnValueChange;
    }

    public interface IStoredResourceEvents
    {
        event UnityAction<int> OnValueChange;
        event UnityAction OnReachMax;
        event UnityAction OnReachMin;
    }

    public interface IStatsController
    {
        void AddObserver<T, U>(IObserver<T>  observer, IStat<U> stat) where U : T;
        T FindStorage<T>(IStat<T> stat);
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
