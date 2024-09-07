using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Items.UI
{
    public class LootPanel : MonoBehaviour
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] InventorySection _lootSection;

        [SerializeField] UIScreen _canvas;

        ILootStorage _loot;

        public event UnityAction OnTakeAll;
        public event UnityAction OnClose;


        public void Open(ILootStorage loot)
        {
            _loot = loot;
            _canvas.Open();
        }

        //used as click handler in editor
        public void Close()
        {
            _canvas.Close();
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