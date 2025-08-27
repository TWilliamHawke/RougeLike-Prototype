using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UILayoutWithObserver<T> : UIElement, IUILayout<T>, IObserversController<T> where T : MonoBehaviour
{
    [SerializeField] LayoutGroup _layout;

    ObserversController<T> _observers = new();

    public void AddObserver(IObserver<T> observer)
    {
        _observers.AddObserver(observer);
    }

    public void RemoveObserver(IObserver<T> observer)
    {
        _observers.RemoveObserver(observer);
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
        _layout.gameObject.SetActive(true);
    }

    public void HideLayout()
    {
        _layout.gameObject.SetActive(false);
    }

    public U CreateLayoutElement<U>(U prefab) where U : T
    {
        var element = _layout.CreateChild(prefab);
        AddLayoutElement(element);
        return element;
    }

    public void AddLayoutElement(T uiElement)
    {
        uiElement.SetParent(_layout);
        _observers.AddTarget(uiElement);
    }

    public virtual void ClearLayout()
    {
        _observers.ClearTargets();
        foreach (Transform children in _layout.transform)
        {
            Destroy(children.gameObject);
        }
    }
}

public abstract class UILayoutWithObserver<T, U> : UILayoutWithObserver<U> where U : UIDataElement<T>
{
    [SerializeField] U _layoutElementPrefab;

    public virtual void UpdateLayout(IEnumerable<T> templates)
    {
        ClearLayout();
        templates.ForEach(template => CreateDataElement(template));
    }

    protected U CreateDataElement(T template)
    {
        U uiElement = CreateLayoutElement(_layoutElementPrefab);
        uiElement.BindData(template);
        return uiElement;
    }
}
