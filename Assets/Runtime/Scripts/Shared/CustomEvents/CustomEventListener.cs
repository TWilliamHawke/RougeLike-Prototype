using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomEventListener : MonoBehaviour
{
    [SerializeField] CustomEvent _customEvent;
    [SerializeField] UnityEvent _unityEvent;

    private void Awake()
    {
      _customEvent.Register(this);
    }

    private void OnDestroy()
    {
        _customEvent.Deregister(this);
    }

    public void RaiseEvent()
    {
        _unityEvent?.Invoke();
    }
}


