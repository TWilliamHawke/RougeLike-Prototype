using System.Collections;
using System.Collections.Generic;
using Entities.NPC;
using Items;
using Items.UI;
using UnityEngine;

namespace Map.Actions
{
    public class Rob : IMapActionCreator
    {
        IStealingController _controller;

        public Rob(IStealingController controller)
        {
            _controller = controller;
        }

        public IMapAction CreateActionLogic(MapActionTemplate template, IMapActionLocation actionTarget)
        {
            return new RobAction(template, _controller, actionTarget.inventory);
        }

        class RobAction : IMapAction
        {
            public bool isEnable => !_inventory.IsEmpty() && _inventory.isStealingTarget;
            public bool isHidden => false;
            public Sprite icon => _template.icon;
            public string actionTitle => _template.displayName;

            readonly IIconData _template;
            readonly IStealingController _controller;
            readonly IInteractiveStorage _inventory;

            public RobAction(IIconData template, IStealingController controller, IInteractiveStorage inventory)
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


