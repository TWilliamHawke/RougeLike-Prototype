using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Objects
{
    public class DefaultMapActionsController : IMapActionsController
    {
		List<IMapAction> _actionsLogic = new List<IMapAction>();

        public event UnityAction OnActionStateChange;

        public IMapAction this[int idx] => _actionsLogic[idx];
        public int count => _actionsLogic.Count;
        IMapActionsFactory _actionsFactory;

        public DefaultMapActionsController(IMapActionsFactory actionsFactory)
        {
            _actionsFactory = actionsFactory;
        }

        public void AddAction(MapActionTemplate template)
        {
            AddAction(_actionsFactory.CreateActionLogic(template));
        }

        public void CreateLootAction(MapActionTemplate template, IEnumerable<IHaveLoot> enemies)
        {
            var loot = new ItemSection<Item>(ItemContainerType.loot);

            foreach (var enemy in enemies)
            {
                enemy.lootTable.FillItemSection(ref loot);
            }

            AddAction(_actionsFactory.CreateLootAction(template, loot));
        }

        private void AddAction(IMapAction actionLogic)
		{
			_actionsLogic.Add(actionLogic);
			actionLogic.OnCompletion += DisableAction;
		}

		private void DisableAction(IMapAction actionLogic)
		{
			actionLogic.isEnable = false;
			OnActionStateChange?.Invoke();
		}
    }
}

