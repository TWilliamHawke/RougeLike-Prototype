using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public interface IInventorySection
    {
        void AddItem(Item someItem);
        void AddItems(Item someItem, int count);
		bool ItemMeet(Item item);
        void Clear();
    }
}