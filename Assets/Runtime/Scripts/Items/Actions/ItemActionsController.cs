using System.Collections;
using System.Collections.Generic;
using Core;
using Core.UI;
using Effects;
using Entities.PlayerScripts;
using UnityEngine;

namespace Items.Actions
{
    public class ItemActionsController : MonoBehaviour
    {
        [SerializeField] Injector _selfIjector;
        [SerializeField] Inventory _inventory;

        [InjectField] IContextMenu _contextMenu;
        [InjectField] Player _player;
        [InjectField] ModalWindowController _modalWindowController;

        List<IItemActionFactory> _itemActionsFactory = new();
        List<IContextAction> _itemActions = new();

        private void Awake()
        {
            _selfIjector.SetDependency(this);
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

        public void FillContextMenu(ItemSlot itemSlot)
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


