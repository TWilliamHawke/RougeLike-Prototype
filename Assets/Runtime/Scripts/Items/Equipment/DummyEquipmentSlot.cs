using UnityEngine;

namespace Items.Equipment
{
    public struct DummyEquipmentSlot : IEquipmentSlotTemplate
	{
        public int index => 0;
        public string displayName => _item.displayName;
        public Sprite icon => _item.icon;

		IItem _item;

        public DummyEquipmentSlot(IItem item)
        {
            _item = item;
        }
	}
}