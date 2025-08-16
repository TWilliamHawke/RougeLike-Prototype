using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;
using System.Text;
using Abilities;

namespace Items
{
	[CreateAssetMenu(fileName = "Magic Card", menuName = "Items/Magic Card")]
	public class SpellString : Item
	{
		[UseFileName]
	    [SerializeField] Color _previewColor = Color.red;
		[SerializeField] int _spellPowerMod;
		[SerializeField] int _manaCostMod;
        [SerializeField] SourceEffectData[] _effects;

		public int spellPowerMod => _spellPowerMod;
		public int manaCostMod => _manaCostMod;
        public IEnumerable<SourceEffectData> effects => _effects;

        const string _itemType = "SpellString";

        public override string GetDescription()
        {
            var sb = new StringBuilder();
            AbilityModifiers abilityModifiers = new AbilityModifiers(1f);
            _effects.ForEach(effect => effect.AddDescription(ref sb, abilityModifiers));
            return sb.ToString();
        }

        public override string GetItemType()
        {
            return _itemType;
        }
    }
}