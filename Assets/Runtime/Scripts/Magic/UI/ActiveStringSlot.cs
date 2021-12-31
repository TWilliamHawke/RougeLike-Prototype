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

        public void Init()
        {
            _dragController.OnBeginDrag += TryEnableDragMask;
            _dragController.OnEndDrag += DisableDragMask;
        }

        void OnDestroy()
        {
            _dragController.OnBeginDrag -= TryEnableDragMask;
            _dragController.OnEndDrag -= DisableDragMask;
        }

        public bool DataIsMeet(ItemSlotData data)
        {
            return data != null && data.item as SpellString && _activeString == null;
        }

        public void SetData(SpellString data)
        {
            _icon.sprite = data?.icon ?? _defaultIcon;
        }

        public void DropData(ItemSlotData data)
        {
            _icon.sprite = data.item.icon;
            _activeString = data.item as SpellString;
            _inventory.spellStrings.RemoveItemFromSlot(data);
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
            if (eventData.button != MouseButton.Right || _activeString == null) return;

            _inventory.spellStrings.AddItem(_activeString);
            _icon.sprite = _defaultIcon;
            _activeString = null;
        }
    }
}