using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Objects
{
    public class Rob : INpcActionCreator
    {
        public IMapAction CreateActionLogic(MapActionTemplate template, INpcActionTarget actionTarget)
        {
            return new RobAction(template);
        }

        class RobAction : IMapAction
        {
            public bool isEnable => true;
            public bool isHidden => false;
            public Sprite icon => _template.icon;
            public string actionTitle => _template.displayName;

			IIconData _template;

            public RobAction(IIconData template)
            {
                _template = template;
            }
        }
    }
}


