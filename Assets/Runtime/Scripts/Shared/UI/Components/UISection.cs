using UnityEngine.Events;

public interface IUISection
{
    event UnityAction<IUISection> OnSectionSelect;
    void Collapse();
    void Toggle();
    void UpdateSectionView();
}

public abstract class UISection<T, U> : UIDataElement<IUISectionData<T>>, IUISection where U : UIDataElement<T>
{
    IUISectionData<T> _sectionData;
    bool _isCollapsed = true;
    public event UnityAction<IUISection> OnSectionSelect;

    protected abstract UISectionHeader _header { get; }
    protected abstract UILayoutWithObserver<T, U> _layout { get; }

    void Start()
    {
        _header.OnClick += SelectSection;
    }

    void OnDestroy()
    {
        if (_sectionData == null) return;
        _sectionData.OnSectionDataChange -= UpdateSectionView;
    }

    public override void BindData(IUISectionData<T> sectionData)
    {
        _sectionData = sectionData;
        _sectionData.OnSectionDataChange += UpdateSectionView;
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

    public void UpdateSectionView()
    {
        _layout.UpdateLayout(_sectionData);
        _header.ReplaceTitle(_sectionData);
    }

    public void AddObserver(IObserver<U> observer)
    {
        _layout.AddObserver(observer);
    }

    private void SelectSection()
    {
        if (_sectionData?.filledSlotsCount == 0) return;
        OnSectionSelect?.Invoke(this);
    }
}
