using UnityEngine;

namespace Items.Equipment.UI
{
    public class EquipmentScreen : MonoBehaviour, IObserversController<EquipmentSlot>
	{
		[SerializeField] PlayerEquipment _playerEquipment;
		[SerializeField] UIScreen _screen;
		[SerializeField] EquipmentSlot[] _equipmentSlots;

        void Awake()
		{
			_screen.OnScreenOpen += ShowEquipmentInSlots;
			_equipmentSlots.ForEach(slot => slot.Init());
		}

        public void AddObserver(IObserver<EquipmentSlot> observer)
        {
            _equipmentSlots.ForEach(slot => observer.AddToObserve(slot));
        }

        public void RemoveObserver(IObserver<EquipmentSlot> observer)
        {
            _equipmentSlots.ForEach(slot => observer.RemoveFromObserve(slot));
        }

		public void ShowEquipmentInSlots()
		{
			_equipmentSlots.ForEach(slot => slot.Clear());
			_equipmentSlots.ForEach(slot => slot.SelectEquipment(_playerEquipment));
		}
	}
}