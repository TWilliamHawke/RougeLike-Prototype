using UnityEngine;

//used as prefab so MonoBehaviour instead of interface
public abstract class UIDataElement<T> : MonoBehaviour
{
    public abstract void UpdateData(T data);
}