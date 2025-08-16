using System.Collections;
using System.Collections.Generic;

namespace Effects
{
    public interface IEffectSource : IIconData, IBonusValueSource
    {
        IEnumerable<SourceEffectData> GetEffects();
	}

    public interface IStaticEffectData
    {
        IEffect effect { get; }
        int power { get; }
        IEffectSignature effectType { get; }
        BonusValueType bonusType { get; }
    }

    public interface IEffectSignature
    {

    }
}