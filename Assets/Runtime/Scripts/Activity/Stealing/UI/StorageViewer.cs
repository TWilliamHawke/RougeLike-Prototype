using System.Collections.Generic;
using Lockpicking;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Items.UI
{
    public class StorageViewer : MonoBehaviour, IObserver<ItemSlotWithPrice>
    {
        [SerializeField] TextMeshProUGUI _storageName;
        [SerializeField] ShopSection _listView;
        [SerializeField] ShopSection _gridView;

        [SerializeField] StorageProtectionPanel _lockPanel;
        [SerializeField] StorageProtectionPanel _trapPanel;
        [SerializeField] LockpickingPanel _lockpickingPanel;

        public event UnityAction<ItemSlotData> OnItemSelection;

        ItemContainer _storage;
        ValueStorage _actionPoints;
        HashSet<ItemSlotData> _selectedItems = new();

        int _unlockSkill = 4;
        int _disarmTrapSkill = 4;

        void Start()
        {
            _lockPanel.OnButtonClick += ShowLockpickingPanel;
            _trapPanel.OnButtonClick += DisarmTrap;
            _lockpickingPanel.OnUnlock += UnlockChest;
            _listView.AddObserver(this);
            _gridView.AddObserver(this);
        }

        public void SetActionPoints(ValueStorage actionPoints, HashSet<ItemSlotData> selectedItems)
        {
            _actionPoints = actionPoints;
            _selectedItems = selectedItems;
        }

        public void ShowStorage(ItemContainer storage)
        {
            _storage = storage;
            _storageName.text = storage.storageName;
            UpdatePanels();
        }

        public void UpdatePanels()
        {
            HideAllPanels();

            if (_storage.lockLevel > 0)
            {
                ShowLockPanel();
                return;
            }

            if (_storage.trapLevel > 0)
            {
                ShowTrapPanel();
                return;
            }

            if (_storage.isIdentified)
            {
                _listView.Show();
                _listView.ShowUnselectedItems(_storage.GetItems(), _selectedItems);
                return;
            }

            _gridView.Show();
            _gridView.ShowUnselectedItems(_storage.GetItems(), _selectedItems);
        }

        private void HideAllPanels()
        {
            _listView.Hide();
            _gridView.Hide();
            _lockPanel.Hide();
            _trapPanel.Hide();
            _lockpickingPanel.Hide();
        }

        private void SelectItem(ItemSlotData item)
        {
            OnItemSelection?.Invoke(item);
        }

        private void UnlockChest()
        {
            _storage.Unlock();
            UpdatePanels();
        }

        private void DisarmTrap()
        {
            _storage.DisarmTrap();
            UpdatePanels();
        }

        private void ShowLockpickingPanel()
        {
            _lockPanel.Hide();
            _lockpickingPanel.OpenScreen(_storage.lockLevel);
        }

        private void ShowLockPanel()
        {
            _lockPanel.Show();
            _lockPanel.ResetProtection();
            _lockPanel.SetSkillProtection(_unlockSkill, _storage.lockLevel);
            _lockPanel.SetCostProtection(_actionPoints.currentValue, 20);
        }

        private void ShowTrapPanel()
        {
            _trapPanel.Show();
            _trapPanel.ResetProtection();
            _trapPanel.SetSkillProtection(_disarmTrapSkill, _storage.trapLevel);
            _trapPanel.SetCostProtection(_actionPoints.currentValue, 20);
        }

        void IObserver<ItemSlotWithPrice>.AddToObserve(ItemSlotWithPrice target)
        {
            target.OnClick += SelectItem;
            target.SetValueStorage(_actionPoints);
        }

        void IObserver<ItemSlotWithPrice>.RemoveFromObserve(ItemSlotWithPrice target)
        {
            target.OnClick -= SelectItem;
        }

    }
}
