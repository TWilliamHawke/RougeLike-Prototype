using UnityEngine;

namespace Items
{
    public interface IItemSection
    {
        // event UnityAction OnItemRemove;
        // void RemoveItemFromSlot(ItemSlotData itemSlotData);
        void AddItem(Item item);
        void AddItems(Item item, int count);
		bool ItemMeet(Item item);
        void Clear();
        int FindItemCount(Item item);
    }
}