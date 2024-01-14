using System.Collections.Generic;
using Core;
using Core.UI;
using UnityEngine;

namespace Items.Actions
{
    public class Destroy : RadialActionFactory<ItemSlotData>
    {
        Inventory _inventory;
        ModalWindowController _modalWindow;
        ItemSection<Item> _itemsList = new(ItemStorageType.none);

        public Destroy(Inventory inventory, ModalWindowController modalWindow)
        {
            _inventory = inventory;
            _modalWindow = modalWindow;
        }

        protected override IRadialMenuAction CreateAction(ItemSlotData itemSlot)
        {
            _itemsList.Clear();
            var item = itemSlot.item as IDestroyable;
            item?.resourcesData?.FillItemSection(ref _itemsList);

            var modalWindowData = new ModalWindowData
            {
                title = "Destoy Item",
                mainText = "You will receive resources:",
                action = new ConfirmDestroy(itemSlot, _inventory, _itemsList),
                resourcesData = _itemsList
            };

            return new OpenDestroyWindow(_modalWindow, modalWindowData);
        }

        protected override bool ElementIsValid(ItemSlotData itemSlot)
        {
            return (itemSlot.slotContainer == ItemStorageType.inventory ||
                itemSlot.slotContainer == ItemStorageType.storage) &&
                itemSlot.item is IDestroyable;
        }

        class OpenDestroyWindow : IRadialMenuAction
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

        class ConfirmDestroy : IRadialMenuAction
        {
            public string actionTitle => "Confirm";
            Inventory _inventory;
            ItemSlotData _itemSlot;
            ItemSection<Item> _items;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.bottomLeft;

            public ConfirmDestroy(ItemSlotData itemSlot, Inventory inventory, ItemSection<Item> itemsList)
            {
                _itemSlot = itemSlot;
                _inventory = inventory;
                _items = itemsList;
            }

            public void DoAction()
            {
                _inventory.AddItems(_items);
                _itemSlot.RemoveOneItem();
            }
        }
    }
}