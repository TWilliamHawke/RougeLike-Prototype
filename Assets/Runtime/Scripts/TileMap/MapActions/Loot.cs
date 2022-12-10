using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Items.UI;
using UnityEngine.Events;

namespace Map.Objects
{
    class Loot : IMapActionLogic
    {
        IIconData _action;
        [InjectField] LootPanel _lootPanel;
        LootTable _lootTable;

        public bool isEnable { get; set; } = true;

        public event UnityAction<IMapActionLogic> OnCompletion;

        public IIconData template => _action;
        public bool waitForAllDependencies => false;

        public Loot(IIconData action, LootTable lootTable)
        {
            _action = action;
            _lootTable = lootTable;
        }

        public void DoAction()
        {
            _lootPanel.Open(_lootTable.GetLoot());
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

        public void FinalizeInjection()
        {
            
        }
    }
}

