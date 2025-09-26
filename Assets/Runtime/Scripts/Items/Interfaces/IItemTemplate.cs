using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Items
{
    public interface IItemTemplate : IIconData
    {
        int maxStackSize { get; }
        AudioClip useSound { get; }
        AudioClip dragSound { get; }
        IEnumerable<ContextActionTemplate> GetActions();
        bool HasItemType(ItemType itemType);
        IItem CreateItem(int rarity = 0);
    }
}