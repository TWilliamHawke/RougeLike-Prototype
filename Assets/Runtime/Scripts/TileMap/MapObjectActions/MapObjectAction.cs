using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Objects
{
    [CreateAssetMenu(menuName = "Map/Action", fileName = "MapObjectAction")]
    public class MapObjectAction : ScriptableObject, IIconData, IActionLogicCreator
    {
        [UseFileName]
        [SerializeField] string _displayName;
        [SpritePreview]
        [SerializeField] Sprite _icon;

        [SerializeField] List<Injector> _dependencyInjectors;
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

            foreach(var injector in _dependencyInjectors)
            {
                injector.AddInjectionTarget(actionLogic);
            }

            return actionLogic;
        }

        //requires LootPanel injector
        public IMapActionLogic CreateLootLogic(LootTable lootTable)
        {
            return new Loot(this, lootTable);
        }

        [System.Serializable]
        public class ActionCreator: SerializableCallback<IMapActionLogic>
        {
        }
    }
}

