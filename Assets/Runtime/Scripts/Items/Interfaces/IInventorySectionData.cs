using System.Collections.Generic;
using UnityEngine.Events;

namespace Items
{
    public interface IInventorySectionData : IEnumerable<ItemSlotData>
    {
        event UnityAction OnSectionDataChange;
        int capacity { get; }
        int count { get; }
        bool isInfinity { get; }
        string sectionName { get; }
    }
}