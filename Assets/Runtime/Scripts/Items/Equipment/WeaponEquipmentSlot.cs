using UnityEngine;

namespace Items.Equipment
{
    public struct WeaponEquipmentSlot : IEquipmentSlotTemplate
    {
        public int index => (int)type;
        public string displayName => _weaponTemplate.name;
        public Sprite icon => _weaponTemplate.icon;

        static EquipmentTypes type => EquipmentTypes.weapon;
        WeaponTemplate _weaponTemplate;

        public WeaponEquipmentSlot(WeaponTemplate weaponTemplate)
        {
            _weaponTemplate = weaponTemplate;
        }
    }
}