using UnityEngine.Events;

namespace Items
{
    public interface IInventorySectionData
    {
        event UnityAction OnSectionDataChange;
        int capacity { get; }
        int count { get; }
        ItemSlotData this[int idx] { get; }
    }
}