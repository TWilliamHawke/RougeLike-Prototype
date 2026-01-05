using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map
{
    [CreateAssetMenu(fileName = "TileTemplate", menuName = "Map/TileTemplate", order = 0)]
    public class TileTemplate : ScriptableObject
    {
        [SerializeField] TileBase _tile;
        [SerializeField] bool _isWalkable;
        [SerializeField] List<AudioClip> _stepSounds;

        public TileBase tile => _tile;
        public bool isWalkable => _isWalkable;
        public AudioClip stepSound => _stepSounds.GetRandom();
    }
}