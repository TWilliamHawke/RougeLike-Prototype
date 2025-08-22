using UnityEngine;

//uses as prefab so MonoBehaviour instead of interface
//uses for children of layout
public abstract class UIDataElement<T> : MonoBehaviour
{
    public abstract void BindData(T data);
}