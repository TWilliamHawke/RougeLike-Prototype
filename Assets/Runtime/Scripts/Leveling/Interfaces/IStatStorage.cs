namespace Entities.Stats
{
    public interface IStatStorage
    {
        void AddObserver(IObserver<StatContainer> observer, StaticStat stat);
        ResourceContainer FindContainer(StoredResource stat);
        StatContainer FindContainer(StaticStat stat);
    }
}
