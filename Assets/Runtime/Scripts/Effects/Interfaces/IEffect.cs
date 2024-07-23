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
}