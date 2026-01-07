using System.Collections.Generic;

namespace Items
{
    public interface IItemContainer
    {
        ItemStorageType storageType { get; }
        IEnumerable<ItemSlotData> GetItems();
    }
}


