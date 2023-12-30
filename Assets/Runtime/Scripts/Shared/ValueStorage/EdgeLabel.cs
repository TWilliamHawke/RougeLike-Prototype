using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EdgeLabel : EdgeUIElement
{
    [SerializeField] bool _staticText;
    [SerializeField] string _baseText;
    [SerializeField] Behavior _behavior = Behavior.changeColor;
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI _labelText;
    [SerializeField] Color _defaultTextColor = Color.white;
    [SerializeField] Color _inactiveTextColor = Color.red;

    protected override void UpdateText(float edgeValue)
    {
        if (_staticText) return;
        _labelText.text = _baseText + edgeValue.ToString();
    }

    protected override void UpdateVisual()
    {
        if (_behavior == Behavior.changeColor)
        {
            _labelText.color = isActive ? _defaultTextColor : _inactiveTextColor;
        }
        else
        {
            gameObject.SetActive(isActive);
        }
    }

    enum Behavior
    {
        changeColor = 1,
        disable = 2,
    }
}
