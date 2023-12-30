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

    protected virtual void UpdateLayout()
    {
        ClearLayout();

        foreach (var template in _layoutElementsData)
        {
            var uiElement = _layout.CreateChild(_layoutElementPrefab);
            uiElement.BindData(template);
        }
    }

    protected void ClearLayout()
    {
        foreach (Transform children in _layout.transform)
        {
            Destroy(children.gameObject);
        }
    }
}
