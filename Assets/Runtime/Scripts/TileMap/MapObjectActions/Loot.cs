using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Items.UI;

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
            public bool isEnable { get; set; }
            LootPanel _lootPanel;
            public IActionData template => _action;

            public LootLogic(Loot action)
            {
                _action = action;
            }

            public void DoAction()
            {
                _lootPanel.Open(_action._lootTable.GetLoot());
            }

            public void AddActionDependencies(IActionDependenciesProvider provider)
            {
                _lootPanel = provider.lootPanel;
            }
        }
    }
}

