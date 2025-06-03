using System.Collections;
using System.Collections.Generic;
using Abilities;
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
        [SerializeField] QuickBarSetupController _quickBarSetupController;

        [InjectField] Player _player;
        [InjectField] ModalWindowController _modalWindowController;

        void Start()
        {
            _inventoryScreen.AddSlotObservers(this);
        }

        protected override void FillFactory(FactoryList factory)
        {
            var abilityController = _player.GetComponent<AbilityController>();
            var abilitiesFactory = _player.GetComponent<PlayerAbilitiesFactory>();

            factory.Add(new Use(abilityController));
            factory.Add(new UseAbility(abilitiesFactory, abilityController));
            factory.Add(new Buy());
            factory.Add(new Sell());
            factory.Add(new Equip());
            factory.Add(new MoveToStorage());
            factory.Add(new BindToQuickbar<ItemSlotData>(
                abilitiesFactory, _quickBarSetupController));
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


