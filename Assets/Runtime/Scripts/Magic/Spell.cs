using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magic
{
	[CreateAssetMenu(fileName = "Spell", menuName = "EffectHandlers/Spell")]
	public class Spell : ScriptableObject
	{
		[UseFileName]
		[SerializeField] string _displayName;
		[SpritePreview]
		[SerializeField] Sprite _icon;
		[Range(1,5)]
		[SerializeField] int _startLevel = 1;
		[Space(10)]
		[SerializeField] SpellLevelData[] _levels;

		public int startLevel => _startLevel;

		SpellLevelData this[int idx] => _levels[idx];
	}
}