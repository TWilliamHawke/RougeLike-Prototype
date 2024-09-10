using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Items.UI;
using UnityEngine.Events;

namespace Map.Actions
{
    class Loot : IMapActionCreator
    {
        LootPanel _lootPanel;
        IActionScreenController _actionScreenController;

        public Loot(LootPanel lootPanel, IActionScreenController actionScreenController)
        {
            _lootPanel = lootPanel;
            _actionScreenController = actionScreenController;
        }

        public IMapAction CreateLootAction(MapActionTemplate template, IContainersList loot)
        {
            return new LootAction(template, loot, _lootPanel, _actionScreenController);
        }

        public IMapAction CreateActionLogic(MapActionTemplate template, IMapActionLocation store)
        {
            if (template is ILootActionData lootTemplate && lootTemplate.lootTable != null)
            {
                LootContainer lootContainer = new();
                lootContainer.AddItemsFrom(lootTemplate.lootTable);
                return new LootAction(template, lootContainer, _lootPanel, _actionScreenController);
            }

            throw new System.Exception($"{template.name} labeled as loot but loot table is not assigned");
        }

        class LootAction : IMapAction
        {
            LootPanel _lootPanel;
            ILootActionData _template;
            public Sprite icon => _template.icon;
            public string actionTitle => _template.displayName;

            IContainersList _loot;
            IActionScreenController _actionScreenController;
            public bool isEnable => !_loot.IsEmpty();
            public bool isHidden => false;

            public LootAction(ILootActionData template, IContainersList loot, LootPanel lootPanel, IActionScreenController actionScreenController)
            {
                _template = template;
                _lootPanel = lootPanel;
                _actionScreenController = actionScreenController;
                _loot = loot;
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
                //ClearEvents();
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

