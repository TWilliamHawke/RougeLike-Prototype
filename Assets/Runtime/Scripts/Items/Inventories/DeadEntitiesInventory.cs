using System.Collections.Generic;
using Entities;
using Entities.NPC;

namespace Items
{
    public class DeadEntitiesInventory : EntitiesInventory
    {
        public override void AddToObserve(Entity target)
        {
            target.OnDeath += HandleDeath;
        }

        public override void RemoveFromObserve(Entity target)
        {
            target.OnDeath -= HandleDeath;
        }

        protected override void HandleDeath(Entity target)
        {
            target.AddLoot(this);
        }
    }
}