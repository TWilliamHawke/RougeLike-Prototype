public interface IObserver<in T>
{
    void AddToObserve(T target);
    void RemoveFromObserve(T target);
}
