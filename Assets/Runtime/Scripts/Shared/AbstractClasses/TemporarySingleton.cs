using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporarySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance { get; private set; }

    void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(this);
        }
    }

    void OnDestroy()
    {
        if (instance != this) return;
        instance = null;
    }


}
