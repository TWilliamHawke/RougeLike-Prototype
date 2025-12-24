using TMPro;
using UnityEngine;

namespace Items.Equipment.UI
{
    public class EquipmentSelectionScreen : MonoBehaviour, IEquipmentSelectior, IObserver<EquipmentSlot>
    {
        [SerializeField] ItemSectionTemplate _mainSection;
        [SerializeField] ItemSectionTemplate _storageSection;
        [SerializeField] Inventory _inventory;
        [SerializeField] EquipmentSelectionButton _buttonPrefab;
		[SerializeField] TextMeshProUGUI _equipmentSlotName;

        [SerializeField] EquipmentSelectionLayout _layout;
		[SerializeField] UIScreen _screen;
		[SerializeField] EquipmentScreen _equipmentScreen;

		void Awake()
		{
			_equipmentScreen.AddObserver(this);
		}

        public void AddToObserve(EquipmentSlot target)
        {
            target.OnEmptySlotClick += ShowMainItems;
        }

        public void RemoveFromObserve(EquipmentSlot target)
        {
            target.OnEmptySlotClick -= ShowMainItems;
        }

        public void ShowMainItems(IEquipmentSlotTemplate slotTemplate)
        {
			_equipmentSlotName.SetLocalisedText(slotTemplate.displayName);
			_screen.Open();
            _layout.ClearLayout();
            ShowItemsInSection(slotTemplate, _mainSection);
        }

        public void ShowStorageItems(IEquipmentSlotTemplate slotTemplate)
        {
            ShowMainItems(slotTemplate);
            ShowItemsInSection(slotTemplate, _storageSection);
        }

        private void ShowItemsInSection(IEquipmentSlotTemplate slotTemplate, ItemSectionTemplate sectionTemplate)
        {
            var mainSection = _inventory.GetSection(sectionTemplate);
			Debug.Log("Search for " + slotTemplate.displayName);

            foreach (var slot in mainSection)
            {
				// Debug.Log(slot.item.displayName);
				if (slot.GetEquipmentSlot() != null)
				{
					Debug.Log(slot.GetEquipmentSlot().displayName);
				}
                if (slot.GetEquipmentSlot() != slotTemplate) continue;

                var button = _layout.CreateLayoutElement(_buttonPrefab);
                button.BindData(slot);
            }
        }
    }
}