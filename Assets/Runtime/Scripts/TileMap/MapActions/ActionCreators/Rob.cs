using System.Collections;
using System.Collections.Generic;
using Entities.NPC;
using Items.UI;
using UnityEngine;

namespace Map.Actions
{
    public class Rob : INpcActionCreator
    {
        IStealingController _controller;

        public Rob(IStealingController controller)
        {
            _controller = controller;
        }

        public IMapAction CreateActionLogic(MapActionTemplate template, INpcActionTarget actionTarget)
        {
            return new RobAction(template, _controller, actionTarget.inventory);
        }

        class RobAction : IMapAction
        {
            public bool isEnable => true;
            public bool isHidden => false;
            public Sprite icon => _template.icon;
            public string actionTitle => _template.displayName;

            readonly IIconData _template;
            readonly IStealingController _controller;
            readonly INPCInventory _inventory;

            public RobAction(IIconData template, IStealingController controller, INPCInventory inventory)
            {
                _template = template;
                _controller = controller;
                _inventory = inventory;
            }

            public void DoAction()
            {
                _controller.OpenScreen(_inventory);
            }
        }
    }
}


