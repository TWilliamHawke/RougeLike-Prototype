using UnityEngine.Events;

namespace Entities.Stats
{
    public interface IParentStat : IStatContainer
    {
        int currentValue { get; }
        int minValue { get; }
        event UnityAction<int> OnValueChange;
    }
}
