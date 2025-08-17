using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Abilities;

namespace Effects
{
    [System.Serializable]
    public class SourceEffectData : ISourceEffectData, IStaticEffectData
    {
        [SerializeField] Effect _effect;
        [SerializeField] IntValue _magnitude;
        [SerializeField] int _duration;

        const string MAGNITUDE_PATTERN = "%m";
        const string DURATION_PATTERN = "%d";
        const string DURATION_LOC_PATTERN = "effect_duration";

        public IEffect effect => _effect;
        public int magnitude => _magnitude;
        public int duration => _duration;

        public IEffectSignature effectType => _effect.effectType;

        public BonusValueType bonusType => _effect is IEffectWithBonusValue e ? e.bonusType : BonusValueType.none;

        public SourceEffectData(Effect effect, int power, int duration = 0)
        {
            _effect = effect;
            _magnitude = power;
            _duration = duration;
        }

        public SourceEffectData Clone(int newMagnitude = 0)
        {
            return new SourceEffectData(_effect, newMagnitude, _duration);
        }

        public void ApplyEffect(EffectsStorage storage, IEffectSource effectSource)
        {
            _effect.ApplyEffect(storage, effectSource, this);
        }

        public void AddDescription(ref StringBuilder sb, AbilityModifiers abilityMods)
        {
            sb.AppendLine(GetDescription(abilityMods));
        }

        public string GetDescription(AbilityModifiers abilityModifiers)
        {
            var magnitude = _magnitude * abilityModifiers.magnitudeMult;

            string description = LocalDictionary.GetLocalisedString(effect.description, new TextReplacer
            {
                pattern = MAGNITUDE_PATTERN,
                replacer = magnitude.ToString()
            });

            if (duration > 0)
            {
                string appendix = LocalDictionary.GetLocalisedString(DURATION_LOC_PATTERN, new TextReplacer
                {
                    pattern = DURATION_PATTERN,
                    replacer = _duration.ToString()
                });
                description = $"{description} {appendix}";
            }

            return description;
        }
    }
}