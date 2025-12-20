namespace Entities.Stats
{
    public interface IStatsController
    {
        void AddObserver(IObserver<StatContainer> observer, StaticStat stat);
        ResourceContainer FindContainer(StoredResource stat);
        StatContainer FindContainer(StaticStat stat);
    }
}
