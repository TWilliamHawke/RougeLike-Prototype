using UnityEngine.Events;

public interface IValueStorage
{
    int currentValue { get; }
    int maxValue { get; }
    event UnityAction<int> OnValueChange;
}
