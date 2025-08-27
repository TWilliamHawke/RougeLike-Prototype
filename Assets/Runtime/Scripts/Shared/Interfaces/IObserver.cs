public interface IObserver<in T>
{
    void AddToObserve(T target);
    void RemoveFromObserve(T target);
}

public interface IObserversController<T>
{
    void AddObserver(IObserver<T> observer);
    void RemoveObserver(IObserver<T> observer);
}
