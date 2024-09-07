using Entities;

namespace Items
{
    public class AliveEntitiesInventory : EntitiesInventory
    {
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


