using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects.Conditions
{
    public class TargetIsSetMember : EffectCondition
    {
        [SerializeField] DatabaseSet _set;

        public override bool IsMeet(IEffectSource source, IAbilityTarget target)
        {
            if (target is ScriptableObject obj)
            {
                return _set.Has(obj);
            }

            return false;
        }
    }
}
