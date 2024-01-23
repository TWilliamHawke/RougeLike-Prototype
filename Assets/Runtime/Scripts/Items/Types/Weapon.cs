using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]
    public class Weapon : Item, IEquipment
    {
        [SerializeField] DamageEffect _damageType;
        [Min(1)]
        [SerializeField] int _minDamage;
        [Min(1)]
        [SerializeField] int _maxDamage;
        [Min(1)]
        [SerializeField] int _accuracy;

        public AudioClip attackSound => useSound;
        public EquipmentTypes equipmentType => EquipmentTypes.weapon;

        public override string GetDescription()
        {
            return $"Min admage: {_minDamage}\nMax Damage: {_maxDamage}\nAccuracy: {_accuracy}";
        }

        public override string GetItemType()
        {
            return "Weapon";
        }
    }
}