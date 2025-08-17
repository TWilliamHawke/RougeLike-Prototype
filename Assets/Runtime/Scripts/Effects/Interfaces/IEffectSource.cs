using System.Collections;
using System.Collections.Generic;
using Abilities;

namespace Effects
{
    public interface IEffectSource : IIconData, IBonusValueSource
    {
        IEnumerable<ISourceEffectData> GetEffects();
	}

    public interface IStaticEffectData
    {
        IEffect effect { get; }
        int magnitude { get; }
        IEffectSignature effectType { get; }
        BonusValueType bonusType { get; }
    }

    public interface ISourceEffectData
    {
        int duration { get; }
        void ApplyEffect(EffectsStorage storage, IEffectSource effectSource);
        string GetDescription(AbilityModifiers abilityModifiers);
    }

    public interface IEffectSignature
    {

    }
}