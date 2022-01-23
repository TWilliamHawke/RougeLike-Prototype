using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magic
{
	[CreateAssetMenu(fileName = "Spell", menuName = "EffectHandlers/Spell")]
	public class Spell : ScriptableObject, ISpriteGetter
	{
		[UseFileName]
		[SerializeField] string _displayName;
		[SpritePreview]
		[SerializeField] Sprite _icon;
		[Range(1,5)]
		[SerializeField] int _startRank = 1;
		[Space(10)]
		[SerializeField] SpellLevelData[] _levels;

		public int startRank => _startRank;
		public Sprite icon => _icon;
		public string displayName => _displayName;
        public Sprite sprite => _icon;

        public SpellLevelData this[int idx] => GetLevelData(idx);

        private SpellLevelData GetLevelData(int idx)
        {
            int index = Mathf.Clamp(idx, _startRank, 6) - _startRank;
			index = Mathf.Min(index, _levels.Length - 1);
			return _levels[index];
        }
    }
}