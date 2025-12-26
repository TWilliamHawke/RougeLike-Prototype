using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effects;
using Abilities;

namespace Magic
{
	[System.Serializable]
	public class SpellLevelData
	{
		public AbilityTemplate spellEffect;
		public int manaCost;

		public IAbility CreateAbility()
		{
			return spellEffect.CreateAbility();
		}
	}
}