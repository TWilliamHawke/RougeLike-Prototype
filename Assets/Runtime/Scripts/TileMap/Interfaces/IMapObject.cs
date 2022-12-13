using UnityEngine;

namespace Map
{
    public interface IMapObject
    {
        IIconData template { get; }
        RandomStack<Vector3Int> GetWalkableTiles();
        void BindTemplate(IMapObjectTemplate template);
        IMapObjectBehavior behavior { get; }
    }
}

