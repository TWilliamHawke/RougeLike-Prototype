using UnityEngine.Events;
using UnityEngine;

public abstract class UISection : MonoBehaviour, IUISection
{
    bool _isCollapsed = true;
    public event UnityAction<IUISection> OnSectionSelect;

    protected abstract bool _sectionDataIsEmpty { get; }
    protected abstract UISectionHeader _header { get; }
    protected abstract IUILayout _layout { get; }
    protected abstract void FillLayout();

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
        FillLayout();
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

public abstract class UISection<T, U> : UISection, IObserver<U> where U : UIDataElement<T>
{
    protected abstract UILayoutWithObserver<U> _observerLayout { get; }
    protected abstract U _slotPrefab { get; }
    public abstract void AddToObserve(U target);
    public abstract void RemoveFromObserve(U target);

    IUISectionData<T> _sectionData;

    protected override bool _sectionDataIsEmpty => _sectionData.filledSlotsCount == 0;

    void OnDestroy()
    {
        if (_sectionData == null) return;
        _sectionData.OnSectionDataChange -= UpdateSectionLayout;
    }

    public void BindData(IUISectionData<T> sectionData)
    {
        _sectionData = sectionData;
        _sectionData.OnSectionDataChange += UpdateSectionLayout;
        AddObserver(this);
    }

    public void AddObserver(IObserver<U> observer)
    {
        _observerLayout.AddObserver(observer);
    }

    protected override void FillLayout()
    {
        UpdateSectionTitle(_sectionData);
        foreach (var ability in _sectionData)
        {
            var slot = _observerLayout.CreateLayoutElement(_slotPrefab);
            slot.BindData(ability);
        }
    }

}
