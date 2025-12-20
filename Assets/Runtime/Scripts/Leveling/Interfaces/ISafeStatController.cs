namespace Entities.Stats
{
    public interface ISafeStatController
    {
        int currentValue { get; }
        bool TryReduceStat(int value);
    }
}
