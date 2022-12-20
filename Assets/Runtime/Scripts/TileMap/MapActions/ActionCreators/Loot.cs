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

        public IMapAction CreateActionLogic(MapActionTemplate template, int numOfUsage)
        {
            if ((template as ILootActionData)?.lootTable is null)
            {
                throw new System.Exception($"{template.name} labeled as loot but loot table is not assigned");
            }
            return new LootAction(template, _lootPanel, numOfUsage, _actionScreenController);
        }


        class LootAction : IMapAction
        {
            LootPanel _lootPanel;
            ILootActionData _template;
            public Sprite icon => _template.icon;
            public string actionTitle => _template.displayName;

            LootTable _loot;
            int _numOfUsage;
            IActionScreenController _actionScreenController;
            public bool isEnable => _numOfUsage != 0;
            public bool isHidden => false;

            public LootAction(ILootActionData template, LootPanel lootPanel, int numOfUsage, IActionScreenController actionScreenController)
            {
                _loot = template.lootTable;
                _template = template;
                _lootPanel = lootPanel;
                _numOfUsage = numOfUsage;
                _actionScreenController = actionScreenController;
            }

            public void DoAction()
            {
                _lootPanel.Open(_loot.GetLoot());
                _actionScreenController.CloseActionScreen();
                _lootPanel.OnTakeAll += CompleteAction;
                _lootPanel.OnClose += ClearEvents;
            }

            void CompleteAction()
            {
                if (_numOfUsage > 0)
                {
                    _numOfUsage--;
                }

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

