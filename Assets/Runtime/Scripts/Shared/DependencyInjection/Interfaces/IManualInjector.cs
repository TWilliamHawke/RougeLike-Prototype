using UnityEngine;

public interface IManualInjector
{
    void AddTarget();
    MonoBehaviour target { get; }
}


