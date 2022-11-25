using UnityEngine;

public static class MonoBehaviourExtension
{
    public static void DestroyChildren(this Component mono)
	{
		foreach(Transform children in mono.transform)
		{
			GameObject.Destroy(children.gameObject);
		}
	}

	public static void SetParent(this Component mono, Component parent)
	{
		mono.transform.SetParent(parent.transform);
	}

	public static T CreateChild<T>(this Component mono, T prefab, Vector3 position) where T: Component
	{
		T obj = GameObject.Instantiate(prefab, position, Quaternion.identity, mono.transform);

		return obj;
	}

	public static bool TryStopCoroutine(this MonoBehaviour mono, Coroutine coroutine)
	{
		if(coroutine is null) return false;
		mono.StopCoroutine(coroutine);
		return true;
	}
}

