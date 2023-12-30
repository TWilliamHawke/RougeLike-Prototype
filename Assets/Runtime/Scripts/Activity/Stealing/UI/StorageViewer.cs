using UnityEngine;

namespace Items.UI
{
    public class StorageViewer : MonoBehaviour, IObserver<ItemSlotWithPrice>
    {
        [SerializeField] ShopSection _listView;
        [SerializeField] ShopSection _gridView;

        [SerializeField] StorageProtectionPanel _lockPanel;
        [SerializeField] StorageProtectionPanel _trapPanel;

        ItemStorage _storage;
        ValueStorage _actionPoints;

        int _unlockSkill = 4;
        int _disarmTrapSkill = 4;

        void Start()
        {
            _lockPanel.OnDisable += UnlockChest;
            _trapPanel.OnDisable += DisarmTrap;
            _listView.AddObserver(this);
            _gridView.AddObserver(this);
        }

        public void SetActionPoints(ValueStorage actionPoints)
        {
            _actionPoints = actionPoints;
        }

        public void ShowStorage(ItemStorage storage)
        {
            _storage = storage;
            UpdatePanels();
        }

        private void UpdatePanels()
        {
            _listView.Hide();
            _gridView.Hide();
            _lockPanel.Hide();
            _trapPanel.Hide();

            if(_storage.lockLevel > 0)
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
                _listView.UpdateLayout(_storage);
                return;
            }

            _gridView.Show();
            _gridView.UpdateLayout(_storage);
        }

        public void AddToObserve(ItemSlotWithPrice target)
        {
            target.OnClick += ProcessClick;
            target.SetValueStorage(_actionPoints);
        }

        public void RemoveFromObserve(ItemSlotWithPrice target)
        {
            target.OnClick -= ProcessClick;
        }

        private void ProcessClick(ItemSlotData data)
        {
            _actionPoints.ReduceValue(data.slotPrice);
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
    }
}
