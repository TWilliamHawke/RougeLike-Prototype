using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public interface IItemSection : IEnumerable<ItemSlotData>
    {
        event UnityAction OnItemAdd;
        event UnityAction OnItemRemove;
        void AddItem(Item item);
        void AddItems(Item item, int count);
        void RemoveItemFromSlot(ItemSlotData itemSlotData);
		bool ItemMeet(Item item);
        void Clear();
        int maxCount { get; }
        int count { get; }
        ItemSlotData this[int idx] { get; }
        int FindItemCount(Item item);
    }
}