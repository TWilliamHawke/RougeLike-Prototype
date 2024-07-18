using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public abstract class EffectCondition : ScriptableObject
    {
        public abstract bool IsMeet(IEffectSource source, IAbilityTarget target);
    }
}
