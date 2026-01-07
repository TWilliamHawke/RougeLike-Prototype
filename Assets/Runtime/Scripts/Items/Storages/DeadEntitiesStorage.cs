using Entities;

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

        public override ItemContainer ContainerAt(int idx)
        {
            if (!_lootItems.isEmpty)
            {
                if (idx == 0)
                {
                    return _lootItems;
                }
                idx--;
            }

            return _containers[idx];
        }

        protected override void HandleDeath(Entity target)
        {
            target.AddLootTo(this);
        }
    }
}