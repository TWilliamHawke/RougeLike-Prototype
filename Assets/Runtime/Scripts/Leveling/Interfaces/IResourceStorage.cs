using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Stats
{
    public interface IResourceStorage
    {
        int value { get; }
        void ChangeStatPctOfParent(float statMod);
        bool TryReduceStat(int value);
        event UnityAction<int, int> OnValueChange;
        event UnityAction OnReachMax;
        event UnityAction OnReachMin;
    }

    public interface IStoredResourceEvents
    {
        event UnityAction<int, int> OnValueChange;
        event UnityAction OnReachMax;
        event UnityAction OnReachMin;
    }

    public interface IResourceStorageData
    {
        int value { get; }
        int maxValue { get; }
        event UnityAction<int, int> OnValueChange;
    }
}
