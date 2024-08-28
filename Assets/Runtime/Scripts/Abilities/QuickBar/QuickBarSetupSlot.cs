using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Abilities
{
    public class QuickBarSetupSlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] Image _abilityIcon;
        [SerializeField] TextMeshProUGUI _abilityName;
        [SerializeField] Image _abilityCountBg;
        [SerializeField] TextMeshProUGUI _abilityCount;

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
            _abilityIcon.Show();
            _abilityIcon.sprite = data.abilityIcon;
            _abilityName.text = $"{slotIndex + 1} - {data.displayName}";

            if (data.numOfUses > -1)
            {
                _abilityCountBg.gameObject.SetActive(true);
                _abilityCount.text = data.numOfUses.ToString();
            }
            else
            {
                _abilityCountBg.gameObject.SetActive(false);
            }
        }

        private void SetEmptySlotData()
        {
            _abilityIcon.Hide();
            _abilityName.text = $"{slotIndex + 1} - Empty";
            _abilityCountBg.gameObject.SetActive(false);
            _abilityCount.text = "";
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnSlotClick?.Invoke(slotIndex);
        }
    }
}