using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	[CreateAssetMenu(fileName = "Magic Card", menuName = "Items/Magic Card")]
	public class SpellString : Item
	{
		[UseFileName]
	    [SerializeField] Color _previewColor = Color.red;
		[SerializeField] int _spellPowerMod;
		[SerializeField] int _manaCostMod;

		
    }
}