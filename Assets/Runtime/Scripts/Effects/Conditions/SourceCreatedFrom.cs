using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class SourceCreatedFrom : EffectCondition
    {
        [SerializeField] MonoBehaviour _template;

        public override bool IsMeet(IEffectSource source, IEffectTarget target)
        {
            if (source is IHaveTemplate obj)
            {
                return obj.template == _template;
            }

            return source as MonoBehaviour == _template;
        }
    }
}
