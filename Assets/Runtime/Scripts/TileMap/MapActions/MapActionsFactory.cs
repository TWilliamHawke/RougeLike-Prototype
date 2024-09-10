using System.Collections;
using System.Collections.Generic;
using Items;
using Items.UI;
using Map.UI;
using UnityEngine;

namespace Map.Actions
{
    [RequireComponent(typeof(ComponentInjector))]
    public class MapActionsFactory : MonoBehaviour, IMapActionsFactory
    {
        [InjectField] LootPanel _lootPanel;
        [InjectField] ActionScreenController _actionScreenController;
        [InjectField] StealingController _stealingController;

        [SerializeField] Injector _thisInjector;

        Loot _lootActionCreator;

        Dictionary<MapActionType, IMapActionCreator> _mapActionCreators = new();

        private void Awake()
        {
        }

        //used in editor
        public void CreateFactory()
        {
            if (_lootActionCreator != null) return;
            _lootActionCreator = new Loot(_lootPanel, _actionScreenController);
            _mapActionCreators.Add(MapActionType.loot, _lootActionCreator);
            _mapActionCreators.Add(MapActionType.talk, new Talk());
            _mapActionCreators.Add(MapActionType.attack, new Attack(_actionScreenController));
            _mapActionCreators.Add(MapActionType.rob, new Rob(_stealingController));
            _mapActionCreators.Add(MapActionType.trade, new Trade());
        }

        IMapAction IMapActionsFactory.CreateLootAction(MapActionTemplate template, IContainersList loot)
        {
            CreateFactory();
            return _lootActionCreator.CreateLootAction(template, loot);
        }

        IMapAction IMapActionsFactory.CreateAction(MapActionTemplate template, IMapActionLocation store)
        {
            CreateFactory();
            if (_mapActionCreators.TryGetValue(template.actionType, out var actionCreator))
            {
                return actionCreator.CreateActionLogic(template, store);
            }
            else
            {
                return new EmptyAction(template);
            }
        }
    }
}


