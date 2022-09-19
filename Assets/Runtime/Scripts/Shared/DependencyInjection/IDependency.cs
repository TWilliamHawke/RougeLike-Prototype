using UnityEngine.Events;

public interface IDependency
{
    bool isReadyForUse { get; }
    event UnityAction OnReadyForUse;
}
