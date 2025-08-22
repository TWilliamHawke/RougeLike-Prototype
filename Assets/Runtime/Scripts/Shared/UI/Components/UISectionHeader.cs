using Items;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UISectionHeader : MonoBehaviour, IPointerClickHandler
{
    const string COLLAPSED_CHAR = "►";
    const string EXPANDED_CHAR = "▼";

    public event UnityAction OnClick;

    [SerializeField] TextMeshProUGUI _sectionStateText;
    [SerializeField] TextMeshProUGUI _sectionTitle;

    public void ReplaceTitle(IUISectionData section)
    {
        string counter = section.filledSlotsCount.ToString();

        if (section.capacity > 0)
        {
            counter = $"{section.filledSlotsCount}/{section.capacity}";
        }

        _sectionTitle.text = $"{section.sectionName} ({counter})";
    }

    public void ShowCollapcePointer()
    {
        _sectionStateText.text = COLLAPSED_CHAR;
    }

    public void ShowExpandPointer()
    {
        _sectionStateText.text = EXPANDED_CHAR;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }
}
