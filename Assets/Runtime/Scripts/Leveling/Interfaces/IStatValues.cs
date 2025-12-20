using UnityEngine.Events;

namespace Entities.Stats
{
    public interface IStatValues
    {
        int currentValue { get; }
        int maxValue { get; }
        event UnityAction<int> OnValueChange;
    }
}
