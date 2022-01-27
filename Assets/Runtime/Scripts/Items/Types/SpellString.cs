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

		public int spellPowerMod => _spellPowerMod;
		public int manaCostMod => _manaCostMod;

        public override string GetDescription()
        {
            return $"Spell power: {_spellPowerMod}%\nSpell cost: {_manaCostMod}%";
        }

        public override string GetItemType()
        {
            return "SpellString";
        }
    }
}