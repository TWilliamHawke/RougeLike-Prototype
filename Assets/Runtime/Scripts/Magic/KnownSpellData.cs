using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Magic
{
	[System.Serializable]
	public class KnownSpellData
	{
		[SerializeField] Spell _spell;
		[SerializeField] int _rank = 1;
		[SerializeField] SpellString[] _magicCards;

		public int rank => _rank;
		public SpellString[] magicCards => _magicCards;
		public int manaCost => _spell[_rank].manaCost;
		public string displayName => _spell.displayName;
		public Sprite icon => _spell.icon;

        public KnownSpellData(Spell spell)
        {
            _spell = spell;
			_rank = _spell.startRank;
			_magicCards = new SpellString[6];
        }
    }
}