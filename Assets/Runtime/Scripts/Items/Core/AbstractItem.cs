using System.Collections.Generic;
using UnityEngine;
using UI.Tooltips;
using Core;

namespace Items
{
    public abstract class AbstractItem : IItem
    {
        protected abstract ItemTemplate _template { get; }

        public Sprite icon => _template.icon;
        public abstract string displayName {get; }
        public int maxStackSize => _template.maxStackSize;
        public AudioClip useSound => _template.useSound;
        public AudioClip dragSound => _template.dragSound;

        public abstract int value { get; }
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