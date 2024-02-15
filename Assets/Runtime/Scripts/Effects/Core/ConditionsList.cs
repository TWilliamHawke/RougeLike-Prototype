using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    [System.Serializable]
    public class ConditionsList
    {
        [SerializeField] EffectCondition[] _conditions;

        public bool CanApply(IEffectSource source, IEffectTarget target)
        {
            foreach (var condition in _conditions)
            {
                if (condition == null) continue;
                if (!condition.IsMeet(source, target)) return false;
            }

            return true;
        }

    }
}
