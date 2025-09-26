using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;
using System.Text;
using Abilities;

namespace Items
{
	[CreateAssetMenu(fileName = "Magic Card", menuName = "Items/Magic Card")]
	public class SpellStringTemplate : ItemTemplate
	{
		[UseFileName]
	    [SerializeField] Color _previewColor = Color.red;
        [SerializeField] SourceEffectData[] _effects;

        public IEnumerable<SourceEffectData> effects => _effects;

        public override IItem CreateItem(int rarity = 0)
        {
            return new SpellString(this);
        }

        public override string GetDescription()
        {
            var sb = new StringBuilder();
            AbilityModifiers abilityModifiers = new AbilityModifiers(1f);
            foreach (var effect in _effects)
            {
                string description = effect.GetDescription(abilityModifiers);
                sb.AppendLine(description);
            }
            return sb.ToString();
        }
    }
}