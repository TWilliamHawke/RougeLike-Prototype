using System.Collections.Generic;
using UnityEngine;
using UI.Tooltips;
using Core;

namespace Items
{
    public interface IItem : IIconData
    {
        int maxStackSize { get; }
        int value { get; }
        AudioClip useSound { get; }
        AudioClip dragSound { get; }
        bool HasItemType(ItemType itemType);
        string GetDescription();
        IEnumerable<ContextActionTemplate> GetActions();
        ItemTooltipData GetTooltipData();
    }
}