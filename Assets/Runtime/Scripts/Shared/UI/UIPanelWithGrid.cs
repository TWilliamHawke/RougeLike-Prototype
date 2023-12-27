using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIPanelWithGrid<T> : MonoBehaviour
{
    [SerializeField] UIDataElement<T> _layoutElementPrefab;
    [SerializeField] LayoutGroup _layout;

    protected abstract IEnumerable<T> _layoutElementsData { get; }
    protected LayoutGroup layout => _layout;

    List<IObserver<UIDataElement<T>>> _observers = new();
    //protected UIDataElement<T> prefab => _layoutElementPrefab;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void AddObserver(IObserver<UIDataElement<T>> observer)
    {
        _observers.Add(observer);
        foreach (Transform children in _layout.transform)
        {
            if (children.TryGetComponent<UIDataElement<T>>(out var element))
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

    protected virtual void UpdateLayout()
    {
        ClearLayout();

        foreach (var template in _layoutElementsData)
        {
            var uiElement = _layout.CreateChild(_layoutElementPrefab);
            uiElement.BindData(template);
            _observers.ForEach(observer => observer.AddToObserve(uiElement));
        }

    }

    protected void ClearLayout()
    {
        foreach (Transform children in _layout.transform)
        {

            if (children.TryGetComponent<UIDataElement<T>>(out var element))
            {
                _observers.ForEach(observer => observer.RemoveFromObserve(element));
            }
            Destroy(children.gameObject);
        }

    }

}
