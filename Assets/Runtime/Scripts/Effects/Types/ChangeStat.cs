using System.Collections;
using System.Collections.Generic;
using Entities.Stats;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "StatName", menuName = "Effects/Change Stat")]
    public class ChangeStat : Effect, IEffectWithBonusValue
    {
        [SerializeField] StaticStat _stat;
        [SerializeField] BonusValueType _bonusType = BonusValueType.flat;
        [SerializeField] ConditionsList _conditions;

        public BonusValueType bonusType => _bonusType;
        public override IEffectSignature effectType => _stat;

    }
}
