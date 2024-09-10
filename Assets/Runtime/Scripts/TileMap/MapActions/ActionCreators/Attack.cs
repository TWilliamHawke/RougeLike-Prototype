using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Actions
{
    class Attack : IMapActionCreator
    {
        IActionScreenController _actionScreenController;

        public Attack(IActionScreenController actionScreenController)
        {
            _actionScreenController = actionScreenController;
        }

        public IMapAction CreateActionLogic(MapActionTemplate template, IMapActionLocation actionTarget)
        {
            return new AttackAction(template, actionTarget, _actionScreenController);
        }

        public class AttackAction : IMapAction
        {
            IAttackActionData _template;
            IMapActionLocation _target;

            public bool isEnable { get; } = true;

            public Sprite icon => _template.icon;
            public string actionTitle => _template.displayName;

            public bool isHidden => false;
            IActionScreenController _actionScreenController;

            public AttackAction(IAttackActionData action, IMapActionLocation target, IActionScreenController actionScreenController)
            {
                _template = action;
                _target = target;
                _actionScreenController = actionScreenController;
            }

            public void DoAction()
            {
                _target.ReplaceFactionForAll(_template.enemyFaction);
                _actionScreenController.CloseActionScreen();
            }

        }
    }
}

