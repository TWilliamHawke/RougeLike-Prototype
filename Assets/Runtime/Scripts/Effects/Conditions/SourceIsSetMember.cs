using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;

namespace Effects.Conditions
{
    public class SourceIsSetMember : EffectCondition
    {
        [SerializeField] DatabaseSet _set;

        public override bool IsMeet(IEffectSource source, IAbilityTarget target)
        {
            if (source is ScriptableObject obj)
            {
                return _set.Has(obj);
            }

            return false;
        }
    }
}
