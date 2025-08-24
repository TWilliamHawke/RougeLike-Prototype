using System.Collections.Generic;
using Core;
using Core.UI;
using UnityEngine;

namespace Items.Actions
{
    public class Destroy : ContextActionFactory<ItemSlotData>
    {
        Inventory _inventory;
        ModalWindowController _modalWindow;
        ItemSection _itemsList = new();

        public Destroy(Inventory inventory, ModalWindowController modalWindow)
        {
            _inventory = inventory;
            _modalWindow = modalWindow;
        }

        protected override ContextActionContainer CreateAction(ItemSlotData itemSlot)
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
            return itemSlot.item is IDestroyable;
        }

        class OpenDestroyWindow : ContextActionContainer
        {
            ModalWindowController _modalWindowController;
            ModalWindowData _modalWindowData;

            public OpenDestroyWindow(ModalWindowController modalWindowController, ModalWindowData modalWindowData)
            {
                _modalWindowController = modalWindowController;
                _modalWindowData = modalWindowData;
            }

            public override void DoAction()
            {
                _modalWindowController.OpenWindow(_modalWindowData);
            }
        }

        class ConfirmDestroy : IContextAction
        {
            public string actionTitle => "Confirm";
            Inventory _inventory;
            ItemSlotData _itemSlot;
            ItemSection _items;

            public int preferedPosition => 7;


            public ConfirmDestroy(ItemSlotData itemSlot, Inventory inventory, ItemSection itemsList)
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