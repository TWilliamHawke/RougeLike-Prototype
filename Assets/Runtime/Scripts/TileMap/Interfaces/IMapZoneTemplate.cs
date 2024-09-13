using Entities;
using UnityEngine;

namespace Map
{
    public interface IMapZoneTemplate : IIconData, ISpawnZoneTemplate
    {
    }

    public interface ISpawnZoneTemplate
    {
        Vector2Int size { get; }
        Vector2Int centerZoneSize { get; }
        bool centerZoneIsWalkable { get; }
        EntitiesTable enemies { get;}
    }
}

