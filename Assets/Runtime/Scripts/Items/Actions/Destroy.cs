using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Items.Actions
{
    public class Destroy : IItemAction
    {
        public string actionTitle => "Destroy";
        Inventory _inventory;
        public IItemSlot itemSlot { get; set; }
        IDestroyable _item;

        public RadialButtonPosition preferedPosition => RadialButtonPosition.bottomLeft;

        public Destroy(IItemSlot itemSlot, Inventory inventory)
        {
            this.itemSlot = itemSlot;
            _inventory = inventory;
            _item = itemSlot?.itemSlotData?.item as IDestroyable;
        }

        public Destroy(Inventory inventory)
        {
            _inventory = inventory;
        }

        public void DoAction()
        {
            var itemList = new List<ItemSlotData>();
            _item?.AddItemComponentsTo(ref itemList);
            _inventory.AddItems(itemList);
        }

        public bool SlotIsValid(IItemSlot itemSlot)
        {
            return (itemSlot.itemSlotContainer == ItemSlotContainers.inventory ||
                itemSlot.itemSlotContainer == ItemSlotContainers.storage) &&
                itemSlot.itemSlotData.item is IDestroyable;
        }
    }
}