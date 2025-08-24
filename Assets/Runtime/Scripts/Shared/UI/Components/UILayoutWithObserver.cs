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

    public virtual void UpdateLayout(IEnumerable<T> templates)
    {
        CleanLayout();
        templates.ForEach(template => CreateLayoutElement(template));
    }

    public Vector2Int GetLayoutSize()
    {
        if (_layout is GridLayoutGroup grid)
        {
            return grid.GetLayoutSize();
        }
        return new Vector2Int(1, _layout.gameObject.transform.childCount);
    }

    public void ShowLayout()
    {
        SetLayoutVisibility(true);
    }

    public void HideLayout()
    {
        SetLayoutVisibility(false);
    }

    protected U CreateLayoutElement(T template)
    {
        U uiElement = _layout.CreateChild(_layoutElementPrefab);
        uiElement.BindData(template);
        _observers.ForEach(observer => observer.AddToObserve(uiElement));
        return uiElement;
    }

    protected virtual void CleanLayout()
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
