using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;

namespace Effects
{
    public class Effect : ScriptableObject, IEffect, IEffectSignature
    {
        [LocalisationKey]
        [SerializeField] string _description;
        [SpritePreview]
        [SerializeField] Sprite _icon;
        [SerializeField] bool _isPositiveValueGood = true;

        public Sprite icon => _icon;
        public string description => _description;
        public bool isPositiveValueGood => _isPositiveValueGood;

        public virtual IEffectSignature effectType => this;

        public virtual bool CanApply(IEffectSource source, IAbilityTarget target)
        {
            return true;
        }

        public virtual void ApplyEffect(EffectsStorage storage, IEffectSource source, SourceEffectData effectData)
        { 
            if (effectData.duration > 0)
            {
                storage.AddTemporaryEffect(effectData);
            }
            else
            {
                storage.AddStaticEffect(source, effectData);
            }
        }
    }
}