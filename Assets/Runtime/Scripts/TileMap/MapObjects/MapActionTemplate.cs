using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Objects
{
    [CreateAssetMenu(menuName = "Map/Action", fileName = "MapObjectAction")]
    public class MapActionTemplate : ScriptableObject, ILootActionData
    {
        [UseFileName]
        [SerializeField] string _displayName;
        [SpritePreview]
        [SerializeField] Sprite _icon;

        [SerializeField] MapActionType _actionType;
        [Space()]
        [SerializeField] LootTable _lootTable;
        [SerializeField] string _lootDescription;

        public string displayName => _displayName;
        public Sprite icon => _icon;
        public MapActionType actionType => _actionType;
        LootTable ILootActionData.lootTable => _lootTable;
        string ILootActionData.lootDescription => _lootDescription;
    }
}

