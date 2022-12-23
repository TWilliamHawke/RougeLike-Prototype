public interface IObserver<T>
{
    void AddToObserve(T target);
    void RemoveFromObserve(T target);
}
