using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Actions
{
    public class OpenWorldActionsController : IMapActionsController
    {
		List<IMapAction> _actionsLogic = new List<IMapAction>();

        public IMapAction this[int idx] => _actionsLogic[idx];
        public int count => _actionsLogic.Count;
        IMapActionsFactory _actionsFactory;

        public OpenWorldActionsController(IMapActionsFactory actionsFactory)
        {
            _actionsFactory = actionsFactory;
        }

        public void AddAction(MapActionTemplate template)
        {
            _actionsLogic.Add(_actionsFactory.CreateActionLogic(template, 1));
        }

        public void AddLootAction(MapActionTemplate template, IEnumerable<IHaveLoot> enemies)
        {
            var loot = new ItemSection(new LootSectionTemplate());

            foreach (var enemy in enemies)
            {
                enemy.lootTable.FillItemSection(ref loot);
            }

            if(loot.isEmpty) return;
            _actionsLogic.Add(_actionsFactory.CreateLootAction(template, loot));
        }
    }
}

