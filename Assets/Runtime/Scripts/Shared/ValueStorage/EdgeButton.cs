using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EdgeButton : EdgeUIElement, IPointerClickHandler
{
    [SerializeField] string _baseText;
    [SerializeField] bool _disableButton;
    [SerializeField] bool _changeColor;
    [Header("UI Elements")]
    [SerializeField] Color _defaultTextColor = Color.white;
    [SerializeField] Color _inactiveTextColor = Color.red;

    [SerializeField] Button _unityButton;
    [SerializeField] TextMeshProUGUI _buttonText;

    public event UnityAction OnClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!isActive) return;
        OnClick?.Invoke();
    }

    protected override void UpdateText(float edgeValue)
    {
        _buttonText.text = _baseText + edgeValue.ToString();
    }

    protected override void UpdateVisual()
    {
        if (_disableButton)
        {
            _unityButton.interactable = isActive;
        }
        if (_changeColor)
        {
            _buttonText.color = isActive ? _defaultTextColor : _inactiveTextColor;
        }
    }
}
