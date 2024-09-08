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
        public event UnityAction<int> OnContainerSelected;
        ItemContainer _selectedContainer;
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
            OnContainerSelected?.Invoke(0);
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
            OnContainerSelected?.Invoke(idx);
        }

        private void UpdateButtonIcons()
        {
            SetIcon(_bigButton, _inventory[_bigButtonPosition]);

            for (int i = 0; i < _smallButtons.Count; i++)
            {
                if (i + 1 >= _inventory.sectionsCount)
                {
                    _smallButtons[i].gameObject.SetActive(false);
                    continue;
                }

                int containerIdx = _bigButtonPosition > i ? i : i+1;
                SetIcon(_smallButtons[i], _inventory[containerIdx]);
            }
        }

        private void SetIcon(DynamicButton button, ItemContainer storage)
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
