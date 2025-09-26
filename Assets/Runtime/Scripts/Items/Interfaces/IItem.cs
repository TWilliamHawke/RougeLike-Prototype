using System.Collections.Generic;
using UnityEngine;
using UI.Tooltips;
using Core;

namespace Items
{
    public interface IItem : IIconData
    {
        ItemType itemType { get; }
        int maxStackSize { get; }
        int value { get; }
        AudioClip useSound { get; }
        AudioClip dragSound { get; }
        IEnumerable<ContextActionTemplate> GetActions();
        ItemTooltipData GetTooltipData();
    }
}