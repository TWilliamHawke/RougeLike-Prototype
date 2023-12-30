using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DynamicButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI _buttonText;
    [SerializeField] Image _buttonIcon;

    public event UnityAction<DynamicButton> OnClick;

    public void SetText(string text)
    {
        _buttonText.text = text;
    }

    public void SetText(float text)
    {
        SetText(text.ToString());
    }

    public void SetIcon(Sprite icon)
    {
        _buttonIcon.sprite = icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(this);
    }
}
