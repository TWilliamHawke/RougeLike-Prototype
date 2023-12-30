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
        [SerializeField] StatList _statList;
        [Header("UI Elements")]
        [SerializeField] UIScreen _stealingScreen;
        [SerializeField] InventorySection _catchedItems;
        [SerializeField] StorageSelector _storageList;
        [SerializeField] Bar _actionPointsBar;
        [SerializeField] StorageViewer _storageViewer;

        [SerializeField] TextMeshProUGUI _playerSkillText;
        [SerializeField] TextMeshProUGUI _targetSecurityText;

        int _playerSkill = 69;
        int _targetSecurity = 42;

        ValueStorage _actionPoints;
        INPCInventory _inventory;

        ItemSection _stolenItems = new(ItemStorageType.loot, 5);

        int maxActionPoints => Mathf.CeilToInt(100f * _playerSkill / _targetSecurity);

        void Awake()
        {
            _catchedItems.AddObserver(this);
            _storageList.OnStorageSelected += ShowStorage;
        }

        public void OpenScreen(INPCInventory inventory)
        {
            _inventory = inventory;
            _actionPoints = new ValueStorage(0, maxActionPoints, maxActionPoints);
            _storageViewer.SetActionPoints(_actionPoints);
            _storageList.SetInventory(inventory);
            _actionPointsBar.AddToObserve(_actionPoints);
            _stealingScreen.Open();
            var selectedStorage = inventory.FirstOrDefault();
            _catchedItems.UpdateLayout(_stolenItems);
            _playerSkillText.text = _playerSkill.ToString();
            _targetSecurityText.text = _targetSecurity.ToString();
        }

        public void AddToObserve(ItemSlot target)
        {
            target.OnClick += ProcessClick;
        }

        public void RemoveFromObserve(ItemSlot target)
        {
            target.OnClick -= ProcessClick;
        }

        private void ShowStorage(int storageNumber)
        {
            _storageViewer.ShowStorage(_inventory[storageNumber]);
        }

        void ProcessClick(ItemSlotData itemSlotData)
        {
            _actionPoints.IncreaseValue(itemSlotData.slotPrice);
        }
    }

    public interface IStealingController
    {
        void OpenScreen(INPCInventory inventory);
    }
}
