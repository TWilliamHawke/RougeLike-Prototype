using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.Events;

namespace Magic
{
	[System.Serializable]
	public class KnownSpellData
	{
		public static event UnityAction<KnownSpellData> OnChangeData;

		Spell _spell;
		int _rank = 1;
		SpellString[] _activeStrings;

		//other classes shouldn't read spell data directly
		public int rank => _rank;
		public SpellString[] activeStrings => _activeStrings;
		public int manaCost => _spell[_rank].manaCost;
		public string displayName => _spell.displayName;
		public Sprite icon => _spell.icon;

        public KnownSpellData(Spell spell)
        {
            _spell = spell;
			_rank = _spell.startRank;
			_activeStrings = new SpellString[6];
        }

		public void IncreaseRank()
		{
			if(_rank >= 6) return;

			_rank++;
			OnChangeData?.Invoke(this);
		}
    }
}