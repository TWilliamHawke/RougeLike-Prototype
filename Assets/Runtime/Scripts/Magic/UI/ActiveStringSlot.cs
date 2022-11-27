using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.UI;
using UI.DragAndDrop;
using UnityEngine.EventSystems;
using MouseButton = UnityEngine.EventSystems.PointerEventData.InputButton;

namespace Magic.UI
{
    public class ActiveStringSlot : MonoBehaviour, IDropTarget<ItemSlotData>, IPointerClickHandler
    {
        [SerializeField] Sprite _defaultIcon;
        [SerializeField] Inventory _inventory;
        [Header("UI Elements")]
        [SerializeField] Image _icon;
        [SerializeField] Image _dragMask;

        const int RESOURCE_COST_TO_CLEAR = 100;

        SpellString _activeLine;
        int _slotIndex = 0;
        KnownSpellData _spellData;

        public void Init(int slotIndex)
        {
            _slotIndex = slotIndex;
        }

        public bool DataIsMeet(ItemSlotData data)
        {
            return data != null && data.item is SpellString && _activeLine is null;
        }

        public void SetData(KnownSpellData data)
        {
            _spellData = data;
            _activeLine = data.activeStrings[_slotIndex];
            _icon.sprite = _activeLine?.icon ?? _defaultIcon;
        }

        public void DropData(ItemSlotData data)
        {
            _icon.sprite = data.item.icon;
            _activeLine = data.item as SpellString;
            _inventory.spellStrings.RemoveItemFromSlot(data);
            _spellData.SetActiveString(_slotIndex, _activeLine);
        }

        //used as unityevent
        public void ShowDragMask()
        {
            if (_activeLine is not null) return;
            _dragMask.gameObject.SetActive(true);
        }

        //used as unityEvent
        public void HideDragMask()
        {
            _dragMask.gameObject.SetActive(false);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != MouseButton.Right || _activeLine is null) return;

            if(_inventory.resources.TrySpendResource(ResourceType.magicDust, RESOURCE_COST_TO_CLEAR))
            {
                _inventory.spellStrings.AddItem(_activeLine);
                _icon.sprite = _defaultIcon;
                _activeLine = null;
                _spellData.SetActiveString(_slotIndex, _activeLine);
            }

        }
    }
}