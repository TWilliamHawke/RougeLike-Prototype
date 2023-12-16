using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field)]
public class PlusMinusBtnAttribute : PropertyAttribute
{
    public float amount { get; init; } = 1f;

    public PlusMinusBtnAttribute(float amount = 1f)
    {
        this.amount = amount;
    }
}

