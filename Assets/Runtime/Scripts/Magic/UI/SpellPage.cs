using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.UI
{
	public class SpellPage : MonoBehaviour
	{
		KnownSpellData _knownSpell;

	    public void Open(KnownSpellData knownSpell)
		{
			_knownSpell = knownSpell;
		}
	}
}