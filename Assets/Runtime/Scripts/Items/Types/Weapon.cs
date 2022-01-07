using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]
	public class Weapon : Item, IEquipment
	{
		[Min(1)]
		[SerializeField] int _minDamage;
		[Min(1)]
		[SerializeField] int _maxDamage;
		[SerializeField] int _accuracy;

        public EquipmentTypes equipmentType => EquipmentTypes.weapon;

	}
}