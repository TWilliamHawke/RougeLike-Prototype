using System.Collections;
using System.Collections.Generic;

namespace Effects
{
    public interface IEffectSource : IBonusValueSource
	{
	    string displayName { get; }
	}

    public interface IStaticEffectData
    {
        IEffect effect { get; }
        int power { get; }
        IEffectSignature effectType { get; }
    }

    public interface IEffectSignature
    {

    }
}