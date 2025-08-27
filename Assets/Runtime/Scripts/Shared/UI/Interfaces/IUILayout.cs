public interface IUILayout<T>
{
    U CreateLayoutElement<U>(U prefab) where U : T;
}
