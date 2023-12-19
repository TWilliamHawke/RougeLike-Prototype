using UnityEngine.Events;

namespace Entities.Stats
{
    public interface IParentStat : IStatStorage
    {
        int currentValue { get; }
        int minValue { get; }
        event UnityAction<int> OnValueChange;
    }
}
