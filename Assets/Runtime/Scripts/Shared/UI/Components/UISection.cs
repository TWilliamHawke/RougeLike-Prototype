using UnityEngine.Events;
using UnityEngine;

public abstract class UISection<U> : MonoBehaviour, IUISection where U : MonoBehaviour
{
    bool _isCollapsed = true;
    public event UnityAction<IUISection> OnSectionSelect;

    protected abstract bool _sectionDataIsEmpty { get; }
    protected abstract UISectionHeader _header { get; }
    protected abstract UILayoutWithObserver<U> _layout { get; }
    protected abstract void UpdateSectionLayout(IUILayout<U> parent);

    void Start()
    {
        _header.OnClick += SelectSection;
    }

    public void Collapse()
    {
        _layout.HideLayout();
        _header.ShowCollapcePointer();
        _isCollapsed = true;
    }

    public void Expand()
    {
        _layout.ShowLayout();
        _header.ShowExpandPointer();
        _isCollapsed = false;
    }

    public void Toggle()
    {
        if (_isCollapsed)
        {
            Expand();
            return;
        }
        Collapse();
    }

    public void UpdateSectionLayout()
    {
        _layout.ClearLayout();
        UpdateSectionLayout(_layout);
    }

    public void AddObserver(IObserver<U> observer)
    {
        _layout.AddObserver(observer);
    }

    protected void UpdateSectionTitle(IUISectionData sectionData)
    {
        _header.ReplaceTitle(sectionData);
    }

    private void SelectSection()
    {
        if (_sectionDataIsEmpty) return;
        OnSectionSelect?.Invoke(this);
    }
}
