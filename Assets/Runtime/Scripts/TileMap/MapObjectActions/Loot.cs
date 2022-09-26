using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Items.UI;
using UnityEngine.Events;

namespace Map.Objects
{
    [CreateAssetMenu(menuName = "Map/Actions/Loot", fileName = "Loot")]
    public class Loot : MapObjectAction
    {
        [SerializeField] LootTable _lootTable;

        public override IMapActionLogic actionLogic => new LootLogic(this);

        class LootLogic : IMapActionLogic
        {
            Loot _action;
            public bool isEnable { get; set; } = true;
            LootPanel _lootPanel;

            public event UnityAction<IMapActionLogic> OnCompletion;

            public IActionData template => _action;

            public LootLogic(Loot action)
            {
                _action = action;
            }

            public void DoAction()
            {
                _lootPanel.Open(_action._lootTable.GetLoot());
                _lootPanel.OnTakeAll += InvokeEvent;
                _lootPanel.OnClose += ClearEvents;
            }

            public void AddActionDependencies(IActionDependenciesProvider provider)
            {
                _lootPanel = provider.lootPanel;
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

