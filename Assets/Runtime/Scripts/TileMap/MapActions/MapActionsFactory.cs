using System.Collections;
using System.Collections.Generic;
using Items;
using Items.UI;
using UnityEngine;

namespace Map.Objects
{
    [RequireComponent(typeof(ComponentInjector))]
    public class MapActionsFactory : MonoBehaviour, IMapActionsFactory
    {
        [InjectField] LootPanel _lootPanel;

        [SerializeField] Injector _selfInjector;

        Loot _lootActionCreator;

        Dictionary<MapActionType, IMapActionCreator> _mapActionCreators = new();

        private void Awake()
        {
            _selfInjector.SetDependency(this);
        }

        IMapAction IMapActionsFactory.CreateLootAction(MapActionTemplate template, ILootStorage loot)
        {
            return _lootActionCreator.CreateActionLogic(template, loot);
        }

        IMapAction IMapActionsFactory.CreateActionLogic(MapActionTemplate template)
        {
            if (_mapActionCreators.TryGetValue(template.actionType, out var actionCreator))
            {
                return actionCreator.CreateActionLogic(template);
            }
            else
            {
                return new EmptyAction(template);
            }
        }

        //used in editor
        public void CreateFactory()
        {
            _lootActionCreator = new Loot(_lootPanel);
            _mapActionCreators.Add(MapActionType.loot, _lootActionCreator);
            _mapActionCreators.Add(MapActionType.attack, new Attack());
            _mapActionCreators.Add(MapActionType.talk, new Talk());
        }
    }
}


