using Entities;

namespace Items
{
    public class AliveEntitiesStorage : EntitiesStorage
    {
        public override bool isStealingTarget { get; set; } = true;
        public override int count => _containers.Count;

        public override void AddToObserve(Entity target)
        {
            target.OnDeath += HandleDeath;
            target.AddLootTo(this);
        }

        public override void RemoveFromObserve(Entity target)
        {
            target.OnDeath -= HandleDeath;
        }

        public override ItemContainer ContainerAt(int idx)
        {
            return _containers[idx];
        }

        protected override void HandleDeath(Entity target)
        {
            target.RemoveLootFrom(this);
        }
    }
}


