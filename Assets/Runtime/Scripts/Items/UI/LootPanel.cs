using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Items.UI
{
    public class LootPanel : UIPanelWithGrid<ItemSlotData>
    {
        [SerializeField] Inventory _inventory;

        [SerializeField] Injector _selfInjector;

        protected override IEnumerable<ItemSlotData> _layoutElementsData => _loot;
        IEnumerable<ItemSlotData> _loot;

        public event UnityAction OnTakeAll;
        public event UnityAction OnClose;


        public void Init()
        {
            _selfInjector.SetDependency(this);
        }

        public void Open(ILootStorage loot)
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