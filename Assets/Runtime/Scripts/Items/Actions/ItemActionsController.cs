using System.Collections;
using System.Collections.Generic;
using Core;
using Core.UI;
using Effects;
using Entities.PlayerScripts;
using Items.UI;
using UnityEngine;


namespace Items.Actions
{
    using FactoryList = List<IActionFactory<ItemSlotData>>;

    public class ItemActionsController : ActionController<ItemSlotData>, IObserver<ItemSlot>
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] InventoryScreen _inventoryScreen;

        [InjectField] Player _player;
        [InjectField] ModalWindowController _modalWindowController;

        void Start()
        {
            _inventoryScreen.AddSlotObservers(this);
        }

        protected override void FillFactory(FactoryList factory)
        {
            factory.Add(new Use(_player.GetComponent<AbilityController>()));
            factory.Add(new Buy());
            factory.Add(new Sell());
            factory.Add(new Equip());
            factory.Add(new MoveToStorage());
            factory.Add(new Destroy(_inventory, _modalWindowController));
            factory.Add(new Drop());
        }

        public void AddToObserve(ItemSlot target)
        {
            target.OnDragStart += FillContextMenu;
        }

        public void RemoveFromObserve(ItemSlot target)
        {
            target.OnDragStart -= FillContextMenu;
        }


    }
}


