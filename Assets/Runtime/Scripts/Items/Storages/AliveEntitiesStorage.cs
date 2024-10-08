using Entities;

namespace Items
{
    public class AliveEntitiesStorage : EntitiesStorage
    {
        public override bool isStealingTarget { get; set; } = true;

        public override void AddToObserve(Entity target)
        {
            target.OnDeath += HandleDeath;
            target.AddLoot(this);
        }

        public override void RemoveFromObserve(Entity target)
        {
            target.OnDeath -= HandleDeath;
        }

        protected override void HandleDeath(Entity target)
        {
            target.RemoveLoot(this);
        }
    }
}


