using System.Collections;
using System.Collections.Generic;
using Entities.Stats;
using UnityEngine;

namespace Effects
{
	[CreateAssetMenu(fileName = "StatName", menuName = "Effects/Change Stat")]
    public class ChangeStat : Effect
    {
        [SerializeField] StaticStat _stat;
        [SerializeField] ConditionsList _conditions;

        public StaticStat stat => _stat;
    }
}
