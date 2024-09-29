using System.Collections.Generic;
using Entities;
using Entities.NPC;

namespace Items
{
    public class DeadEntitiesStorage : EntitiesStorage
    {
        public override bool isStealingTarget { get; set; } = false;        

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