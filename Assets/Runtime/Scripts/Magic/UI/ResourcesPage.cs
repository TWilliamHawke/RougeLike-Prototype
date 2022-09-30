using System.Collections;
using System.Collections.Generic;
using Entities.PlayerScripts;
using UnityEngine;
using TMPro;
using Items;

namespace Magic.UI
{
    public class ResourcesPage : UIPanelWithGrid<ItemSlotData>
    {
        [SerializeField] Inventory _inventory;
        [Header("UI Elements")]
        [SerializeField] TextMeshProUGUI _magicDustCount;

        IItemSection _spellStringSection;

        protected override IEnumerable<ItemSlotData> _layoutElementsData => _inventory.spellStrings;

        public void Init()
        {
            _inventory.resources.OnResourceChange += UpdateDustCount;
            _inventory.spellStrings.OnItemAdd += UpdateLayout;
            _inventory.spellStrings.OnItemRemove += UpdateLayout;
        }

        void OnDestroy()
        {
            _inventory.resources.OnResourceChange -= UpdateDustCount;
            _inventory.spellStrings.OnItemAdd -= UpdateLayout;
            _inventory.spellStrings.OnItemRemove -= UpdateLayout;

        }

        void OnEnable()
        {
            UpdateLayout();
            _magicDustCount.text = _inventory.resources[ResourceType.magicDust].ToString();
            
        }

        void UpdateDustCount(ResourceType type)
        {
            if (type != ResourceType.magicDust) return;

            _magicDustCount.text = _inventory.resources[ResourceType.magicDust].ToString();
        }


    }
}