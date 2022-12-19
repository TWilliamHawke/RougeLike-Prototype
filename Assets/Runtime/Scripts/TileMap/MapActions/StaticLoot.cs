using System.Collections;
using System.Collections.Generic;
using Items;
using Items.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Objects
{
    public class StaticLoot
    {
        LootPanel _lootPanel;
        IActionScreenController _actionScreenController;

        public StaticLoot(LootPanel lootPanel, IActionScreenController actionScreenController)
        {
            _lootPanel = lootPanel;
            _actionScreenController = actionScreenController;
        }

        public IMapAction CreateActionLogic(MapActionTemplate template, ILootStorage loot)
        {
            return new LootAction(template, loot, _lootPanel, _actionScreenController);
        }

        class LootAction : IMapAction
        {
            LootPanel _lootPanel;
            ILootActionData _template;
            ILootStorage _loot;

            public Sprite icon => _template.icon;
            public string actionTitle => _template.displayName;
            public bool isEnable => !_loot.isEmpty;
            public bool isHidden => false;

            IActionScreenController _actionScreenController;

            public LootAction(ILootActionData template, ILootStorage loot, LootPanel lootPanel, IActionScreenController actionScreenController)
            {
                _template = template;
                _loot = loot;
                _lootPanel = lootPanel;
                _actionScreenController = actionScreenController;
            }

            public void DoAction()
            {
                _lootPanel.Open(_loot);
                _actionScreenController.CloseActionScreen();
                _lootPanel.OnTakeAll += CompleteAction;
                _lootPanel.OnClose += ClearEvents;
            }

            void CompleteAction()
            {
                _loot.Clear();
                ClearEvents();
            }

            void ClearEvents()
            {
                _actionScreenController.OpenActionScreen();
                _lootPanel.OnTakeAll -= CompleteAction;
                _lootPanel.OnClose -= ClearEvents;
            }

        }
    }
}


