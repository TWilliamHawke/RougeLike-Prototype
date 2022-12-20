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

        [SerializeField] Injector _thisInjector;

        StaticLoot _lootActionCreator;

        Dictionary<MapActionType, IMapActionCreator> _mapActionCreators = new();
        Dictionary<MapActionType, INpcActionCreator> _npcActionCreators = new();

        private void Awake()
        {
            _thisInjector.SetDependency(this);
        }

        IMapAction IMapActionsFactory.CreateLootAction(MapActionTemplate template, ILootStorage loot)
        {
            return _lootActionCreator.CreateActionLogic(template, loot);
        }

        IMapAction IMapActionsFactory.CreateActionLogic(MapActionTemplate template, int numOfUsage)
        {
            if (_mapActionCreators.TryGetValue(template.actionType, out var actionCreator))
            {
                return actionCreator.CreateActionLogic(template, numOfUsage);
            }
            else
            {
                return new EmptyAction(template);
            }
        }

        //used in editor
        public void CreateFactory()
        {
            _lootActionCreator = new StaticLoot(_lootPanel, _actionScreenController);
            _mapActionCreators.Add(MapActionType.loot, new Loot(_lootPanel, _actionScreenController));

            _npcActionCreators.Add(MapActionType.talk, new Talk());
            _npcActionCreators.Add(MapActionType.attack, new Attack(_actionScreenController));
            _npcActionCreators.Add(MapActionType.rob, new Rob());
            _npcActionCreators.Add(MapActionType.trade, new Trade());
        }

        public IMapAction CreateNPCAction(MapActionTemplate template, INpcActionTarget target, int numOfUsage = -1)
        {
            if(_npcActionCreators.TryGetValue(template.actionType, out var npcActionCreator))
            {
                return npcActionCreator.CreateActionLogic(template, target);
            }
            if (_mapActionCreators.TryGetValue(template.actionType, out var actionCreator))
            {
                return actionCreator.CreateActionLogic(template, numOfUsage);
            }
            else
            {
                return new EmptyAction(template);
            }
        }
    }
}


