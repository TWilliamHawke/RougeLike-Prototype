using UnityEngine;

public static class MonoBehaviourExtension
{
    public static void Enable(this MonoBehaviour mono)
    {
        mono.gameObject.SetActive(true);
    }

    public static void Disable(this MonoBehaviour mono)
    {
        mono.gameObject.SetActive(false);
    }

    public static void DestroyChildren(this Component mono)
    {
        foreach (Transform children in mono.transform)
        {
            GameObject.Destroy(children.gameObject);
        }
    }

    public static void SetParent(this Component mono, Component parent)
    {
        mono.transform.SetParent(parent.transform);
    }

    public static T CreateChild<T>(this Component mono, T prefab, Vector3 position) where T : Component
    {
        T obj = GameObject.Instantiate(prefab, position, Quaternion.identity, mono.transform);

        return obj;
    }

    public static T CreateChild<T>(this Component mono, T prefab) where T : Component
    {
        T obj = GameObject.Instantiate(prefab, mono.transform);

        return obj;
    }

    public static bool TryStopCoroutine(this MonoBehaviour mono, Coroutine coroutine)
    {
        if (coroutine is null) return false;
        mono.StopCoroutine(coroutine);
        return true;
    }

    public static void StartInjection(this MonoBehaviour mono)
    {
        var injectors = mono.GetComponents<IManualInjector>();

        foreach (var injector in injectors)
        {
            if (injector.target != mono) continue;
            injector.AddTarget();
            break;
        }

    }
}

