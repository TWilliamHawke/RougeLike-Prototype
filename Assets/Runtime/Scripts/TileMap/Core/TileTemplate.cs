using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map
{
    [CreateAssetMenu(fileName = "TileTemplate", menuName = "Map/TileTemplate", order = 0)]
    public class TileTemplate : ScriptableObject
    {
        [SerializeField] TileBase _tile;
        [SerializeField] TileWalkability _walkability = TileWalkability.walkable;
        [SerializeField] TileBlockVision _vision = TileBlockVision.notBlock;
        [SerializeField] List<AudioClip> _stepSounds;

        public TileBase tile => _tile;
        public bool isWalkable => _walkability == TileWalkability.walkable;
        public bool blockVision => _vision == TileBlockVision.blocked;
        public AudioClip stepSound => _stepSounds.GetRandom();
    }
}