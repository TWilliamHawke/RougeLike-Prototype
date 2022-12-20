using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Actions
{
    class Talk : INpcActionCreator
    {
        public IMapAction CreateActionLogic(MapActionTemplate template, INpcActionTarget target)
        {
            return new TalkAction(template);
        }

        public class TalkAction : IMapAction
        {
            IIconData _template;

            public bool isEnable { get; } = true;
            public Sprite icon => _template.icon;
            public string actionTitle => _template.displayName;

            public bool isHidden => false;

            public TalkAction(IIconData template)
            {
                _template = template;
            }

            public void DoAction()
            {

            }

        }
    }
}

