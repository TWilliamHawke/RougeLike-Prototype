using Map.Zones;
using UnityEngine;

namespace Map
{
    public interface IMapZoneTemplate : IIconData, ISpawnZoneTemplate
    {
        ITaskController CreateTaskController();
        void FillSpawnQueue(ISpawnQueue spawnQueue, System.Random rng);
    }

    public interface ISpawnZoneTemplate
    {
        Vector2Int size { get; }
        Vector2Int centerZoneSize { get; }
        bool centerZoneIsWalkable { get; }
    }
}
