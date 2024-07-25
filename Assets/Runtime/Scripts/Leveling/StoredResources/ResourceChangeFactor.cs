using System.Collections;
using System.Collections.Generic;
using Entities.Stats;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "Damage", menuName = "Effects/Damage Type")]
    public class ResourceChangeFactor : ScriptableObject, IEffectSignature
    {
        [LocalisationKey]
        [SerializeField] string _displayName;
        [LocalisationKey]
        [SerializeField] string _description;
        [SpritePreview]
        [SerializeField] Sprite _icon;

        [SerializeField] BonusValueType _type;
        [SerializeField] StoredResource _targetStat;
        [SerializeField] StaticStat _resist;

        [SerializeField] StaticStat[] _factormods;

        public StoredResource targetResource => _targetStat;

        public int ApplyStatsToValue(int baseValue, StatsContainer statsContainer, IEffectsIterator effects)
        {
            float updatedValue = baseValue;

            foreach (var stat in _factormods)
            {
                var storage = statsContainer.FindStorage(stat);
                int statValue = storage.GetAdjustedValue(effects);
                updatedValue = updatedValue * (1 + statValue * 0.01f);
            }

            return Mathf.FloorToInt(updatedValue);
        }
    }
}
