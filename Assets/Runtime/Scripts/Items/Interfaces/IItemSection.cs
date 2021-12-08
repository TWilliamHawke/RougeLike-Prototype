using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public interface IItemSection
    {
        event UnityAction OnItemAdd;
        void AddItem(Item item);
        void AddItems(Item item, int count);
		bool ItemMeet(Item item);
        void Clear();
        int maxCount { get; }
        int count { get; }
        ItemSlotData this[int idx] { get; }
    }
}