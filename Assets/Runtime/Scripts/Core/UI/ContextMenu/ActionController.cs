using System.Collections.Generic;
using Core;
using Core.UI;
using UnityEngine;

namespace Core.UI
{
    public abstract class ActionController<T> : MonoBehaviour
    {
        [InjectField] IContextMenu _contextMenu;

        protected List<IActionFactory<T>> _factory = new();
        List<IContextAction> _contextActions = new();

        protected abstract void FillFactory(List<IActionFactory<T>> factory);

        //event handler in editor
        public void CreateFactory()
        {
            FillFactory(_factory);
        }

        public void FillContextMenu(T actionSource)
        {
            _contextActions.Clear();

            foreach (var factory in _factory)
            {
                if (factory.TryCreateAction(actionSource, out var action))
                {
                    _contextActions.Add(action);
                }
            }

            _contextMenu.Fill(_contextActions);
        }
    }
}


