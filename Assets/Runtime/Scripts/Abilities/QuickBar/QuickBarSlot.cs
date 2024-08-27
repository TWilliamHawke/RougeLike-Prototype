using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Items;
using Effects;
using UI.DragAndDrop;
using Entities.PlayerScripts;
using UnityEngine.EventSystems;
using Magic;
using Abilities;

namespace Core.UI
{
    public class QuickBarSlot : MonoBehaviour, IDropTarget<ItemSlotData>, IDropTarget<KnownSpellData>, IPointerClickHandler
    {
		[SerializeField] ActiveAbilities _activeAbilities;
        [Header("UI Elements")]
        [SerializeField] TextMeshProUGUI _slotNumber;
        [SerializeField] Image _actionIcon;

        int _slotIndex;
		IAbilityInstruction _abilityInSlot;

        public bool checkImageAlpha => false;

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
			FillSlot(data.item as IAbilitySource);
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

        void FillSlot(IAbilitySource abilitySource)
        {
            if(abilitySource is null) return;
            _abilityInSlot = abilitySource.CreateAbilityInstruction();

            _activeAbilities[_slotIndex] = _abilityInSlot;
			UpdateSlotGraphic();
        }

		void UseAbility()
		{
			if(_abilityInSlot is null) return;
            
			_activeAbilities.UseAbility(_slotIndex);
		}

        void IDropTarget<KnownSpellData>.DropData(KnownSpellData data)
        {
            FillSlot(data as IAbilitySource);
        }

        bool IDropTarget<KnownSpellData>.DataIsMeet(KnownSpellData data)
        {
            return true;
        }
    }
}