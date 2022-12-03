using System.Collections;
using System.Collections.Generic;
using Core;
using Core.UI;
using UnityEngine;

namespace Items.Actions
{
    public class ItemActionsController : MonoBehaviour
    {
        [SerializeField] Injector _selfIjector;
        [SerializeField] Inventory _inventory;

        [InjectField] IContextMenu _contextMenu;

        List<IItemActionFactory> _itemActionsFactories = new();
        List<IContextAction> _itemActions = new();

        private void Awake()
        {
            _selfIjector.SetDependency(this);

            _itemActionsFactories.Add(new Use());
            _itemActionsFactories.Add(new Buy());
            _itemActionsFactories.Add(new Sell());
            _itemActionsFactories.Add(new Equip());
            _itemActionsFactories.Add(new MoveToStorage());
            _itemActionsFactories.Add(new Destroy(_inventory));
            _itemActionsFactories.Add(new Drop());

        }

        public void FillContextMenu(ItemSlot itemSlot)
        {
            _itemActions.Clear();

            foreach (var factory in _itemActionsFactories)
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


