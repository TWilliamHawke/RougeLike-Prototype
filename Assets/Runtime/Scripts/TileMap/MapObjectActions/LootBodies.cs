using System.Collections;
using System.Collections.Generic;
using Items;
using Items.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Objects
{
    public class LootBodies : IMapActionLogic
    {
        IIconData _action;
        public bool isEnable { get; set; } = true;
        [InjectField] LootPanel _lootPanel;
        ItemSection<Item> _loot;

        public event UnityAction<IMapActionLogic> OnCompletion;

        public IIconData template => _action;

        public bool waitForAllDependencies => false;

        public LootBodies(IIconData action)
        {
            _action = action;
        }

        public void CreateLoot(IEnumerable<IHaveLoot> enemies)
        {
            _loot = new ItemSection<Item>(ItemContainerType.loot);
            foreach (var enemy in enemies)
            {
                enemy.lootTable.FillItemSection(ref _loot);
            }
        }

        public void DoAction()
        {
            _lootPanel.Open(_loot);
            _lootPanel.OnTakeAll += InvokeEvent;
            _lootPanel.OnClose += ClearEvents;
        }

        void InvokeEvent()
        {
            _loot.Clear();
            OnCompletion?.Invoke(this);
            ClearEvents();
        }

        void ClearEvents()
        {
            _lootPanel.OnTakeAll -= InvokeEvent;
            _lootPanel.OnClose -= ClearEvents;
        }

        public void FinalizeInjection()
        {

        }
    }

}

