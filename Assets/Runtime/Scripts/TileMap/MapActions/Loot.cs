using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Items.UI;
using UnityEngine.Events;

namespace Map.Objects
{
    class Loot : IMapActionCreator
    {
        LootPanel _lootPanel;

        public Loot(LootPanel lootPanel)
        {
            _lootPanel = lootPanel;
        }

        public IMapAction CreateActionLogic(MapActionTemplate template)
        {
            if((template as ILootActionData)?.lootTable is null)
            {
                throw new System.Exception($"{template.name} labeled as loot but loot table is not assigned");
            }
            return new LootAction(template, _lootPanel);
        }

        public IMapAction CreateActionLogic(MapActionTemplate template, ILootStorage loot)
        {
            return new LootAction(template, loot, _lootPanel);
        }


        class LootAction : IMapAction
        {
            LootPanel _lootPanel;
            ILootActionData _template;
            public Sprite icon => _template.icon;
            public string actionTitle => _template.displayName;

            ILootStorage _loot;

            //in some locations action can be used multiple times
            //so it require outer control
            public bool isEnable { get; set; } = true;

            public event UnityAction<IMapAction> OnCompletion;


            public LootAction(ILootActionData template, LootPanel lootPanel)
            {
                _loot = new ItemSection<Item>(ItemContainerType.loot);
                _template = template;
                template.lootTable.FillItemSection(ref _loot);
                _lootPanel = lootPanel;
            }

            public LootAction(ILootActionData template, ILootStorage loot, LootPanel lootPanel)
            {
                _template = template;
                _loot = loot;
                _lootPanel = lootPanel;
            }

            public void DoAction()
            {
                _lootPanel.Open(_loot);
                _lootPanel.OnTakeAll += InvokeEvent;
                _lootPanel.OnClose += ClearEvents;
            }

            void InvokeEvent()
            {
                OnCompletion?.Invoke(this);
                ClearEvents();
            }

            void ClearEvents()
            {
                _lootPanel.OnTakeAll -= InvokeEvent;
                _lootPanel.OnClose -= ClearEvents;
            }

        }
    }
}

