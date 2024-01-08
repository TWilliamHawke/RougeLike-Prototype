using System.Collections.Generic;
using Core.UI;
using UnityEngine;

namespace Items.Actions
{
    public abstract class ActionController<T> : MonoBehaviour
    {
        [InjectField] IContextMenu _contextMenu;

        protected List<IActionFactory<T>> _itemActionsFactory = new();
        List<IContextAction> _itemActions = new();

        public abstract void CreateFactory();


        public void FillContextMenu(T itemSlot)
        {
            _itemActions.Clear();

            foreach (var factory in _itemActionsFactory)
            {
                if (factory.TryCreateAction(itemSlot, out var action))
                {
                    _itemActions.Add(action);
                }
            }

            _contextMenu.Fill(_itemActions);
        }
    }
}


