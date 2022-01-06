using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    [System.Serializable]
    public class SourceEffectData
    {
        [SerializeField] Effect _effect;
        [SerializeField] int _power;
        [SerializeField] int _duration;

        IEffectSource _source;

        public Effect effect => _effect;
        public int power => _power;
        public int duration => _duration;
        public IEffectSource source => _source;

        public void ApplyEffect(IEffectTarget target, IEffectSource effectSource)
        {
            _source = effectSource;

            if (duration > 0)
            {
                target.effectStorage.AddTemporaryEffect(this);
                return;
            }

            var instantEffect = _effect as IInstantEffect;

            if (instantEffect != null)
            {
                instantEffect.Apply(target, power);
                return;
            }

            target.effectStorage.AddStaticEffect(this);

        }

        public string EffectDescription()
        {
            return "Description";
        }
    }
}