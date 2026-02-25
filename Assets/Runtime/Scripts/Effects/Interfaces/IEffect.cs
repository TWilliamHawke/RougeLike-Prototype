using Abilities;
using UnityEngine;

namespace Effects
{
    public interface IEffect
    {
        IEffectSignature effectType { get; }
        string description { get; }
        Sprite icon { get; }
        bool isPositiveValueGood { get; }
        BonusValueType bonusType { get; }
        bool CanApply(IEffectSource source, IAbilityTarget target);
    }
}