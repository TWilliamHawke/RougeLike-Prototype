using System.Collections.Generic;
using UnityEngine;
using UI.Tooltips;
using Core;

namespace Items
{
    public interface IItemTemplate : IIconData
    {
        int maxStackSize { get; }
        AudioClip useSound { get; }
        AudioClip dragSound { get; }
        string GetDescription();
        ItemTooltipData GetTooltipData();
        IEnumerable<ContextActionTemplate> GetActions();
        bool HasItemType(ItemType itemType);
    }
}