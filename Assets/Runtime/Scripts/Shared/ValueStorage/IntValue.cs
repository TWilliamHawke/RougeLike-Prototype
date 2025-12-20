using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rng = System.Random;


[System.Serializable]
public struct IntValue
{
    [SerializeField] int _minValue;
    [SerializeField] int _maxValue;

    [SerializeField] bool _isRandom; //requires in inspector

    public int minValue => _minValue;
    public int maxValue => _maxValue;

    public IntValue(int minValue, int maxValue) : this()
    {
        _maxValue = Mathf.Max(minValue, maxValue, 0);
        _minValue = Mathf.Clamp(minValue, 0, _maxValue);
        _isRandom = _maxValue != _minValue;
    }

    public IntValue(int value) : this()
    {
        _minValue = Mathf.Max(value, 0);
        _maxValue = _minValue;
        _isRandom = false;
    }

    public static implicit operator IntValue(int val) => new IntValue(val);
    public static implicit operator int(IntValue val) => val.GetValue();

    public int GetValue()
    {
        if (_isRandom)
        {
            return GetRandomValue(_minValue, _maxValue);
        }
        return _minValue;
    }

    public int GetValue(Rng rng)
    {
        if(_isRandom)
        {
            return rng.Next(_minValue, _maxValue);
        }
        return _minValue;
    }

    public int GetAdjustedValue(int ajMin, int ajMax)
    {
        ajMin += _minValue;
        ajMax += _maxValue;
        ajMin = Mathf.Min(ajMin, ajMax);

        if (ajMax <= 0)
        {
            return 0;
        }

        if (ajMax == ajMin)
        {
            return ajMin;
        }

        return GetRandomValue(ajMin, ajMax);
    }

    private int GetRandomValue(int min, int max)
    {
        return Random.Range(min, max + 1);
    }

    public override string ToString()
    {
        return $"{_minValue}-${_maxValue}";
    }

}
