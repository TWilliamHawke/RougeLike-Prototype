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
        IContainersList _NPCInventory;
        IItemPriceCalculator _priceCalculator;

        int maxActionPoints => Mathf.CeilToInt(100f * _playerSkill / _targetSecurity);

        void Awake()
        {
            _catchedItems.AddObserver(this);
            _storageList.OnContainerSelected += ShowStorage;
            _storageViewer.OnItemSelection += UpdateSelectedItems;
            _priceCalculator = new StealingCostCalculator(_playerStats);
        }

        public void OpenScreen(IContainersList inventory)
        {
            _NPCInventory = inventory;
            _priceCalculator.SetPrices(inventory);
            _storageList.SetInventory(inventory);
            InitActionPoints();
            UpdateSelectedItems();
            SetSkillText();
            _stealingScreen.Open();
        }

        public void AddToObserve(ItemSlot target)
        {
            target.OnClick += ProcessClick;
        }

        public void RemoveFromObserve(ItemSlot target)
        {
            target.OnClick -= ProcessClick;
        }

        //Used in Unity Editor
        public void FinalizeStealing()
        {
            LootContainer loot = new();
            //loot.AddSelectedItemsFrom(_NPCInventory);
            _stealingScreen.Close();
            if (loot.IsEmpty()) return;

            _lootPanel.Open(loot);
        }

        private void ShowStorage(int storageNumber)
        {
            _storageViewer.ShowStorage(_NPCInventory.ContainerAt(storageNumber));
        }

        private void InitActionPoints()
        {
            _actionPoints = new ValueStorage(0, maxActionPoints, maxActionPoints);
            _storageViewer.SetActionPoints(_actionPoints);
            _actionPointsBar.AddToObserve(_actionPoints);
        }

        private void SetSkillText()
        {
            _playerSkillText.text = _playerSkill.ToString();
            _targetSecurityText.text = _targetSecurity.ToString();
        }

        private void ProcessClick(ItemSlotData item)
        {
            _actionPoints.IncreaseValue(item.slotPrice);
            RemoveFromSelection(item);
            UpdateSelectedItems();
            _storageViewer.UpdatePanels();
        }

        private void UpdateSelectedItems()
        {
            _catchedItems.ShowSelectedItems(_NPCInventory);
        }

        private void RemoveFromSelection(ItemSlotData item)
        {
            foreach(var section in _NPCInventory.GetAllContainers())
            {
                section.DeselectItem(item);
            }
        }
    }

    public interface IStealingController
    {
        void OpenScreen(IContainersList inventory);
    }
}
