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
        IMapActionLocation _actionsStore;

        public OpenWorldActionsController(IMapActionsFactory actionsFactory, IMapActionLocation actionsStore)
        {
            _actionsFactory = actionsFactory;
            _actionsStore = actionsStore;
        }

        public void AddAction(MapActionTemplate template)
        {
            _actionsLogic.Add(_actionsFactory.CreateAction(template, _actionsStore));
        }

        public void AddLootAction(MapActionTemplate template, IContainersList loot)
        {
            _actionsLogic.Add(_actionsFactory.CreateLootAction(template, loot));
        }
    }
}

