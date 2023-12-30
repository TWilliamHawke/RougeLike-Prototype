using System.Collections;
using System.Collections.Generic;
using Entities.NPC;
using Items;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace Items.UI
{
    public class StorageSelector : MonoBehaviour
    {
        public event UnityAction<int> OnStorageSelected;
        ItemStorage _selectedStorage;
        INPCInventory _inventory;

        [SerializeField] Sprite _equipmentSprite;
        [SerializeField] Sprite _chestSprite;
        [Header("UI Elements")]
        [SerializeField] List<DynamicButton> _smallButtons;
        [SerializeField] DynamicButton _bigButton;

        int _bigButtonPosition = 0;

        void Start()
        {
            _smallButtons.ForEach(btn => btn.OnClick += UpdateSelectedStorage);
        }

        public void SetInventory(INPCInventory inventory)
        {
            _inventory = inventory;
            UpdateButtonIcons();
            OnStorageSelected?.Invoke(0);
        }

        private void UpdateSelectedStorage(DynamicButton itemStorage)
        {
            var idx = _smallButtons.IndexOf(itemStorage);
            if (idx >= _bigButtonPosition)
            {
                idx++;
            }

            _bigButton.transform.SetSiblingIndex(idx);
            _bigButtonPosition = idx;
            UpdateButtonIcons();
            OnStorageSelected?.Invoke(idx);
        }

        private void UpdateButtonIcons()
        {
            SetIcon(_bigButton, _inventory[_bigButtonPosition]);

            for (int i = 0; i < _smallButtons.Count; i++)
            {
                if (i + 1 >= _inventory.storageCount)
                {
                    _smallButtons[i].gameObject.SetActive(false);
                    continue;
                }

                int storageIdx = _bigButtonPosition > i ? i : i+1;
                SetIcon(_smallButtons[i], _inventory[storageIdx]);
            }
        }

        private void SetIcon(DynamicButton button, ItemStorage storage)
        {
            Sprite sprite = storage.storageType switch
            {
                ItemStorageType.inventory => _equipmentSprite,
                _ => _chestSprite
            };

            button.SetIcon(sprite);
        }
    }
}
