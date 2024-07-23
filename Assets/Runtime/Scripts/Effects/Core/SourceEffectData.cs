using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace Effects
{
    [System.Serializable]
    public class SourceEffectData : IHaveDescription, IStaticEffectData
    {
        [SerializeField] Effect _effect;
        [SerializeField] int _power;
        [SerializeField] int _duration;

        const string magnitudePattern = "%m";
        const string durationPattern = "%d";
        const string duaration_loc_key = "effect_duration";

        public IEffect effect => _effect;
        public int power => _power;
        public int duration => _duration;

        public IEffectSignature effectType => _effect.effectType;

        public void ApplyEffect(EffectsStorage storage, IEffectSource effectSource)
        {
            if (duration > 0)
            {
                storage.AddTemporaryEffect(this);
                return;
            }

            if (_effect is IInstantEffect instantEffect)
            {
                instantEffect.Apply(storage, power);
                return;
            }

            storage.AddStaticEffect(effectSource, this);

        }

        public void AddDescription(ref StringBuilder sb)
        {
            sb.AppendLine(CreateDescription());
        }

        public string CreateDescription()
        {
            string description = LocalDictionary.GetLocalisedString(effect.description, new TextReplacer
            {
                pattern = magnitudePattern,
                replacer = _power.ToString()
            });

            if (duration > 0)
            {
                string appendix = LocalDictionary.GetLocalisedString(duaration_loc_key, new TextReplacer
                {
                    pattern = duaration_loc_key,
                    replacer = _duration.ToString()
                });
                description = $"{description} {appendix}";
            }

            return description;
        }
    }
}