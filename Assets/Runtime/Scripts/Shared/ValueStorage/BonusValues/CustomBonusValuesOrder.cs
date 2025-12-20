using System.Collections.Generic;
using UnityEngine;

public class CustomBonusValuesOrder : ScriptableObject, IBonusValuesOrder
{
    [SerializeField] private List<BonusValueType> _order = new List<BonusValueType>();

    public IEnumerable<BonusValueType> GetOrder()
    {
        return _order;
    }
}