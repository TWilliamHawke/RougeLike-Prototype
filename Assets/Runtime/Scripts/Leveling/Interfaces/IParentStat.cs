using UnityEngine.Events;

namespace Entities.Stats
{
    public interface IParentStat
    {
        int maxValue { get; }
        int minValue { get; }
        event UnityAction<int> OnValueChange;
    }
}
