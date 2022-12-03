using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Items.Actions
{
    public class Destroy : ItemActionsFactory
    {
        Inventory _inventory;

        public Destroy(Inventory inventory)
        {
            _inventory = inventory;
        }

        protected override IRadialMenuAction CreateAction(IItemSlot itemSlot)
        {
            return new DestroyAction(itemSlot, _inventory);
        }

        protected override bool SlotIsValid(IItemSlot itemSlot)
        {
            return (itemSlot.itemSlotContainer == ItemSlotContainers.inventory ||
                itemSlot.itemSlotContainer == ItemSlotContainers.storage) &&
                itemSlot.itemSlotData.item is IDestroyable;
        }

        class DestroyAction : IItemAction
        {
            public string actionTitle => "Destroy";
            Inventory _inventory;
            IItemSlot _itemSlot;
            IDestroyable _item;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.bottomLeft;

            public DestroyAction(IItemSlot itemSlot, Inventory inventory)
            {
                _itemSlot = itemSlot;
                _inventory = inventory;
                _item = itemSlot?.itemSlotData?.item as IDestroyable;
            }

            public void DoAction()
            {
                var itemList = new List<ItemSlotData>();
                _item?.AddItemComponentsTo(ref itemList);
                _inventory.AddItems(itemList);
            }
        }
    }
}