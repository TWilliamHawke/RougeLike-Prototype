using System.Collections;
using System.Collections.Generic;
using Entities.NPC;
using Entities.PlayerScripts;
using Entities.Stats;
using TMPro;
using UnityEngine;
using System.Linq;

namespace Items.UI
{
    public class StealingController : MonoBehaviour, IObserver<ItemSlot>, IStealingController
    {
        [SerializeField] PlayerStats _playerStats;
        [SerializeField] Inventory _playerInventory;
        [SerializeField] StatList _statList;
        [Header("UI Elements")]
        [SerializeField] UIScreen _stealingScreen;
        [SerializeField] ShopSection _catchedItems;
        [SerializeField] StorageSelector _storageList;
        [SerializeField] Bar _actionPointsBar;
        [SerializeField] StorageViewer _storageViewer;

        [SerializeField] TextMeshProUGUI _playerSkillText;
        [SerializeField] TextMeshProUGUI _targetSecurityText;

        [InjectField] LootPanel _lootPanel;

        int _playerSkill = 69;
        int _targetSecurity = 42;

        ValueStorage _actionPoints;
        IInteractiveStorage _NPCInventory;
        IItemPriceCalculator _priceCalculator;

        List<ItemSlotData> _selectedItems = new();
        HashSet<ItemSlotData> _selectedItemsSet = new();

        int maxActionPoints => Mathf.CeilToInt(100f * _playerSkill / _targetSecurity);

        void Awake()
        {
            _catchedItems.AddObserver(this);
            _storageList.OnContainerSelected += ShowStorage;
            _storageViewer.OnItemSelection += AddItemsToSelection;
            _priceCalculator = new StealingCostCalculator(_playerStats);
        }

        public void OpenScreen(IInteractiveStorage inventory)
        {
            _NPCInventory = inventory;
            _priceCalculator.SetPrices(inventory);
            _storageList.SetInventory(inventory);
            InitActionPoints();
            _catchedItems.UpdateLayout(_selectedItems);
            SetSkillText();
            _stealingScreen.Open();
        }

        public void AddToObserve(ItemSlot target)
        {
            target.OnClick += RemoveItemFromSelection;
        }

        public void RemoveFromObserve(ItemSlot target)
        {
            target.OnClick -= RemoveItemFromSelection;
        }

        //Used in Unity Editor
        public void FinalizeStealing()
        {
            LootContainer loot = new();
            loot.AddItems(_selectedItems);
            _selectedItems.ForEach(slot => slot.RemoveAllItems());
            _selectedItemsSet.Clear();
            _selectedItems.Clear();
            _stealingScreen.Close();
            if (loot.IsEmpty()) return;

            _NPCInventory.isStealingTarget = false;
            _lootPanel.Open(loot);
        }

        private void ShowStorage(int storageNumber)
        {
            _storageViewer.ShowStorage(_NPCInventory.ContainerAt(storageNumber));
        }

        private void InitActionPoints()
        {
            _actionPoints = new ValueStorage(0, maxActionPoints, maxActionPoints);
            _storageViewer.SetActionPoints(_actionPoints, _selectedItemsSet);
            _actionPointsBar.AddToObserve(_actionPoints);
        }

        private void SetSkillText()
        {
            _playerSkillText.text = _playerSkill.ToString();
            _targetSecurityText.text = _targetSecurity.ToString();
        }

        private void RemoveItemFromSelection(ItemSlotData item)
        {
            if (!_selectedItemsSet.Contains(item)) return;

            _actionPoints.IncreaseValue(item.slotPrice);
            _selectedItemsSet.Remove(item);
            _selectedItems.Remove(item);
            _catchedItems.UpdateLayout(_selectedItems);
            _storageViewer.UpdatePanels();
        }

        private void AddItemsToSelection(ItemSlotData item)
        {
            if (_selectedItemsSet.Contains(item)) return;
            if (!_actionPoints.TryReduceValue(item.slotPrice)) return;

            _selectedItemsSet.Add(item);
            _selectedItems.Add(item);
            _catchedItems.UpdateLayout(_selectedItems);
            _storageViewer.UpdatePanels();
        }
    }
}
