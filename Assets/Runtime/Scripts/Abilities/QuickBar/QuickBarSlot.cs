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
using UnityEngine.Events;

namespace Abilities
{
    public class QuickBarSlot : MonoBehaviour, IDropTarget<ItemSlotData>, IDropTarget<KnownSpellData>, IPointerClickHandler
    {
        [Header("UI Elements")]
        [SerializeField] TextMeshProUGUI _slotNumber;
        [SerializeField] Image _actionIcon;

        public event UnityAction<int, ItemSlotData> OnItemDrop;
        public event UnityAction<int, KnownSpellData> OnSpellDrop;
        public event UnityAction<int> OnAbilityUsed;

        int _slotIndex = -1;

        public bool checkImageAlpha => false;

        public void SetSlotNumber(int slotIndex)
        {
            _slotIndex = slotIndex;
            _slotNumber.text = ((slotIndex + 1) % 10).ToString();
        }

        public bool DataIsMeet(ItemSlotData data)
        {
            return data?.item is IAbilitySource;
        }

        public void DropData(ItemSlotData data)
        {
            OnItemDrop?.Invoke(_slotIndex, data);
        }

        public void UpdateState()
        {
            //updata cooldown, active on/off
        }

        public void ClearSlot()
        {
            _actionIcon.gameObject.SetActive(false);
        }

        public void UpdateSlotGraphic(IAbilityContainerData data)
        {
            _actionIcon.gameObject.SetActive(true);
            _actionIcon.sprite = data.abilityIcon;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnAbilityUsed?.Invoke(_slotIndex);
        }

        void IDropTarget<KnownSpellData>.DropData(KnownSpellData data)
        {
            OnSpellDrop?.Invoke(_slotIndex, data);
        }

        bool IDropTarget<KnownSpellData>.DataIsMeet(KnownSpellData data)
        {
            return true;
        }
    }
}