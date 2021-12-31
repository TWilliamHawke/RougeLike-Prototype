using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Items;
using Effects;
using UI.DragAndDrop;
using Entities.Player;
using UnityEngine.EventSystems;

namespace Core.UI
{
    public class QuickBarSlot : MonoBehaviour, IDropTarget<ItemSlotData>, IPointerClickHandler
    {
		[SerializeField] ActiveAbilities _activeAbilities;
        [Header("UI Elements")]
        [SerializeField] TextMeshProUGUI _slotNumber;
        [SerializeField] Image _actionIcon;

        int _slotIndex;
		IAbilitySource _abilityInSlot;

        public void SetSlotNumber(int slotIndex)
        {
            _slotIndex = slotIndex;
            _slotNumber.text = ((slotIndex + 1) % 10).ToString();
			_abilityInSlot = _activeAbilities[slotIndex];
			UpdateSlotGraphic();
        }

        public bool DataIsMeet(ItemSlotData data)
        {
            return data?.item is IAbilitySource;
        }

        public void DropData(ItemSlotData data)
        {
			_abilityInSlot = data.item as IAbilitySource;
			if(_abilityInSlot == null) return;

            _activeAbilities[_slotIndex] = data.item as IAbilitySource;
			UpdateSlotGraphic();
        }

		void UpdateSlotGraphic()
		{
			if(_abilityInSlot == null)
			{
				_actionIcon.gameObject.SetActive(false);
			}
			else
			{
				_actionIcon.gameObject.SetActive(true);
				_actionIcon.sprite = _abilityInSlot.abilityIcon;
			}
		}

		void UseAbility()
		{
			if(_abilityInSlot == null) return;
			_activeAbilities.UseAbility(_slotIndex);
		}

        public void OnPointerClick(PointerEventData eventData)
        {
            UseAbility();
        }
    }
}