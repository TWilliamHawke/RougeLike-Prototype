using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.UI
{
    public class SpellList : UIPanelWithGrid<KnownSpellData>
    {
        [SerializeField] Spellbook _spellBook;

        protected override IEnumerable<KnownSpellData> _layoutElementsData => _spellBook.knownSpells;

		public void UpdateSpellList()
		{
            Debug.Log("uodate");
			UpdateLayout();
		}
    }
}


