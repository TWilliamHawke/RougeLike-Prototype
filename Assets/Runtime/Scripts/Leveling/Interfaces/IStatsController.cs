namespace Entities.Stats
{
    public interface IStatsController
    {
        void AddObserver(IObserver<StatContainer> observer, StaticStat stat);
        void InitStat(StoredResource stat, int baseValue);
        void InitStat(StaticStat stat, int baseValue);
    }
}
