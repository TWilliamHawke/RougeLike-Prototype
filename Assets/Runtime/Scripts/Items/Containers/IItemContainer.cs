using System.Collections.Generic;

namespace Items
{
    public interface IItemContainer : IEnumerable<ItemSlotData>
    {
        ItemStorageType storageType { get; }
    }
}


