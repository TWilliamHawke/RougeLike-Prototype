using UnityEngine;

namespace Items.Equipment.UI
{
    public class EquipmentSelectionObserver : MonoBehaviour, IObserver<EquipmentSelectionButton>
	{
        [SerializeField] PlayerEquipment _equipment;
        [SerializeField] EquipmentSelectionLayout _layout;
		[SerializeField] UIScreen _screen;

		void Awake()
		{
			_layout.AddObserver(this);
		}

		public void AddToObserve(EquipmentSelectionButton target)
		{
			target.OnClick += SelectEquipment;
		}

		public void RemoveFromObserve(EquipmentSelectionButton target)
		{
			target.OnClick -= SelectEquipment;
		}

		private void SelectEquipment(ItemSlotData data)
		{
			var itemSlot = data.GetEquipmentSlot();
			if (itemSlot.equipmentType == EquipmentTypes.none) return;
			_equipment.Equip(data);
			_screen.Close();
		}
	}
}