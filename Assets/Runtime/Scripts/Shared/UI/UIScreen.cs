using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class UIScreen : MonoBehaviour, IUIScreen
{
    public event UnityAction OnScreenOpen;
    public event UnityAction OnScreenClose;

    [SerializeField] bool CloseOnStart;

    private void Awake()
    {
        if (!CloseOnStart) return;
        gameObject.SetActive(false);

    }

    public void Close()
    {
        if (!gameObject.activeSelf) return;
        gameObject.SetActive(false);
        OnScreenClose?.Invoke();
    }

    public void Open()
    {
        if (gameObject.activeSelf) return;
        gameObject.SetActive(true);
        OnScreenOpen?.Invoke();
    }

    //should invoke events so setActive(!gameObject.activeSelf) doesn't meet
    public void Toggle()
    {
        if (gameObject.activeSelf)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

}


