using Items;

namespace Map.Objects
{
    public interface ILootActionData : IIconData
    {
        LootTable lootTable { get; }
        string lootDescription { get; }
    }
}

