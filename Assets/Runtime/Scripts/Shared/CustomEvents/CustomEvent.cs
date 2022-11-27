using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CustomEvent", menuName = "Musc/CustomEvent", order = 0)]
public class CustomEvent : ScriptableObject
{
	HashSet<CustomEventListener> _eventListeners = new();

	public void Invoke()
	{
		foreach(var listeners in _eventListeners)
		{
			listeners.RaiseEvent();
		}
	}

	public void Register(CustomEventListener eventListener)
	{
		_eventListeners.Add(eventListener);
	}

	public void Deregister(CustomEventListener eventListener)
	{
		_eventListeners.Remove(eventListener);
	}
}
