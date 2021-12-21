using UnityEngine;

public abstract class UIDataElement<T> : MonoBehaviour
{
    public abstract void UpdateData(T data);
}