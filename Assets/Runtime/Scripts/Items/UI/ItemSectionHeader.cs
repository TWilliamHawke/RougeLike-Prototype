using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Items
{
    public class ItemSectionHeader : MonoBehaviour, IPointerClickHandler
    {
        const string COLLAPSED_CHAR = "►";
        const string EXPANDED_CHAR = "▼";

        public event UnityAction OnClick;

        [SerializeField] TextMeshProUGUI _sectionStateText;
        [SerializeField] TextMeshProUGUI _sectionTitle;
        [SerializeField] LocalString _sectionName;

        public void ReplaceTitle(IInventorySectionData section)
        {
            string counter = section.count.ToString();

            if (!section.isInfinity)
            {
                counter = $"{section.count}/{section.capacity}";
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
}