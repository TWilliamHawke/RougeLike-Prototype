using System.Collections.Generic;

namespace Items
{
    public interface IInventoryIterator
    {
        IEnumerable<ItemSlotData> GetMainItems();
        IEnumerable<ItemSectionTemplate> GetVisibleSections();
        bool HasEquipmentForSlot(ItemSlotData slot);
    }
}