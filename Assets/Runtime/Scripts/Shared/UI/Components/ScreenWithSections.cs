using System.Collections.Generic;
using UnityEngine;

public abstract class ScreenWithSections<T> : MonoBehaviour, IObserver<T> where T : IUISection
{
    [SerializeField] UIScreen _screen;

    HashSet<IUISection> _sections = new();

    protected abstract IObserversController<T> _layout { get; }

    protected abstract void CreateSections();

    void Awake()
    {
        _layout.AddObserver(this);
        _sections.ForEach(s => s.OnSectionSelect += ToggleSection);
        _screen.OnScreenOpen += SetDefaultScreenView;

        CreateSections();
    }

    public void AddSectionObservers(IObserver<T> observer)
    {
        _layout.AddObserver(observer);
    }

    protected void SetDefaultScreenView()
    {
        _sections.ForEach(s => s.UpdateSectionLayout());
        _sections.ForEach(s => s.Collapse());
    }

    protected void ToggleSection(IUISection selectedSection)
    {
        foreach (var section in _sections)
        {
            if (section == selectedSection)
            {
                section.Toggle();
                continue;
            }
            section.Collapse();
        }
    }

    void IObserver<T>.AddToObserve(T target)
    {
        target.OnSectionSelect += ToggleSection;
        _sections.Add(target);
    }

    void IObserver<T>.RemoveFromObserve(T target)
    {
        target.OnSectionSelect -= ToggleSection;
        _sections.Remove(target);
    }
}
