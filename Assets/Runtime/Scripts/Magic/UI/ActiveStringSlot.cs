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
        [SerializeField] DragController _dragController;
        [Header("UI Elements")]
        [SerializeField] Image _icon;
        [SerializeField] Image _dragMask;

        SpellString _activeString;
        int _slotIndex = 0;
        KnownSpellData _spellData;

        public void Init(int slotIndex)
        {
            _dragController.OnBeginDrag += TryEnableDragMask;
            _dragController.OnEndDrag += DisableDragMask;
            _slotIndex = slotIndex;
        }

        void OnDestroy()
        {
            _dragController.OnBeginDrag -= TryEnableDragMask;
            _dragController.OnEndDrag -= DisableDragMask;
        }

        public bool DataIsMeet(ItemSlotData data)
        {
            return data != null && data.item is SpellString && _activeString is null;
        }

        public void SetData(KnownSpellData data)
        {
            _spellData = data;
            _activeString = data.activeStrings[_slotIndex];
            _icon.sprite = _activeString?.icon ?? _defaultIcon;
        }

        public void DropData(ItemSlotData data)
        {
            _icon.sprite = data.item.icon;
            _activeString = data.item as SpellString;
            _inventory.spellStrings.RemoveItemFromSlot(data);
            _spellData.SetActiveString(_slotIndex, _activeString);
        }

        void TryEnableDragMask(object data)
        {
            if (DataIsMeet(data as ItemSlotData))
            {
                _dragMask.gameObject.SetActive(true);
            }
        }

        void DisableDragMask()
        {
            _dragMask.gameObject.SetActive(false);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != MouseButton.Right || _activeString is null) return;

            if(_inventory.resources.TrySpendResource(ResourceType.magicDust, 100))
            {
                _inventory.spellStrings.AddItem(_activeString);
                _icon.sprite = _defaultIcon;
                _activeString = null;
                _spellData.SetActiveString(_slotIndex, _activeString);
            }

        }
    }
}