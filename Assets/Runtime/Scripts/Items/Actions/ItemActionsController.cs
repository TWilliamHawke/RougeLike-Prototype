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
    public class ItemActionsController : ActionController<ItemSlotData>, IObserver<InventorySection>
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] InventoryScreen _inventoryScreen;
        [SerializeField] PlayerEquipment _playerEquipment;
        [SerializeField] QuickBarSetupController _quickBarSetupController;
        [Header("Item Actions")]
        [SerializeField] ContextActionTemplate _useAbility;
        [SerializeField] ContextActionTemplate _showInfo;
        [SerializeField] ContextActionTemplate _use;
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
            _inventoryScreen.AddSectionObserver(this);
        }

        protected override void FillFactory()
        {
            var abilityController = _player.GetComponent<AbilityController>();
            var abilitiesFactory = _player.GetComponent<PlayerAbilitiesFactory>();

            AddFactory(_use, new Use(abilityController));
            AddFactory(_useAbility, new UseAbility(abilitiesFactory, abilityController));
            AddFactory(_showInfo, new ShowInfo<ItemSlotData>());
            AddFactory(_buy, new Buy());
            AddFactory(_sell, new Sell());
            AddFactory(_equip, new Equip(_playerEquipment, _player));
            AddFactory(_moveToStorage, new MoveToStorage());
            AddFactory(_bindToQuickbar, new BindToQuickbar(
                abilitiesFactory, _quickBarSetupController));
            AddFactory(_destroy, new Destroy(_inventory, _modalWindowController));
            AddFactory(_drop, new Drop());
        }

        public void AddToObserve(InventorySection target)
        {
            target.OnItemSlotClick += FillContextMenu;
        }

        public void RemoveFromObserve(InventorySection target)
        {
            target.OnItemSlotClick -= FillContextMenu;
        }

        private void FillContextMenu(ItemSlotData itemSlot, ItemSectionTemplate sectionTemplate)
        {
            FillContextMenu(itemSlot, itemSlot.GetActions(sectionTemplate));
        }
    }
}


