using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Objects
{
    class Talk : IMapActionCreator
    {
        public IMapAction CreateActionLogic(MapActionTemplate template)
        {
            return new TalkAction(template);
        }

        public class TalkAction : IMapAction
    {
        IIconData _template;

        public event UnityAction<IMapAction> OnCompletion;

        public bool isEnable { get; set; } = true;
        public Sprite icon => _template.icon;
        public string actionTitle => _template.displayName;


        public TalkAction(IIconData template)
        {
            _template = template;
        }

        public void DoAction()
        {
            OnCompletion?.Invoke(this);
        }

    }
    }
}

