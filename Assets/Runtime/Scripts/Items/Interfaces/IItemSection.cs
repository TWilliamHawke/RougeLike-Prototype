using UnityEngine;

namespace Items
{
    public interface IItemSection
    {
        // event UnityAction OnItemRemove;
        // void RemoveItemFromSlot(ItemSlotData itemSlotData);
        void AddItem(IItem item);
        void AddItems(IItem item, int count);
        bool ItemMeet(IItem item);
        void Clear();
        int FindItemCount(IItem item);
        void RemoveItem(IItem item);
        bool HasItem(IItem item);
    }
}