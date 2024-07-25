using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public interface IEffect
    {
        IEffectSignature effectType { get; }
        string description { get; }
        Sprite icon { get; }
        bool CanApply(IEffectSource source, IAbilityTarget target);
    }

    public interface IEffectWithBonusValue
    {
        BonusValueType bonusType { get; }
    }

    public interface IEffectsIterator
    {
        IEnumerable<IStaticEffectData> GetEffects(IEffectSignature type);
        IEnumerable<IStaticEffectData> GetEffects();
    }
}