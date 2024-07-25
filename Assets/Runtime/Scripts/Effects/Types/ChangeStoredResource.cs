using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "FactorName", menuName = "Effects/Change Stored Resource")]
    public class ChangeStoredResource : Effect, IEffectWithBonusValue
    {
        [SerializeField] ResourceChangeFactor _changeFactor;
        [SerializeField] BonusValueType _bonusType = BonusValueType.flat;

        public BonusValueType bonusType => _bonusType;
    }
}
