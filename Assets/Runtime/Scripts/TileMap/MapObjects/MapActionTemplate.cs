using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Objects
{
    [CreateAssetMenu(menuName = "Map/Action", fileName = "MapObjectAction")]
    public class MapActionTemplate : ScriptableObject, IIconData, IActionLogicCreator
    {
        [UseFileName]
        [SerializeField] string _displayName;
        [SpritePreview]
        [SerializeField] Sprite _icon;

        [SerializeField] ActionDependencyInjectors _dependencyInjectors;
        [Space()]
        [SerializeField] ActionCreator _actionLogicCreator;

        public string displayName => _displayName;
        public Sprite icon => _icon;

        IMapActionLogic IActionLogicCreator.CreateActionLogic()
        {
            var actionLogic = _actionLogicCreator?.Invoke();
            if(actionLogic is null)
            {
                actionLogic = new EmptyActionLogic(this);
                Debug.LogError("Action logic cannot be created");
            }

            return actionLogic;
        }

        //requires LootPanel injector
        public IMapActionLogic CreateLootLogic(LootTable lootTable)
        {
            var lootLogic = new Loot(this, lootTable);
            _dependencyInjectors.lootScreen.AddInjectionTarget(lootLogic);
            return lootLogic;
        }

        [System.Serializable]
        public class ActionCreator: SerializableCallback<IMapActionLogic>
        {
        }
    }
}

