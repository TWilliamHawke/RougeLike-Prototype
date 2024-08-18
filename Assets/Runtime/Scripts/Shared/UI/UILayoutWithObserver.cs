using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UILayoutWithObserver<T, U> : UIElement where U : UIDataElement<T>
{
    [SerializeField] U _layoutElementPrefab;
    [SerializeField] LayoutGroup _layout;

    HashSet<IObserver<U>> _observers = new();

    public void AddObserver(IObserver<U> observer)
    {
        _observers.Add(observer);
        foreach (Transform children in _layout.transform)
        {
            if (children.TryGetComponent<U>(out var element))
            {
                observer.AddToObserve(element);
            }
        }
    }

    public void UpdateLayout(IEnumerable<T> data)
    {
        ClearLayout();

        foreach (var template in data)
        {
            var uiElement = _layout.CreateChild(_layoutElementPrefab);
            uiElement.BindData(template);
            _observers.ForEach(observer => observer.AddToObserve(uiElement));
        }
    }

    protected virtual void ClearLayout()
    {
        foreach (Transform children in _layout.transform)
        {
            if (children.TryGetComponent<U>(out var element))
            {
                _observers.ForEach(observer => observer.RemoveFromObserve(element));
            }
            Destroy(children.gameObject);
        }
    }

    protected void SetLayoutVisibility(bool active)
    {
        _layout.gameObject.SetActive(active);
    }
}
