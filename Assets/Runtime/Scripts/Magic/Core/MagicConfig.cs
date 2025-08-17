using Effects;
using Entities.Stats;
using UnityEngine;

namespace Magic
{
    [CreateAssetMenu(fileName = "MagicConfig", menuName = "Magic/MagicConfig")]
    public class MagicConfig : ScriptableObject
    {
        [SerializeField] ResourceChangeFactor _spellCostFactor;
        [SerializeField] StaticStat _spellPower;
        [SerializeField] StoredResource _mana;
        [Range(0, 1)]
        [SerializeField] float _minSpellCostOfBase = .1f;

        public float minCostOfBase => _minSpellCostOfBase;

        public ISafeStatController FindManaStorage(StatsContainer statsContainer)
        {
            return statsContainer.FindStorage(_mana);
        }

        public StaticStatStorage FindSpellPowerStorage(StatsContainer statsContainer)
        {
            return statsContainer.FindStorage(_spellPower);
        }

        public int GetSpellCost(KnownSpellData spellData, StatsContainer statsContainer)
        {
            int manaCost = spellData.baseManaCost;
            var activeEffects = spellData.activeEffects;
            int minCost = Mathf.CeilToInt(_minSpellCostOfBase * manaCost);

            //UNDONE it should iterate trough all effect containers
            manaCost = _spellCostFactor.AdjustValue(manaCost, statsContainer, activeEffects);
            return Mathf.Max(minCost, manaCost);
        }
    }
}