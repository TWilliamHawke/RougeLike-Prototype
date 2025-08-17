namespace Entities.Stats
{
    public interface IResourceStorage
    {
        void AddObserver(IObserver<ResourceContainer> observer, StoredResource stat);
        void InitStat(StoredResource stat, int baseValue);
    }
}
