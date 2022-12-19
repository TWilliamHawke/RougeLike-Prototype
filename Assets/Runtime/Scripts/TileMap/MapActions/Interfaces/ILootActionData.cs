using Items;

namespace Map
{
    public interface ILootActionData : IIconData
    {
        LootTable lootTable { get; }
        string lootDescription { get; }
    }
}

