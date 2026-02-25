using UnityEngine;

namespace Effects
{
    public class StaticEffectsStorage : AbstractEffectsStorage<IStaticEffectData>
    {
        public new void AddEffect(IEffectSource source, IStaticEffectData effectData)
        {
            base.AddEffect(source, effectData);
        }
    }
}