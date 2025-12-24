using UnityEngine;

namespace Items.Equipment
{
    public interface IEquipmentSlotTemplate
    {
		string displayName { get; }
		Sprite icon { get; }
        int index { get; }
    }
}