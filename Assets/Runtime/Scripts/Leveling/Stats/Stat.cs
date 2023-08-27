using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Stats
{
    [CreateAssetMenu(fileName ="Stat", menuName ="Entities/Stat")]
    public class Stat : DisplayedObject, IParentStat
    {
        [SerializeField] bool _applyMultFirst = true;
        [SerializeField] int _capMin = 0;
        [SerializeField] int _capMax = System.Int32.MaxValue;
        [SerializeField] int _defaultValue = 100;
        [SerializeField] Stat _parentStat;


        public int defaultValue => _defaultValue;
        public int capMax => _capMax;
        public int capMin => _capMin;
        public Stat parentStat => _parentStat;

        int IParentStat.maxValue => _capMax;
        int IParentStat.minValue => _capMin;
        int IParentStat.baseValue => _defaultValue;

        public event UnityAction<int> OnValueChange;

        public int ApplyStatMods(int baseValue, int pctBonus, int flatBonus)
        {
            if (_applyMultFirst)
            {
                return Mathf.FloorToInt(baseValue * (1 + pctBonus / 100f)) + flatBonus;
            }

            return Mathf.FloorToInt((baseValue + flatBonus) * (1 + pctBonus / 100f));
        }

    }
}
