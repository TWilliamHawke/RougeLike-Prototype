using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Objects
{
    class Attack : IMapActionCreator
    {
        public IMapAction CreateActionLogic(MapActionTemplate template)
        {
            return new AttackAction(template);
        }

        public class AttackAction: IMapAction
    {
        public event UnityAction<IMapAction> OnCompletion;
        public bool isEnable { get; set; } = true;

        MapActionTemplate _template;
        public Sprite icon => _template.icon;
        public string actionTitle => _template.displayName;

        public AttackAction(MapActionTemplate action)
        {
            _template = action;
        }

        public void DoAction()
        {
            OnCompletion?.Invoke(this);
        }

    }
    }
}

