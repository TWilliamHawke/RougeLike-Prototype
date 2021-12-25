using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.UI;
using UI.DragAndDrop;

namespace Magic.UI
{
    public class ActiveStringSlot : MonoBehaviour, IDropTarget<ItemSlotData>
    {
        [SerializeField] Image _icon;
        [SerializeField] Sprite _defaultIcon;
        [SerializeField] Inventory _inventory;

        SpellString _activeString;

        public bool DataIsMeet(ItemSlotData data)
        {
            return data != null && data.item is SpellString;
        }

        public void SetData(SpellString data)
        {
            _icon.sprite = data?.icon ?? _defaultIcon;
        }

        public void SetDragableData(ItemSlotData data)
        {
            _icon.sprite = data.item.icon;

            if(_activeString)
            {
                _inventory.spellStrings.AddItem(_activeString);
            }

            _activeString = data.item as SpellString;
            _inventory.spellStrings.RemoveItemFromSlot(data);

        }
    }
}