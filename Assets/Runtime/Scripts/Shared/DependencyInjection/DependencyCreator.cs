using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependencyCreator : MonoBehaviour
{
    [SerializeField] MonoBehaviour _dependency;
    [SerializeField] Injector _injector;
    [SerializeField] DependencyReadyTrigger _readyTrigger = DependencyReadyTrigger.allInjectFieldsIsFull;

    private void Awake()
    {
        _injector.SetDependency(_dependency, _readyTrigger);
    }
}
