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
    public class StealingController : MonoBehaviour, IObserver<UIDataElement<ItemSlotData>>, IStealingController
    {
        [SerializeField] PlayerStats _playerStats;
        [SerializeField] StatList _statList;
        [Header("UI Elements")]
        [SerializeField] UIScreen _stealingScreen;
        [SerializeField] InventorySection _itemsForSteal;
        [SerializeField] InventorySection _catchedItems;

        [SerializeField] TextMeshProUGUI _playerSkillText;
        [SerializeField] TextMeshProUGUI _targetSecurityText;

        int _playerSkill = 69;
        int _targetSecurity = 42;

        ItemSection _stolenItems = new(ItemStorageType.loot, 5);

        int maxActionPoints => Mathf.CeilToInt(100f * _playerSkill / _targetSecurity);

        public void OpenScreen(INPCInventory inventory)
        {
            _stealingScreen.Open();
            _itemsForSteal.UpdateLayout(inventory.FirstOrDefault());
            _catchedItems.UpdateLayout(_stolenItems);
            _playerSkillText.text = _playerSkill.ToString();
            _targetSecurityText.text = _targetSecurity.ToString();
        }

        public void AddToObserve(UIDataElement<ItemSlotData> target)
        {

        }

        public void RemoveFromObserve(UIDataElement<ItemSlotData> target)
        {

        }

        void Awake()
        {
            _itemsForSteal.AddObserver(this);
            _catchedItems.AddObserver(this);
        }

        void ProcessClick(ItemSlotData itemSlotData)
        {
            Debug.Log("click");
        }
    }

    public interface IStealingController
    {
        void OpenScreen(INPCInventory inventory);
    }
}
