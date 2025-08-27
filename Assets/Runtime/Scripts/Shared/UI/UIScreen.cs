using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class UIScreen : MonoBehaviour, IUIScreen
{
    public event UnityAction OnScreenOpen;
    public event UnityAction OnScreenClose;

    public UnityEvent OnOpen;
    public UnityEvent OnClose;

    [SerializeField] bool CloseOnStart;

    private void Start()
    {
        if (!CloseOnStart) return;
        gameObject.SetActive(false);
    }

    public void Close()
    {
        if (!gameObject.activeSelf) return;
        gameObject.SetActive(false);
        OnScreenClose?.Invoke();
        OnClose?.Invoke();
    }

    public void Open()
    {
        CloseOnStart = false;
        if (gameObject.activeSelf) return;
        gameObject.SetActive(true);
        OnScreenOpen?.Invoke();
        OnOpen?.Invoke();
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


