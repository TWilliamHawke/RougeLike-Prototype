using System.Collections.Generic;
using Core;
using Core.UI;
using UnityEngine;

namespace Items.Actions
{
    public class Destroy : ItemActionsFactory
    {
        Inventory _inventory;
        ModalWindowController _modalWindow;
        ItemSection<Item> _items = new();

        public Destroy(Inventory inventory, ModalWindowController modalWindow)
        {
            _inventory = inventory;
            _modalWindow = modalWindow;
        }

        protected override IRadialMenuAction CreateAction(IItemSlot itemSlot)
        {
            _items.Clear();
            var item = itemSlot.itemSlotData.item as IDestroyable;
            item?.resourcesData?.FillItemSection(ref _items);

            var modalWindowData = new ModalWindowData
            {
                title = "Destoy Item",
                mainText = "You receive resources:",
                action = new ConfirmDestroy(itemSlot, _inventory, _items),
                resourcesData = _items
            };

            return new OpenDestroyWindow(_modalWindow, modalWindowData);
        }

        protected override bool SlotIsValid(IItemSlot itemSlot)
        {
            return (itemSlot.itemSlotContainer == ItemSlotContainers.inventory ||
                itemSlot.itemSlotContainer == ItemSlotContainers.storage) &&
                itemSlot.itemSlotData.item is IDestroyable;
        }

        class OpenDestroyWindow : IItemAction
        {
            public string actionTitle => "Destroy";
            public RadialButtonPosition preferedPosition => RadialButtonPosition.bottomLeft;

            ModalWindowController _modalWindowController;
            ModalWindowData _modalWindowData;

            public OpenDestroyWindow(ModalWindowController modalWindowController, ModalWindowData modalWindowData)
            {
                _modalWindowController = modalWindowController;
                _modalWindowData = modalWindowData;
            }

            public void DoAction()
            {
                _modalWindowController.OpenWindow(_modalWindowData);
            }
        }

        class ConfirmDestroy : IItemAction
        {
            public string actionTitle => "Confirm";
            Inventory _inventory;
            IItemSlot _itemSlot;
            ItemSection<Item> _items;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.bottomLeft;

            public ConfirmDestroy(IItemSlot itemSlot, Inventory inventory, ItemSection<Item> items)
            {
                _itemSlot = itemSlot;
                _inventory = inventory;
                _items = items;
            }

            public void DoAction()
            {
                _inventory.AddItems(_items);
                _itemSlot.itemSlotData.RemoveFromStack();
            }
        }
    }
}