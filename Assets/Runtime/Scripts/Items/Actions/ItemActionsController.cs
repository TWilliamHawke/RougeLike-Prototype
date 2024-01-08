using System.Collections;
using Core.UI;
using Effects;
using Entities.PlayerScripts;
using Items.UI;
using UnityEngine;

namespace Items.Actions
{

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

        //event handler in editor
        public override void CreateFactory()
        {
            _itemActionsFactory.Add(new Use(_player.GetComponent<AbilityController>()));
            _itemActionsFactory.Add(new Buy());
            _itemActionsFactory.Add(new Sell());
            _itemActionsFactory.Add(new Equip());
            _itemActionsFactory.Add(new MoveToStorage());
            _itemActionsFactory.Add(new Destroy(_inventory, _modalWindowController));
            _itemActionsFactory.Add(new Drop());
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


