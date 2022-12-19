using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Entities.NPCScripts;
using Entities;

namespace Map.Objects
{
    [CreateAssetMenu(fileName = "RandomEncounter", menuName = "Map/Templates/Random Encounter", order = 0)]
    public class EncounterTemplate : ScriptableObject, IMapObjectTemplate, IObjectWithCenterZone
    {
        [UseFileName]
        [SerializeField] string _displayName;
        [SpritePreview]
        [SerializeField] Sprite _icon;
		[TextArea(3,5)]
		[SerializeField] string _interactionDescription;
        [SerializeField] MapActionTemplate[] _possibleActions;

	    [SerializeField] int _width = 5;
		[SerializeField] int _height = 5;

		[Header("Tiles")]
		[SerializeField] int _tilesWidth = 3;
		[SerializeField] int _tilesHeight = 3;
		[SerializeField] TileBase _centerTile;
		[SerializeField] bool _tilesIsWalkable;

        [SerializeField] NPCTemplate _mainNPC;

        [SerializeField] CreaturesTable _otherEntities;


        public int width => _width;
        public int height => _height;
        public string displayName => _displayName;
        public Sprite icon => _icon;

        public int centerZoneWidth => _tilesWidth;
        public int centerZoneHeight => _tilesHeight;
        public bool centerZoneIsWalkable => _tilesIsWalkable;
        public TileBase centerZoneTile => _centerTile;

        public NPCTemplate mainNPC => _mainNPC;
        public MapActionTemplate[] possibleActions => _possibleActions;
    }
}


