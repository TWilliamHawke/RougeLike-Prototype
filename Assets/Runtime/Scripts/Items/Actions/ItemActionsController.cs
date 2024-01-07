using System.Collections;
using System.Collections.Generic;
using Core.UI;
using Effects;
using Entities.PlayerScripts;
using Items.UI;
using UnityEngine;

namespace Items.Actions
{
    public class ItemActionsController : MonoBehaviour, IObserver<ItemSlot>
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] InventoryScreen _inventoryScreen;

        [InjectField] IContextMenu _contextMenu;
        [InjectField] Player _player;
        [InjectField] ModalWindowController _modalWindowController;

        List<IItemActionFactory> _itemActionsFactory = new();
        List<IContextAction> _itemActions = new();

        void Start()
        {
            _inventoryScreen.AddSlotObservers(this);
        }

        //event handler in editor
        public void CreateFactory()
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

        public void FillContextMenu(ItemSlotData itemSlot)
        {
            _itemActions.Clear();

            foreach (var factory in _itemActionsFactory)
            {
                if (factory.TryCreateItemAction(itemSlot, out var action))
                {
                    _itemActions.Add(action);
                }
            }

            _contextMenu.Fill(_itemActions);
        }

    }
}


