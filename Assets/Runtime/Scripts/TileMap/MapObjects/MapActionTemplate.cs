using System.Collections;
using System.Collections.Generic;
using Entities;
using Items;
using UnityEngine;

namespace Map.Objects
{
    [CreateAssetMenu(menuName = "Map/Action", fileName = "MapObjectAction")]
    public class MapActionTemplate : ScriptableObject, ILootActionData, IAttackActionData
    {
        [UseFileName]
        [SerializeField] string _displayName;
        [SpritePreview]
        [SerializeField] Sprite _icon;

        [SerializeField] MapActionType _actionType;
        [Space()]
        [SerializeField] LootTable _lootTable;
        [SerializeField] string _lootDescription;
        [SerializeField] Faction _enemyFaction;

        public string displayName => _displayName;
        public Sprite icon => _icon;
        public MapActionType actionType => _actionType;

        public Faction enemyFaction => _enemyFaction;
        LootTable ILootActionData.lootTable => _lootTable;
        string ILootActionData.lootDescription => _lootDescription;
    }
}

