using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour, IObserver<IValueStorage>
{
    [SerializeField] Image _fillImage;
    [SerializeField] TextMeshProUGUI _barText;
    [SerializeField] TextMode _textMode;

    IValueStorage _valueStorage;

    public void AddToObserve(IValueStorage target)
    {
        _valueStorage = target;
        target.OnValueChange += UpdateToStorage;
        UpdateToStorage();
    }

    public void RemoveFromObserve(IValueStorage target)
    {
        target.OnValueChange -= UpdateToStorage;
    }

    public void UpdateBar(int currentValue, int maxValue)
    {
        _fillImage.fillAmount = (float)currentValue / maxValue;
        SetBarText(currentValue, maxValue);
    }

    private void UpdateToStorage(int _ = 0)
    {
        UpdateBar(_valueStorage.currentValue, _valueStorage.maxValue);
    }

    private void SetBarText(int currentValue, int maxValue)
    {
        if (_textMode == TextMode.none) return;
        float floatValue = (float)currentValue / maxValue;

        _barText.text = _textMode switch
        {
            TextMode.currentValue => $"{currentValue}",
            TextMode.currentAndMax => $"{currentValue}/{maxValue}",
            TextMode.percent => $"{Mathf.Round(floatValue * 1000) / 10}%",
            TextMode.fraction => $"{Mathf.Round(floatValue * 100) / 100}",
            _ => "",
        };
    }

    enum TextMode
    {
        none,
        currentValue,
        currentAndMax,
        percent,
        fraction
    }

}


