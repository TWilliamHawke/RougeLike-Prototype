using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Items.UI
{
    public class InventorySection : UISection<ItemSlotData, ItemSlot>
    {
        [SerializeField] UISectionHeader _sectionHeader;
        [SerializeField] ItemSlotsLayout _itemSlotList;

        protected override UISectionHeader _header => _sectionHeader;
        protected override UILayoutWithObserver<ItemSlotData, ItemSlot> _layout => _itemSlotList;
    }
}