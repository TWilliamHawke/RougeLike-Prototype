using UnityEngine;

namespace Map
{
    public interface IMapZoneTemplate : IIconData
    {
        Vector2Int size { get; }
    }

    public interface ISpawnZoneTemplate
    {
        Vector2Int size { get; }
        Vector2Int centerZoneSize { get; }
        bool centerZoneIsWalkable { get; }
    }
}

