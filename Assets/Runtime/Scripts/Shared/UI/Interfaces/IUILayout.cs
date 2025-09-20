public interface IUILayout<T> : IUILayout
{
    U CreateLayoutElement<U>(U prefab) where U : T;
}

public interface IUILayout
{
    void ShowLayout();
    void HideLayout();
    void ClearLayout();
}
