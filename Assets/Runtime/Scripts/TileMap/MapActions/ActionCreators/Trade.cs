using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Actions
{
    public class Trade : IMapActionCreator
    {
        public IMapAction CreateActionLogic(MapActionTemplate template, IMapActionLocation actionTarget)
        {
            return new TradeAction(template);
        }

        class TradeAction : IMapAction
        {
            public bool isEnable => true;
            public bool isHidden => false;
            public Sprite icon => _template.icon;
            public string actionTitle => _template.displayName;

			IIconData _template;

            public TradeAction(IIconData template)
            {
                _template = template;
            }

            public void DoAction()
            {
                Debug.Log("Trade");
            }
        }
    }
}


