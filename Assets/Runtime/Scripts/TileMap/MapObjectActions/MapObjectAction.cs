using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Objects
{
    [CreateAssetMenu(menuName = "Map/Action", fileName = "MapObjectAction")]
    public class MapObjectAction : ScriptableObject, IIconData
    {
        [UseFileName]
        [SerializeField] string _displayName;
        [SpritePreview]
        [SerializeField] Sprite _icon;

        [SerializeField] UnityEvent _actionLogicCreator;
        [SerializeField] List<Injector> _dependencyInjectors;

        public string displayName => _displayName;
        public Sprite icon => _icon;

        IMapActionLogic _mapActionLogic;

        public IMapActionLogic CreateActionLogic()
        {
            _actionLogicCreator?.Invoke();
            if(_mapActionLogic is null)
            {
                _mapActionLogic = new EmptyActionLogic(this);
                Debug.LogError("Action logic cannot be created");
            }

            foreach(var injector in _dependencyInjectors)
            {
                injector.AddInjectionTarget(_mapActionLogic);
            }

            return _mapActionLogic;
        }

        public void CreateLootLogic(LootTable lootTable)
        {
            _mapActionLogic = new Loot(this, lootTable);
        }
    }
}

