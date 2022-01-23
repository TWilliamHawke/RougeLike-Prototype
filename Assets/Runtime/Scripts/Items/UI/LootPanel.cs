using System.Collections;
using System.Collections.Generic;
using Entities.InteractiveObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Items.UI
{
    public class LootPanel : UIPanelWithGrid<ItemSlotData>, IUIScreen
    {
        [SerializeField] Inventory _inventory;

        protected override IEnumerable<ItemSlotData> _layoutElementsData => _loot;
        ItemSection<Item> _loot;

        public void Init()
        {
            Container.OnContainerOpen += Open;
        }

        private void OnDestroy()
        {
            Container.OnContainerOpen -= Open;
        }


        void Open(ItemSection<Item> loot)
        {
            _loot = loot;
            UpdateLayout();
            gameObject.SetActive(true);
        }

        //used as click handler in editor
        public void Close()
        {
            gameObject.SetActive(false);
        }

        //used as click handler in editor
        public void TakeAll(Inventory inventory)
        {
            foreach (var slotData in _loot)
            {
                inventory.AddItems(slotData.item, slotData.count);
            }
            //_loot.Clear();
            Close();
        }

    }
}