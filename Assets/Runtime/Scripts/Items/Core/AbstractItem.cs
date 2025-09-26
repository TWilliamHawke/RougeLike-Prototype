using System.Collections.Generic;
using UnityEngine;
using UI.Tooltips;
using Core;

namespace Items
{
    public abstract class AbstractItem : IItem
    {
        protected abstract IItemTemplate _template { get; }
        public abstract int value { get; }

        public Sprite icon => _template.icon;
        public string displayName => _template.displayName;
        public int maxStackSize => _template.maxStackSize;
        public AudioClip useSound => _template.useSound;
        public AudioClip dragSound => _template.dragSound;

        public abstract string GetDescription();

        public IEnumerable<ContextActionTemplate> GetActions()
        {
            return _template.GetActions();
        }

        public ItemTooltipData GetTooltipData()
        {
            var tooltipData = new ItemTooltipData();
            tooltipData.icon = icon;
            tooltipData.title = displayName;
            tooltipData.itemType = displayName;
            tooltipData.description = GetDescription();

            return tooltipData;
        }

        public bool HasItemType(ItemType itemType)
        {
            return _template.HasItemType(itemType);
        }

    }
}