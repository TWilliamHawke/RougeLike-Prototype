namespace Entities.Stats
{
    public interface IStatStorage
    {
        void InitStat(StaticStat stat, int baseValue);
        void AddObserver(IObserver<StatContainer> observer, StaticStat stat);
    }
}
