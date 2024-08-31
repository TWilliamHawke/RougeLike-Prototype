using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field)]
public class HideIfAttribute : PropertyAttribute
{
    public string fieldName { get; init; }
    public object fieldValue { get; init; }

    public HideIfAttribute(string fieldName, object fieldValue)
    {
        this.fieldName = fieldName;
        this.fieldValue = fieldValue;
    }
    
}
