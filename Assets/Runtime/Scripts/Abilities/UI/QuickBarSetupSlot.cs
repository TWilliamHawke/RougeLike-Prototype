using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Abilities
{
    public class QuickBarSetupSlot : AbilityButton, IPointerClickHandler
    {
        [SerializeField] TextMeshProUGUI _abilityName;

        public event UnityAction<int> OnSlotClick;
        public int slotIndex { get; set; }

        public void UpdateSlotGraphic(IAbilityContainerData data)
        {
            if (data is null)
            {
                SetEmptySlotData();
                return;
            }

            SetSlotData(data);
        }

        private void SetSlotData(IAbilityContainerData data)
        {
            _abilityName.text = $"{slotIndex + 1} - {data.displayName}";
            data.UpdateAbilityCounter(this);
            UpdateButtonGraphic(data);
        }

        private void SetEmptySlotData()
        {
            _abilityName.text = $"{slotIndex + 1} - Empty";
            HideIcon();
            HideAbilityCounter();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnSlotClick?.Invoke(slotIndex);
        }
    }
}