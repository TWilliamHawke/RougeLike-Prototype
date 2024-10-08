using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;

namespace Effects
{
    public class TargetCreatedFrom : EffectCondition
    {
        [SerializeField] MonoBehaviour _template;

        public override bool IsMeet(IEffectSource source, IAbilityTarget target)
        {
            if (target is IHaveTemplate obj)
            {
                return obj.template == _template;
            }

            return target as MonoBehaviour == _template;
        }
    }
}
