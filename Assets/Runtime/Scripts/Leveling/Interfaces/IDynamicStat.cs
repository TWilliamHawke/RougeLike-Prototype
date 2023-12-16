using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Stats
{
    public interface IDynamicStat
    {
        int value { get; }
        void ChangeStatPctOfParent(float statMod);
        bool TryReduceStat(int value);
        event UnityAction<int> OnValueChange;
        event UnityAction OnReachMax;
        event UnityAction OnReachMin;
    }
}
