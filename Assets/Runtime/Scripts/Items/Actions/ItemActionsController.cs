using System.Collections;
using System.Collections.Generic;
using Abilities;
using Core;
using Core.UI;
using Entities.PlayerScripts;
using Items.UI;
using UnityEngine;


namespace Items.Actions
{
    public class ItemActionsController : ActionController<ItemSlotData>, IObserver<ItemSlot>
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] InventoryScreen _inventoryScreen;
        [SerializeField] QuickBarSetupController _quickBarSetupController;
        [Header("Item Actions")]
        [SerializeField] ContextActionTemplate _useAbility;
        [SerializeField] ContextActionTemplate _buy;
        [SerializeField] ContextActionTemplate _sell;
        [SerializeField] ContextActionTemplate _equip;
        [SerializeField] ContextActionTemplate _moveToStorage;
        [SerializeField] ContextActionTemplate _bindToQuickbar;
        [SerializeField] ContextActionTemplate _destroy;
        [SerializeField] ContextActionTemplate _drop;

        [InjectField] Player _player;
        [InjectField] ModalWindowController _modalWindowController;

        void Start()
        {
            _inventoryScreen.AddSlotObservers(this);
        }

        protected override void FillFactory()
        {
            var abilityController = _player.GetComponent<AbilityController>();
            var abilitiesFactory = _player.GetComponent<PlayerAbilitiesFactory>();

            AddFactory(_useAbility, new UseAbility(abilitiesFactory, abilityController));
            AddFactory(_buy, new Buy());
            AddFactory(_sell, new Sell());
            AddFactory(_equip, new Equip());
            AddFactory(_moveToStorage, new MoveToStorage());
            AddFactory(_bindToQuickbar, new BindToQuickbar<ItemSlotData>(
                abilitiesFactory, _quickBarSetupController));
            AddFactory(_destroy, new Destroy(_inventory, _modalWindowController));
            AddFactory(_drop, new Drop());
        }

        public void AddToObserve(ItemSlot target)
        {
            target.OnClick += FillContextMenu;
        }

        public void RemoveFromObserve(ItemSlot target)
        {
            target.OnClick -= FillContextMenu;
        }

        private void FillContextMenu(ItemSlotData actionSource)
        {
            FillContextMenu(actionSource, actionSource);
        }
    }
}


