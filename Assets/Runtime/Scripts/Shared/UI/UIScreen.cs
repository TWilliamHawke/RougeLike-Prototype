using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIScreen : MonoBehaviour, IUIScreen
{
	public event UnityAction OnScreenOpen;
	public event UnityAction OnScreenClose;


    public void Close()
    {
        gameObject.SetActive(false);
		OnScreenClose?.Invoke();
    }

    public void Open()
    {
        gameObject.SetActive(false);
		OnScreenOpen?.Invoke();
    }
}


