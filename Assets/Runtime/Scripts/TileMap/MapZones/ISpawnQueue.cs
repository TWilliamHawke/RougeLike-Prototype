using Entities;
using Rng = System.Random;


namespace Map
{
    public interface ISpawnQueue
    {
        void AddToQueue(EntitiesTable entitiesTable, Rng rng);
        void AddToQueue(IEntityTemplate template, Rng rng);
        void AddObserver(IObserver<Entity> observer);
        void SpawnAll(EntitiesSpawner spawner);
    }
}


