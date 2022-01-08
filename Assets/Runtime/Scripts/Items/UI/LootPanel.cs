using System.Collections;
using System.Collections.Generic;
using Entities.InteractiveObjects;
using UnityEngine;

namespace Items.UI
{
    public class LootPanel : UIPanelWithGrid<ItemSlotData>, IUIScreen
    {
        protected override IEnumerable<ItemSlotData> _layoutElementsData => _loot;
        ItemSection<Item> _loot;

        void Open(ItemSection<Item> loot)
        {
            _loot = loot;
            UpdateLayout();
            gameObject.SetActive(true);
        }

        public void Init()
        {
            Container.OnContainerOpen += Open;
        }

        private void OnDestroy()
        {
            Container.OnContainerOpen -= Open;
        }
    }
}