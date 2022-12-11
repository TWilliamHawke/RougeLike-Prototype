using UnityEngine;
using UnityEngine.Events;

namespace Map.Objects
{
    public class EmptyAction : IMapAction
    {
        public bool isEnable { get; set; }

        MapActionTemplate _template;
        public Sprite icon => _template.icon;
        public string actionTitle => _template.displayName;

        public event UnityAction<IMapAction> OnCompletion;

        public EmptyAction(MapActionTemplate template)
        {
            _template = template;
        }

        public void DoAction()
        {
            OnCompletion?.Invoke(this);
        }
    }
}

