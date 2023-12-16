using Effects;

namespace Entities.Stats
{
    public interface IStatController
    {
        EffectStorage effectStorage { get; init; }
        void InitStat(Stat stat, int baseValue);
        void AddObserver<T>(IObserver<T>  observer, Stat stat) where T : class;
    }
}
