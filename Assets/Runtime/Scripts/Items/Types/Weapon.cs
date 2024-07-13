using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]
    public class Weapon : Item, IEquipment
    {
        [SerializeField] ResourceChangeFactor _damageType;
        [SerializeField] IntValue _damage;
        [Min(1)]
        [SerializeField] int _accuracy;

        public AudioClip attackSound => useSound;
        public EquipmentTypes equipmentType => EquipmentTypes.weapon;

        public override string GetDescription()
        {
            return $"Damage: {_damage}\nAccuracy: {_accuracy}";
        }

        public override string GetItemType()
        {
            return "Weapon";
        }
    }
}