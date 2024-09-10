using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Items.UI
{
    public class LootPanel : MonoBehaviour
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] InventorySection _lootSection;

        [SerializeField] UIScreen _canvas;
        [SerializeField] TMP_Text _lootText;

        IContainersList _loot;

        public event UnityAction OnTakeAll;
        public event UnityAction OnClose;
        int _selectedContainerIdx = 0;

        ItemContainer _selectedContainer => _loot.ContainerAt(_selectedContainerIdx);


        public void Open(IContainersList loot)
        {
            _loot = loot;
            _selectedContainerIdx = 0;
            _lootSection.BindData(_selectedContainer);
            _lootSection.UpdateSectionView();
            UpdateTakeAllText();
            _canvas.Open();
        }

        public void Close()
        {
            _canvas.Close();
            OnClose?.Invoke();
        }

        //used as click handler in editor
        public void TakeAll(Inventory inventory)
        {
            foreach (var slotData in _selectedContainer)
            {
                inventory.AddItems(slotData.item, slotData.count);
            }

            _selectedContainerIdx++;
            OnTakeAll?.Invoke();
            UpdateTakeAllText();

            if (_selectedContainerIdx >= _loot.count)
            {
                Close();
            }
        }

        private void UpdateTakeAllText()
        {
            _lootText.text = $"Take all ({_selectedContainerIdx + 1}/{_loot.count})";
        }
    }
}