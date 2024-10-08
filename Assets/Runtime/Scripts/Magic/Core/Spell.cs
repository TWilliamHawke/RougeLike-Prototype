using System;
using System.Collections;
using System.Collections.Generic;
using Abilities;
using Effects;
using UnityEngine;

namespace Magic
{
    [CreateAssetMenu(fileName = "Spell", menuName = "EffectHandlers/Spell")]
	public class Spell : ScriptableObject, ISpriteGetter, IEffectSource
	{
		[UseFileName]
		[SerializeField] string _displayName;
		[SpritePreview]
		[SerializeField] Sprite _icon;
		[Range(1,5)]
		[SerializeField] int _startRank = 1;
		[Space(10)]
		[SerializeField] SpellLevelData[] _levels;
		[TextArea(5,10)]
		[Tooltip("Use %m for spell magnitude and %d for spell duration")]
		[SerializeField] string _description;

		public int startRank => _startRank;
		public Sprite icon => _icon;
		public string displayName => _displayName;
        public Sprite sprite => _icon;
		public string description => _description;

        public int GetCostAt(int rank)
        {
            var levelData = GetLevelData(rank);
            return levelData.manaCost;
        }

        public Ability GetEffectAt(int rank)
        {
            var levelData = GetLevelData(rank);
            return levelData.spellEffect;
        }

        private SpellLevelData GetLevelData(int idx)
        {
            int index = Mathf.Clamp(idx, _startRank, 6) - _startRank;
			index = Mathf.Min(index, _levels.Length - 1);
			return _levels[index];
        }


    }
}