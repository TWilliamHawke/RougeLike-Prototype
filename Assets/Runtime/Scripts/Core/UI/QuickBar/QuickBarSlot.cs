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
		IAbilityInstruction _abilityInSlot;

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
			_abilityInSlot = (data.item as IAbilitySource)?.CreateAbilityInstruction();
			if(_abilityInSlot is null) return;

            _activeAbilities[_slotIndex] = _abilityInSlot;
			UpdateSlotGraphic();
        }

        public void UpdateState()
        {
            //updata cooldown, active on/off
        }

		void UpdateSlotGraphic()
		{
			if(_abilityInSlot is null)
			{
				_actionIcon.gameObject.SetActive(false);
			}
			else
			{
				_actionIcon.gameObject.SetActive(true);
				_actionIcon.sprite = _abilityInSlot.abilityIcon;
			}
		}

        public void OnPointerClick(PointerEventData eventData)
        {
            UseAbility();
        }

		void UseAbility()
		{
			if(_abilityInSlot is null) return;
            
			_activeAbilities.UseAbility(_slotIndex);
		}

    }
}