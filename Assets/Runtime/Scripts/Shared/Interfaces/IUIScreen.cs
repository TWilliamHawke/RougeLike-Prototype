using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIScreen
{
    GameObject gameObject { get; }
    void Open()
	{
		gameObject.SetActive(true);
	}

    void Close()
	{
		gameObject.SetActive(false);
	}

    void Toggle()
    {
		//dont use gameObject.SetActive(!gameObject.activeSelf)
		//Open/Close can have another implementation
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
