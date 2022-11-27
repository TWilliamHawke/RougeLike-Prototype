using System.Collections;
using System.Collections.Generic;
using Entities.InteractiveObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Items.UI
{
    public class LootPanel : UIPanelWithGrid<ItemSlotData>, IUIScreen
    {
        [SerializeField] Inventory _inventory;

        [SerializeField] Injector _selfInjector;

        protected override IEnumerable<ItemSlotData> _layoutElementsData => _loot;
        ItemSection<Item> _loot;

        public event UnityAction OnTakeAll;
        public event UnityAction OnClose;


        public void Init()
        {
            _selfInjector.SetDependency(this);
            Container.OnContainerOpen += Open;
        }

        private void OnDestroy()
        {
            Container.OnContainerOpen -= Open;
        }

        public void Open(ItemSection<Item> loot)
        {
            _loot = loot;
            gameObject.SetActive(true);
            UpdateLayout();
        }

        //used as click handler in editor
        public void Close()
        {
            gameObject.SetActive(false);
            OnClose?.Invoke();
        }

        //used as click handler in editor
        public void TakeAll(Inventory inventory)
        {
            foreach (var slotData in _loot)
            {
                inventory.AddItems(slotData.item, slotData.count);
            }
            //_loot.Clear();
            gameObject.SetActive(false);
            OnTakeAll?.Invoke();
        }
    }
}