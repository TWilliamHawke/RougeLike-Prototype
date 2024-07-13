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

    public IntValue(int minValue, int maxValue) : this()
    {
        this._maxValue = Mathf.Max(minValue, maxValue, 0);
        this._minValue = Mathf.Clamp(minValue, 0, this._maxValue);
        _isRandom = this._maxValue != this._minValue;
    }

    public IntValue(int value) : this()
    {
        this._minValue = Mathf.Max(value, 0);
        this._maxValue = this._minValue;
        _isRandom = false;
    }

    public static implicit operator IntValue(int val) => new IntValue(val);

    public int GetValue()
    {
        if(_isRandom)
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

    public int GetAjustedValue(int ajMin, int ajMax)
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
