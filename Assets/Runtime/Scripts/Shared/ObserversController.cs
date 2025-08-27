using System.Collections.Generic;

public class ObserversController<T> : IObserversController<T>
{
    HashSet<IObserver<T>> _observers = new();
    List<T> _targets = new();

    public void AddObserver(IObserver<T> observer)
    {
        _observers.Add(observer);
        _targets.ForEach(element => observer.AddToObserve(element));
    }

    public void RemoveObserver(IObserver<T> observer)
    {
        _observers.Remove(observer);
        _targets.ForEach(element => observer.RemoveFromObserve(element));
    }

    public void AddTarget(T target)
    {
        _targets.Add(target);
        _observers.ForEach(observer => observer.AddToObserve(target));
    }

    public void RemoveTarget(T target)
    {
        RemoveFromObserve(target);
        _targets.Remove(target);
    }

    public void ClearTargets()
    {
        _targets.ForEach(element => RemoveFromObserve(element));
        _targets.Clear();
    }

    private void RemoveFromObserve(T target)
    {
        _observers.ForEach(observer => observer.RemoveFromObserve(target));
    }
}
