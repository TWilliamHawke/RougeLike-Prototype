using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InfoButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Sprite _activeBg;
    [SerializeField] Sprite _inactiveBg;
    [Header("UI Elements")]
    [SerializeField] Image _buttonBg;

    public event UnityAction OnClick;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }

    public void ToggleButton(bool isActive)
    {
        if (isActive)
        {
            _buttonBg.sprite = _activeBg;
        }
        else
        {
            _buttonBg.sprite = _inactiveBg;
        }
    }

}
