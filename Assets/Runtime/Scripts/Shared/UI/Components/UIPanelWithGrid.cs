using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIPanelWithGrid<T> : MonoBehaviour
{
    [SerializeField] UIDataElement<T> _layoutElementPrefab;
    [SerializeField] LayoutGroup _layout;

    protected abstract IEnumerable<T> _layoutElementsData { get; }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    protected void SetLayoutVisibility(bool state)
    {
        _layout.gameObject.SetActive(state);
    }

    public virtual void UpdateLayout(IEnumerable<T> data)
    {
        ClearLayout();

        foreach (var template in data)
        {
            var uiElement = _layout.CreateChild(_layoutElementPrefab);
            uiElement.BindData(template);
        }
    }

    protected virtual void UpdateLayout()
    {
        UpdateLayout(_layoutElementsData);
    }

    protected void ClearLayout()
    {
        foreach (Transform children in _layout.transform)
        {
            Destroy(children.gameObject);
        }
    }
}
