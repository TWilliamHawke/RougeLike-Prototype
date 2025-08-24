using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{

    public abstract class ActionController<T> : MonoBehaviour
    {
        [InjectField] IContextMenu _contextMenu;

        List<ContextActionContainer> _contextActions = new();
        Dictionary<ContextActionTemplate, IActionFactory<T>> _factories = new();

        protected abstract void FillFactory();

        //event handler in editor
        public void CreateFactory()
        {
            FillFactory();
        }

        protected void AddFactory(ContextActionTemplate template, IActionFactory<T> factory)
        {
            _factories.Add(template, factory);
        }

        public void FillContextMenu(T target, IEnumerable<ContextActionTemplate> actions)
        {
            _contextActions.Clear();

            foreach (var template in actions)
            {
                if (_factories.TryGetValue(template, out var factory))
                {
                    if (factory.TryCreateAction(target, out var action))
                    {
                        action.SetActionTemplate(template);
                        _contextActions.Add(action);
                    }
                }
            }

            _contextMenu.Fill(_contextActions);
        }
    }
}


