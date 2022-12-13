using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map.Objects
{
    [CreateAssetMenu(fileName = "RandomEncounter", menuName = "Map/Templates/Random Encounter", order = 0)]
    public class RandomEncounterTemplate : ScriptableObject, IMapObjectTemplate
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
		[SerializeField] TileBase _siteTile;
		[SerializeField] bool _tilesIsWalkable;


        public int centerZoneWidth => _tilesWidth;
        public int centerZoneHeight => _tilesHeight;
        public int width => _width;
        public int height => _height;
        public bool centerZoneIsWalkable => _tilesIsWalkable;
        public string displayName => _displayName;
        public Sprite icon => _icon;
    }
}


