using System.Collections.Generic;

namespace Effects
{
    public interface IEffectsIterator
    {
        IEnumerable<IStaticEffectData> GetEffects(IEffectSignature type);
        IEnumerable<IStaticEffectData> GetEffects();
    }
}