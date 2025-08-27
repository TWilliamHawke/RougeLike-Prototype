using UnityEngine.Events;

public interface IUISection
{
    event UnityAction<IUISection> OnSectionSelect;
    void Collapse();
    void Toggle();
    void UpdateSectionLayout();
}
